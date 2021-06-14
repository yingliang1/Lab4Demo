using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NewLanguageFeatures
{
    public delegate bool KeyValueFilter<K, V>(K key, V value);
    public static class Extensions
    {
        public static List<K> FilterBy<K, V>(
        this Dictionary<K, V> items,
        KeyValueFilter<K, V> filter)
            {
                var result = new List<K>();

                foreach (KeyValuePair<K, V> element in items)
                {
                    if (filter(element.Key, element.Value))
                        result.Add(element.Key);
                }
                return result;
            }

        public static List<T> Append<T>(this List<T> a, List<T> b)
        {
            var newList = new List<T>(a);
            newList.AddRange(b);
            return newList;
        }

        //public static bool Compare(Customer customer1, Customer customer2)
        public static bool Compare(this Customer customer1, Customer customer2)
        {
            if (customer1.CustomerID == customer2.CustomerID &&
                customer1.Name == customer2.Name &&
                customer1.City == customer2.City)
            {
                return true;
            }

            return false;
        }

    }
    public class Store
    {
        public string Name { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return Name + "\t" + City;
        }
    }

    public class Customer
    {
        public int CustomerID { get; private set; }
        public string Name { get; set; }
        public string City { get; set; }
        public Customer(int ID)
        {
            CustomerID = ID;
        }

        public override string ToString()
        {
            return Name + "\t" + City + "\t" + CustomerID;
        }

    }

    class Program
    {
        List<Point> Square = new List<Point>
        {
                new Point { X=0, Y=5 },
                new Point { X=5, Y=5 },
                new Point { X=5, Y=0 },
                new Point { X=0, Y=0 }
        };

        //static void VarTest()
        //{
        //    //    int i = 43;

        //    //    string s = "...This is only a test...";

        //    //    int[] numbers = new int[] { 4, 9, 16 };

        //    //    SortedDictionary<string, List<DateTime>> complex =
        //    //        new SortedDictionary<string, List<DateTime>>();

        //    //the type of the list created was inferred by the type of data provided in the list
        //    var i = 43;

        //    var s = "...This is only a test...";

        //    var numbers = new[] { 4, 9, 16 };

        //    var complex = new SortedDictionary<string, List<DateTime>>();

        //    // var x;  // Error: what type is it?
        //    // var x = { 1, 2, 3 }; // Error: array initalization expression not permitted
        //    // x = new int[] { 1, 2, 3 };

        //    var x = new[] { 1, 2, 3 };



        //}

        static void Query()
        {

            // var stores = CreateStores();
            //foreach (var store in stores.Where(s => s.City == "London"))
            //    Console.WriteLine(store);


            //IEnumerable<Store> results = from s in stores
            //                             where s.City == "London"
            //                             select s;

            //foreach (var s in results)
            //    Console.WriteLine(s);

            //var numLondon = stores.Count(s => s.City == "London");
            //Console.WriteLine("There are {0} stores in London. ", numLondon);

            //foreach (var c in CreateCustomers())
            //{
            //    var customerStores = new            //Anonymous Type Creation:
            //    {                                   //Mouse over the var in this
            //        c.CustomerID,                   //statement to see the type
            //        c.City,

            //        //CustomerID = c.CustomerID,      //statement to see the type
            //        //City = c.City,
            //        CustomerName = c.Name,
            //        Stores = from s in CreateStores()
            //                 where s.City == c.City
            //                 select s
            //    };

            //    Console.WriteLine("{0}\t{1}",
            //  customerStores.City, customerStores.CustomerName);

            //    foreach (var store in customerStores.Stores)
            //        Console.WriteLine("\t<{0}>", store.Name);
            //}

            //var results = from c in CreateCustomers()
            //              select new
            //              {
            //                  c.CustomerID,
            //                  c.City,
            //                  CustomerName = c.Name,
            //                  Stores = CreateStores().Where(s => s.City == c.City)
            //              };

            //foreach (var result in results)
            //{
            //    Console.WriteLine("{0}\t{1}", result.City, result.CustomerName);
            //    foreach (var store in result.Stores)
            //        Console.WriteLine("\t<{0}>", store.Name);
            //}

            //var results = from c in CreateCustomers()
            //              join s in CreateStores() on c.City equals s.City
            //              select new
            //              {
            //                  CustomerName = c.Name,
            //                  StoreName = s.Name,
            //                  c.City,
            //              };

            //foreach (var r in results)
            //    Console.WriteLine("{0}\t{1}\t{2}",
            //        r.City, r.CustomerName, r.StoreName);

            var results = from c in CreateCustomers()
                          join s in CreateStores() on c.City equals s.City
                          group s by c.Name into g
                          //let count = g.Count()
                          //orderby count ascending
                          select new { CustomerName = g.Key, Count = g.Count() };

            foreach (var r in results)
                Console.WriteLine("{0}\t{1}", r.CustomerName, r.Count);

        }

        static void Main(string[] args)
        {
            //List<Customer> customers = CreateCustomers();
            //var customers = CreateCustomers();

            //Console.WriteLine("Customers:\n");
            //foreach (Customer c in customers)
            //    Console.WriteLine(c);

            //Customer c = new Customer(1) { Name = "Maria Anders", City = "Berlin" };

            //Console.WriteLine(c);

            //var customers = CreateCustomers();
            //var addedCustomers = new List<Customer>
            //{
            //    new Customer(9)  { Name = "Paolo Accorti", City = "Torino" },
            //    new Customer(10) { Name = "Diego Roel", City = "Madrid" }
            //};

            // var updatedCustomers = customers.Append(addedCustomers);



            //var newCustomer = new Customer(10)
            //{
            //    Name = "Diego Roel",
            //    City = "Madrid"
            //};

            // foreach (var c in customers)
            //foreach (var c in updatedCustomers)
            //{
            //    // if (Extensions.Compare(newCustomer, c))
            //        if (newCustomer.Compare(c))
            //        {
            //        Console.WriteLine("The new customer was already in the list");
            //        return;
            //    }
            //}

            // Console.WriteLine("The new customer was not in the list");

            //var customers = CreateCustomers();
            //var customerDictionary = new Dictionary<Customer, string>();

            //foreach (var c in customers)
            //    customerDictionary.Add(c, c.Name.Split(' ')[1]);

            //var matches = customerDictionary.FilterBy(
            //    (customer, lastName) => lastName.StartsWith("A"));
            ////The above line runs the query  
            //Console.WriteLine("Number of Matches: {0}", matches.Count);


            //foreach (var c in FindCustomersByCity(customers, "London"))
            //    Console.WriteLine(c);

            //Expression<Func<int, bool>> filter = n => (n * 3) < 5;

            //BinaryExpression lt = (BinaryExpression)filter.Body;
            //BinaryExpression mult = (BinaryExpression)lt.Left;
            //ParameterExpression en = (ParameterExpression)mult.Left;
            //ConstantExpression three = (ConstantExpression)mult.Right;
            //ConstantExpression five = (ConstantExpression)lt.Right;

            //Console.WriteLine("({0} ({1} {2} {3}) {4})", lt.NodeType,
            //         mult.NodeType, en.Name, three.Value, five.Value);


            //Func<int, int> addOne = n => n + 1;
            //Console.WriteLine("Result: {0}", addOne(5));

            //Expression<Func<int, int>> addOneExpression = n => n + 1;

            //var addOneFunc = addOneExpression.Compile();
            //Console.WriteLine("Result: {0}", addOneFunc(5));


            Query();

        }
        public static List<Customer> FindCustomersByCity(
        List<Customer> customers,
        string city)
            {
                //return customers.FindAll(
                //    delegate (Customer c) {
                //        return c.City == city;
                //    });
            //return customers.FindAll(c => c.City == city);
            return customers.FindAll((Customer c) => c.City == city);
        }
        static List<Store> CreateStores()
        {
                    return new List<Store>
          {
            new Store { Name = "Jim’s Hardware",    City = "Berlin" },
            new Store { Name = "John’s Books",  City = "London" },
            new Store { Name = "Lisa’s Flowers",    City = "Torino" },
            new Store { Name = "Dana’s Hardware",   City = "London" },
            new Store { Name = "Tim’s Pets",    City = "Portland" },
            new Store { Name = "Scott’s Books",     City = "London" },
            new Store { Name = "Paula’s Cafe",  City = "Marseille" },
          };
        }

        static List<Customer> CreateCustomers()
        {
            return new List<Customer>
        {
        new Customer(1) { Name = "Maria Anders",     City = "Berlin"    },
        new Customer(2) { Name = "Laurence Lebihan", City = "Marseille" },
        new Customer(3) { Name = "Elizabeth Brown",  City = "London"    },
        new Customer(4) { Name = "Ann Devon",        City = "London"    },
        new Customer(5) { Name = "Paolo Accorti",    City = "Torino"    },
        new Customer(6) { Name = "Fran Wilson",      City = "Portland"  },
        new Customer(7) { Name = "Simon Crowther",   City = "London"    },
        new Customer(8) { Name = "Liz Nixon",        City = "Portland"  }
        };
        }

    }
}
