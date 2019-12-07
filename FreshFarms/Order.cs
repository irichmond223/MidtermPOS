using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFarms
{
    class Order:Product
    {
        #region overloaded constructor
        //At the end, display a receipt with all items ordered, subtotal, grand total, and appropriate payment info. 

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
        public static void DisplayInventory(List<Product> productList)
        {
            //Creates and displays Column Headers
            Console.WriteLine($"\n{"#",-3} {"Name",-15} {"Category",-15} {"Description",-90} {"Price",-90}\n");

            //Iterate through List to show what is in stock
            int countNum = 0;
            foreach (Product inventory in productList)
            {
                Console.WriteLine($"{countNum + 1,-3} {inventory.Name,-15} {inventory.Category,-15} {inventory.Description,-90} ${inventory.Price,-90}");
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
                Console.WriteLine($"Product: {orderedProducts[index].Name}");
                Console.WriteLine($"Product Information: {orderedProducts[index].Description}");
                Console.WriteLine($"Price: ${orderedProducts[index].Price}");
                Console.WriteLine($"Quantity: {quantities[index]}");
                Console.WriteLine($"Total: ${quantities[index] * orderedProducts[index].Price}");
                Console.WriteLine();
            }
        }
        public static bool AddAnotherOrder()
        {
            bool repeat = true;
            Console.Write("Do you wish to add another product to your order? (y/n): ");
            while (repeat)
            {
                string reply = Console.ReadLine().ToLower();
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
