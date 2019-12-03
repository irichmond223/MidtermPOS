using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFarms
{
    class Order:Product
    {
        //At the end, display a receipt with all items ordered, subtotal, grand total, and appropriate payment info. 

           //this overloaded constructor is used when building objects from our CSV file
        public Order(string name, string category, string description, double price)//pass in the necessary values
        {
            Name = name;
            Category = category;
            Description = description;
            Price = price;
        }

      
        public static bool Repeater()
        {
            bool repeat = true;
            Console.WriteLine("Do you wish to add another order? (y) or (n)");
            while (repeat)
            {
                string reply = Console.ReadLine().ToLower();
                if (string.IsNullOrEmpty(reply))
                {
                    Console.WriteLine("Please enter y or n");
                }
                else if (Regex.IsMatch(reply.ToLower(), @"(y)|(yes)"))
                {
                    repeat = false;
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
            return true;
        }

        public static void GroceryList(List<Product> productList)
        {
            //Columns Headers

            Console.WriteLine($"\n{"#",-3} {"Name",-15} {"Category",-15} {"Description",-70} {"Price",-70}\n");

            //Iterate through List to show what is in stock
            int countNum = 1;
            foreach (Product c in productList)
            {

                //byCategory.Add(c);

                Console.WriteLine($"{countNum,-3} {c.Name,-15} {c.Category,-15} {c.Description,-70} ${c.Price,-70}");
                countNum++;
            }
        }

        public static void ProductSelection(List<Product> productList, List<Product> orderedProducts)
        {
            //Asks user to select a product by number
            Console.WriteLine("Which product would you like to purchase?");
            string stringUserInput = Console.ReadLine();
            int intUserInput = Validator.ValidateIndex(stringUserInput, productList);

            //As long as user chooses numbers over 0 and enters a number without exceeding the list index
            if (intUserInput > 0 && intUserInput <= productList.Count)
            {

                Console.WriteLine($"Product: {productList[intUserInput - 1].Name}");
                Console.WriteLine($"Price: ${productList[intUserInput - 1].Price}");
                Console.WriteLine($"Product Information: {productList[intUserInput - 1].Description}");
                orderedProducts.Add(productList[intUserInput - 1]);
            }
        }
    }
}
