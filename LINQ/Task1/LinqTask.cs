using System;
using System.Collections.Generic;
using System.Linq;

using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers
                .Where(customer => customer.Orders.Sum(order => order.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers
                .Select(customer => (customer, suppliers.Where(
                    supplier => supplier.Country == customer.Country && supplier.City == customer.City)));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers
                .Where(customer => customer.Orders.Any(order => order.Total > limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            return customers
                .Where(customer => customer.Orders.Any())
                .Select(customer => (customer, firstOrderDate: customer.Orders.OrderBy(order => order.OrderDate).First().OrderDate));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            return customers
                .Where(customer => customer.Orders.Any())
                .Select(customer => (customer, firstOrderDate: customer.Orders.OrderBy(order => order.OrderDate).First().OrderDate ))
                .OrderBy(data => data.firstOrderDate)
                .ThenByDescending(data => data.customer.Orders.Sum(order => order.Total))
                .ThenBy(data => data.customer.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return customers.
                Where(customer => customer.PostalCode.Any(c => char.IsLetter(c)) || customer.Region is null || !customer.Phone.StartsWith('('));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            return products
                .GroupBy(product => product.Category)
                .Select(groupedProduct => new Linq7CategoryGroup
                {
                    Category = groupedProduct.Key,
                    UnitsInStockGroup = groupedProduct
                        .GroupBy(x => x.UnitsInStock)
                        .Select(product => new Linq7UnitsInStockGroup { UnitsInStock = product.Key, Prices = product.Select(p => p.UnitPrice) })
                });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            return products.Where(p => p.UnitPrice <= cheap).Select(p => (cheap, p)).Concat(
                    products.Where(p => p.UnitPrice > cheap && p.UnitPrice <= middle).Select(p => (middle, p))).Concat(
                        products.Where(p => p.UnitPrice > middle && p.UnitPrice <= expensive).Select(p => (expensive, p))).GroupBy(p => p.Item1).Select(x => (x.Key, x.ToList().Select(p => p.p)));
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            return customers
                .GroupBy(customer => customer.City)
                .Select(groupedCustomers =>
                (
                    groupedCustomers.Key,
                    Convert.ToInt32(groupedCustomers.Average(a => a.Orders.Sum(order => order.Total))),
                    Convert.ToInt32(groupedCustomers.Average(a => a.Orders.Count()))
                ));
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            return suppliers
                .Select(supplier => supplier.Country)
                .Distinct()
                .OrderBy(city => city.Length)
                .ThenBy(city => city)
                .Aggregate((acc, city) => acc += city);
        }
    }
}