using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace nnnn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hellololo");
            using (northwindContext db = new northwindContext())
            {
                var task1 = db.Customers
                               .Where(c => c.ContactName.StartsWith("D"));

                var task2 = from customers in db.Customers
                            select new { ContactName = customers.ContactName.ToUpper() };
                

                var task3 = (from customers in db.Customers
                            select customers.Country)
                            .Distinct();



                var task4 = from customers in db.Customers
                            where customers.City == "London" && customers.ContactTitle.StartsWith("Sales")
                            select customers.ContactName;


                var task5 = from product in db.Products
                            from ord in db.OrderDetails
                            where product.ProductName == "Tofu" && product.ProductId == ord.ProductId
                            select ord.OrderId;
                

                var task6 = (from product in db.Products
                             from ord in db.OrderDetails
                             from order in db.Orders
                             where product.ProductId == ord.ProductId && ord.OrderId == order.OrderId && order.ShipCountry == "Germany"
                             select product.ProductName)
                            .Distinct();
                

                var task7 = (from product in db.Products
                             from customer in db.Customers
                             from order in customer.Orders
                             from ord in order.OrderDetails
                             where product.ProductId == ord.ProductId && product.ProductName == "Ikura"
                             select customer.ContactName)
                            .Distinct();
                
                var task8 = (from employee in db.Employees
                             join order in db.Orders on employee.EmployeeId equals order.EmployeeId
                             into gj
                             from order in gj.DefaultIfEmpty()
                             select new {
                                 Name = employee.FirstName,
                                 Surename = employee.LastName,
                                 OrderId = order.OrderId })
                                .Distinct();

                var task9 = (from order in db.Orders
                             join employee in db.Employees on order.EmployeeId equals employee.EmployeeId
                             into gj
                             from employee in gj.DefaultIfEmpty()
                             select new {
                                 Name = employee.FirstName,
                                 Surename = employee.LastName,
                                 OrderId = order.OrderId })
                                .Distinct();

                var task10 = (from supplier in db.Suppliers select supplier.Phone)
                    .Union(from shipper in db.Shippers select shipper.Phone);



                var task11 = from customer in db.Customers
                             group customer by customer.City into g
                             select new
                             {
                                 name = g.Key,
                                 count = g.Count()

                             };


                var task12 = from customer in db.Customers
                             where customer.Orders.Count() > 10
                             select new
                             {
                                 Name = customer.ContactName,
                                 Orders = customer.Orders.Count()
                             };

                String reg = @"\d{4}-\d{4}$";
                Regex regex = new Regex(reg);
                var task13 = from customer in db.Customers
                             where regex.IsMatch(customer.Phone)
                             select new
                             {
                                 Name = customer.ContactName,
                                 Phone = customer.Phone
                             };


                var maxAmount = (from customer in db.Customers
                              select new
                              {
                                  customer.ContactName,
                                  customer.Orders.Count
                              })
                    .Max(c => c.Count);
                var task14 = (from customer in db.Customers
                              where customer.Orders.Count() == maxAmount
                              select customer.ContactName)
                             .Single();




                PrintRowWithProperties(task1, "1.Select all customers whose name starts with letter 'D' ");
                PrintRowWithProperties(task2, "2.Convert names of all customers to Upper Case");
                PrintRes(task3, "3.Select distinct country from Customers");
                PrintRes(task4, "4.Select Contact name from Customers Table from Lindon and title like “Sales”");
                PrintRes(task5, "5.Select all orders id where was bought “Tofu”");
                PrintRes(task6, "6.Select all product names that were shipped to Germany");
                PrintRes(task7, "7.Select all customers that ordered \"Ikura\"");
                PrintRowWithProperties(task8, "8.Select all employees and any orders they might have");
                PrintRowWithProperties(task9, "9.Selects all employees, and all orders");
                PrintRes(task10, "10.Select all phones from Shippers and Suppliers");
                PrintRowWithProperties(task11, "11.Count all customers grouped by city");
                PrintRowWithProperties(task12, "12.Select all customers that placed more than 10 orders with average Unit Price less than 17");
                PrintRowWithProperties(task13, "13.Select all customers with phone that has format (’NNNN-NNNN’)");
                Console.WriteLine("14.Select customer that ordered the greatest amount of goods (not price)");
                Console.WriteLine(task14);
                //PrintRes(task15, "15.");
                Console.ReadKey();






            }
        }

        public static void PrintRowWithProperties(IQueryable<Object> queue, string message)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine(message);
            var list = queue.ToList();
            Type t = list.First().GetType();
            IList<PropertyInfo> proper = new List<PropertyInfo>(t.GetProperties());
            foreach (var res in list)
            {
                foreach (PropertyInfo p in proper)
                {
                    try
                    {
                        object ob = p.GetValue(res, null);
                        if (ob != null)
                            Console.Write(p.Name + ": " + ob.ToString() + "; ");
                    }catch(Exception e)
                    {

                    }
                }
                //Console.WriteLine("---");
                Console.WriteLine();
            }
        }

        public static void PrintRes(IQueryable list, string message)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine(message);

            foreach(var res in list)
            {
                Console.WriteLine(res);
            }

        }
    }
}
//C:\Users\Natalya\Documents\Visual Studio 2015\Projects\nnnn
