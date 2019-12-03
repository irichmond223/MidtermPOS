using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

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
                if (string.IsNullOrEmpty(userChoice))
                {
                    Console.WriteLine("Please enter y or n");
                }
                else if (Regex.IsMatch(userChoice.ToLower(), @"(y)|(yes)"))
                {
                    Console.WriteLine("Enter Product Name:");
                    string Name = Validator.TestValidity();

                    Console.WriteLine("Enter Category:");
                    string Category = Validator.TestValidity();

                    Console.WriteLine("Enter Description:");
                    string Description = Validator.TestValidity();

                    Console.WriteLine("Enter Price:");
                    double Price = Validator.ValidateDouble();

                    productList.Add(new Product(Name, Category, Description, Price));
                    int countNum = 1;
                    foreach (Product c in productList)
                    {

                        Console.WriteLine($"{countNum,-3} {c.Name,-15} {c.Category,-15} {c.Description,-70} ${c.Price,-70}");
                        countNum++;
                    }
                }
                else if (Regex.IsMatch(userChoice.ToLower(), @"(n)|(no)"))
                {
                    endProgram = false;
                }
                else
                {
                    Console.WriteLine("Please enter a yes or no");
                }
            }

        }
    }

}

