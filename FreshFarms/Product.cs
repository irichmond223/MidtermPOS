using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FreshFarms
{
    class Product
    {
        private string name;
        private string category;
        private string description;
        private double price;
        // public string categoryCode;
        //public string nameCode;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public Product() { }
        public Product(string name, string category, string description, double price)
        {
            Name = name;
            Category = category;
            Description = description;
            Price = price;
           
        }

        public void ProductToFile(List<Product> productList)
        {
            TextWriter tw = new StreamWriter(@"C:..\..\..\Product.txt");
            tw.WriteLine($"\n{"#",-3} {"Name",-15} {"Category",-15} {"Description",-70} {"Price",-70}\n");
            for (int i = 1; i < productList.Count; i++)
            {
                
                tw.WriteLine($"{i,-3} {productList[i].Name,-15} {productList[i].Category,-15} {productList[i].Description,-70} ${productList[i].Price,-70}");
            }
            
            tw.Close();
        }
        public static void AddProduct(List<Product> productList)
        {
            bool endProgram = true;
            while (endProgram)
            {
                Console.WriteLine("Do you want to add a new item to the list? (y) or (n)");
                string userChoice = Console.ReadLine().ToLower();
                if (userChoice == "y")
                {
                    Console.WriteLine("Enter Product Name:");
                    string Name = Console.ReadLine();

                    Console.WriteLine("Enter Category:");
                    string Category = Console.ReadLine();

                    Console.WriteLine("Enter Description:");
                    string Description = Console.ReadLine();

                    Console.WriteLine("Enter Price:");
                    double Price = double.Parse(Console.ReadLine());

                    productList.Add(new Product(Name, Category, Description,Price));
                    int countNum = 1;
                    foreach (Product c in productList)
                    {

                        //byCategory.Add(c);

                        Console.WriteLine($"{countNum,-3} {c.Name,-15} {c.Category,-15} {c.Description,-70} ${c.Price,-70}");
                        countNum++;
                    }
                }
                else
                {
                    endProgram = false;
                }
            }
        }
        //private static void SaveCurrentInventory(List<Product> productLis)
        //{
        //    //create new streamwriter object
        //    StreamWriter sw = new StreamWriter(@"C:\Users\ilona\source\repos\C#\Classes\CarLotCOMPLETE\CarLot\CarLotDB.txt");

        //    //iterate through our list of cars and first make CSV string out of the objects data, and then write that data to the CSV text file
        //    foreach (Product car in productList)
        //    {
        //        //check if the object is a NewCar or a UsedCar
        //        if (car is Product)
        //        {
        //            //if its a NewCar then we call the method that is in the NewCar class
        //            sw.WriteLine(Product.CarToCSV((NewCar)car));
        //        }
        //        else
        //        {
        //            //if it is a UsedCar then we call then methiod that is in the UsedCar class
        //            sw.WriteLine(UsedCar.CarToCSV((UsedCar)car));
        //        }
        //    }

        //    //closed the connection saving data to the text file
        //    sw.Close();
        //}
    }
}
