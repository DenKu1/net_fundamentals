using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Task1.DoNotChange;

namespace Task1
{
    public class Container
    {
        readonly HashSet<Type> types;

        readonly Dictionary<Type, Type> bindedTypes;

        public Container()
        {
            types = new HashSet<Type>();

            bindedTypes = new Dictionary<Type, Type>();
        }

        public void AddAssembly(Assembly assembly)
        {
            var exportedTypes = assembly
                .GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(ExportAttribute)))
                .Select(type => new { type, type.GetCustomAttribute<ExportAttribute>().Contract });
            
            foreach (var exportedType in exportedTypes)
            {
                if (exportedType.Contract == null)
                    AddType(exportedType.type);
                else
                    AddType(exportedType.type, exportedType.Contract);
            }

            var importedTypes = assembly.GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(ImportConstructorAttribute)))
                .ToList();

            foreach (var importedType in importedTypes)
            {
                AddType(importedType);
            }

            var importedViaFieldTypes = assembly.GetTypes()
                .Where(type => GetFieldInfos(type).Any(info => Attribute.IsDefined(info, typeof(ImportAttribute))));

            foreach (var importedType in importedViaFieldTypes)
            {
                AddType(importedType);
            }
        }

        public void AddType(Type type)
        {
            if (types.Contains(type))
                throw new ArgumentException(nameof(type));

            types.Add(type);
        }

        public void AddType(Type type, Type baseType)
        {
            bindedTypes.Add(baseType, type);

            AddType(type);
        }

        public T Get<T>()
        {
            var resultingType = typeof(T);

            if (bindedTypes.TryGetValue(resultingType, out Type bindedType))
                resultingType = bindedType;

            if (!types.Contains(resultingType))
                throw new ArgumentException(nameof(T));

            var fieldsForImport = resultingType.GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(ImportAttribute)));

            var args = GetCtorArgumentInstances(resultingType);

            var instance = (T)Activator.CreateInstance(resultingType, args);

            SetFieldForImportValues(resultingType, instance);

            return instance;
        }

        object[] GetCtorArgumentInstances(Type type)
        {
            var ctorArguments = type
                .GetConstructors()
                .Single()
                .GetParameters();

            var bindedCtorArguments = ctorArguments
                .Select(param => bindedTypes.TryGetValue(param.ParameterType, out Type bindedType)
                    ? bindedType : param.ParameterType);

            var ctorArgumentInstances = bindedCtorArguments
                .Select(argType => Activator.CreateInstance(argType))
                .ToArray();

            return ctorArgumentInstances;
        }

        void SetFieldForImportValues(Type type, object obj)
        {
            var fieldInfos = GetFieldInfos(type);

            var FieldInfosForImport = fieldInfos
                .Where(field => Attribute.IsDefined(field, typeof(ImportAttribute)));

            foreach (var fieldInfo in FieldInfosForImport)
            {
                var fieldType = GetFieldType(fieldInfo);

                if (bindedTypes.TryGetValue(fieldType, out Type bindedType))
                    fieldType = bindedType;

                var value = Activator.CreateInstance(fieldType);

                SetFieldValue(fieldInfo, obj, value);
            }
        }

        Type GetFieldType(MemberInfo info) => info switch
        {
            FieldInfo fieldInfo => fieldInfo.FieldType,
            PropertyInfo propInfo => propInfo.PropertyType,
            _ => null
        };

        void SetFieldValue(MemberInfo memberInfo, object target, object value)
        {
            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    fieldInfo.SetValue(target, value);
                    break;
                case PropertyInfo propInfo:
                    propInfo.SetValue(target, value);
                    break;
            }
        }

        IEnumerable<MemberInfo> GetFieldInfos(Type type) => type
            .GetFields()
            .Cast<MemberInfo>()
            .Concat(type.GetProperties());
    }
}