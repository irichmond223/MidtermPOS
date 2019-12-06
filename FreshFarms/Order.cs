using System;
using System.Collections.Generic;
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
        public static bool Repeater()
        {
            bool repeat = true;
            Console.Write("Do you wish to add another item to your order? (y/n): ");
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
                    Console.WriteLine("Please enter a yes or no");
                }

            }
            return repeat;
        }

        public static void GroceryList(List<Product> productList)
        {
            //Columns Headers

            Console.WriteLine($"\n{"#",-3} {"Name",-15} {"Category",-15} {"Description",-90} {"Price",-90}\n");

            //Iterate through List to show what is in stock
            int countNum = 0;
            foreach (Product inventory in productList)
            {

                //byCategory.Add(c);

                Console.WriteLine($"{countNum + 1,-3} {inventory.Name,-15} {inventory.Category,-15} {inventory.Description,-90} ${inventory.Price,-90}");
                countNum++;
            }
        }

        public static void ProductSelection(List<Product> productList, List<Product> orderedProducts)
        {
            //Asks user to select a product by number
            Console.Write("Which product would you like to purchase? Select by typing a number: ");
            string stringUserInput = Console.ReadLine();

            int intUserInput = Validator.ValidateIndex(stringUserInput, productList);
            orderedProducts.Add(productList[intUserInput]);
        }

        public static void CartDisplay(List<Product> orderedProducts, List<int> quantities)
        {
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
        #endregion
    }
}
