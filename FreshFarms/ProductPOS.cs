using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFarms
{
    class ProductPOS
    {

        //private static List<Product> productList { get; } = new List<Product>();

        //Holds productList 
        public static void GetCurrentInventory()
        {
            StreamReader sr = new StreamReader(@"C:..\..\..\Product.txt");
            List<string> tempList = new List<string>();
            //List<Product> productList = new List<Product>();

            string line = "";


            while (line != null)
            {

                line = sr.ReadLine();
                if (line != null)
                {
                    tempList.Add(line);
                    //productList.Add(line);
                }
            }

            sr.Close();
        }

        public void ProductToFile(List<Product> productList)
        {

            // create a writer and open the file
            TextWriter tw = new StreamWriter(@"C:..\..\..\Product.txt");

            tw.WriteLine($"\n{"#",-3} {"Name",-15} {"Category",-15} {"Description",-70} {"Price",-70}\n");
            for (int i = 1; i < productList.Count; i++)
            {

                tw.WriteLine($"{i,-3} {productList[i].Name,-15} {productList[i].Category,-15} {productList[i].Description,-70} ${productList[i].Price,-70}");
            }

            tw.Close();
        }

        private static void SaveCurrentInventory()
        {
            //private static void SaveCurrentInventory()
            //{
            //    //create new streamwriter object
            //    StreamWriter sw = new StreamWriter(@"C:\Users\ilona\source\repos\C#\Classes\CarLotCOMPLETE\CarLot\CarLotDB.txt");

            //    //iterate through our list of cars and first make CSV string out of the objects data, and then write that data to the CSV text file
            //    foreach (Car car in currentInventory)
            //    {
            //        //check if the object is a NewCar or a UsedCar
            //        if (car is NewCar)
            //        {
            //            //if its a NewCar then we call the method that is in the NewCar class
            //            sw.WriteLine(NewCar.CarToCSV((NewCar)car));
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
        public static void AddProduct(List<Product> productList)
        {
            bool endProgram = true;
            while (endProgram)
            {
                //create new streamwriter object
               // StreamWriter sw = new StreamWriter(@"C:..\..\..\Product.txt");
                StreamWriter sw = File.AppendText(@"C:..\..\..\Product.txt");

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
                       // sw.Write(Name + Environment.NewLine);
                        //sw.Write($"{countNum,-3} {c.Name,-15} {c.Category,-15} {c.Description,-70} ${c.Price,-70}");
                        sw.WriteLine($"{countNum,-3} {c.Name,-15} {c.Category,-15} {c.Description,-70} ${c.Price,-70}");
                        //sw.Write($"{countNum,-3} {c.Name,-15} {c.Category,-15} {c.Description,-70} ${c.Price,-70}");
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
                sw.Close();
            }

        }
    }
}
