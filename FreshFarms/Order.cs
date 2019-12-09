using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFarms
{
    class Order:Product
    {
        #region overloaded constructor

        //this overloaded constructor is used when building objects from our CSV file
        public Order(string name, string category, string description, double price)//pass in the necessary values
        {
            Name = name;
            Category = category;
            Description = description;
            Price = price;
        }
        #endregion

        #region Methods
        public static void GetProductSelection(List<Product> productList, List<Product> orderedProducts)
        {
            //Asks user to select a product by number
            Console.Write("Which product would you like to purchase? (please enter in numerical format): ");
            string stringUserInput = Console.ReadLine();

            int intUserInput = Validator.ValidateIndex(stringUserInput, productList);
            orderedProducts.Add(productList[intUserInput]);
        }
        //Creates headers and displays inventory 
        public static void DisplayInventory(List<Product> productList)
        {
            //Creates and displays Column Headers
            Console.WriteLine($"\n{"#",-3} {"Name",-15} {"Category",-15} {"Description",-90} {"Price",-90}\n");

            //Iterate through List to show what is in stock
            int countNum = 0;
            foreach (Product inventory in productList)
            {
                Console.WriteLine($"{countNum + 1,-3} {inventory.Name,-15} {inventory.Category,-15} {inventory.Description,-90} {inventory.Price.ToString("C", CultureInfo.CurrentCulture),-90}");
                countNum++;
            }
    

        }
        public static void DisplayProductSelection(List<Product> orderedProducts, List<int> quantities)
        {

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Your Cart:");
            Console.WriteLine();

            for (int index = 0; index < orderedProducts.Count; index++)
            {
                double totalDollars = (quantities[index] * orderedProducts[index].Price);
                Console.WriteLine($"Product: {orderedProducts[index].Name}");
                Console.WriteLine($"Product Information: {orderedProducts[index].Description}");
                Console.WriteLine($"Price: {orderedProducts[index].Price.ToString("C", CultureInfo.CurrentCulture)}");
                Console.WriteLine($"Quantity: {quantities[index]}");
                Console.WriteLine($"Total: {totalDollars.ToString("C", CultureInfo.CurrentCulture)}");
                Console.WriteLine();

                string path = @"C:..\..\..\Inventory.txt";
                //AppendAllText adds user selection to a file. 
                File.AppendAllLines(path, new[] { $" Ordered Product: {orderedProducts[index].Name}, Price: {orderedProducts[index].Price.ToString("C", CultureInfo.CurrentCulture)}, Quantity: {quantities[index]}, Total: {totalDollars.ToString("C", CultureInfo.CurrentCulture)}" });
            }
        }
        //Asks user if they would like to select another product to purchase
        public static bool AddAnotherOrder()
        {
            bool repeat = true;
            Console.Write("Do you wish to add another product to your order? (y/n): ");
            while (repeat)
            {
                string reply = Validator.ValidateString().ToLower();

                while (reply != "y" && reply != "n")
                {
                    Console.Write("Please enter either y or n: ");
                    reply = Validator.TestStringValidity().ToLower();
                    Console.WriteLine();
                }
                if (string.IsNullOrEmpty(reply))
                {
                    Console.WriteLine("Please enter y or n");
                }
                else if (Regex.IsMatch(reply.ToLower(), @"(y)|(yes)"))
                {
                    repeat = true;
                    return true;
                }
                else if (Regex.IsMatch(reply.ToLower(), @"(n)|(no)"))
                {
                    repeat = false;
                    return false;
                }
                else
                {
                    Console.WriteLine("Please enter y or n");
                }
            }
            return repeat;
        }
        #endregion
    }
}
