using System;

using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        DoublyLinkedList<T> list;

        public HybridFlowProcessor()
        {
            list = new DoublyLinkedList<T>();
        }

        public T Dequeue()
        {
            return AddToEnd();
        }

        public void Enqueue(T item)
        {
            ValidateItem(item);

            list.AddAt(0, item);
        }

        public T Pop()
        {
            return AddToEnd();
        }

        public void Push(T item)
        {
            ValidateItem(item);

            list.Add(item);
        }

        T AddToEnd()
        {
            if (list.Length == 0)
                throw new InvalidOperationException();

            return list.RemoveAt(list.Length - 1);
        }

        void ValidateItem(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }
    }
}
