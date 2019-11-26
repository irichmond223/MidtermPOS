using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace FreshFarms
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> categoryCode = new List<string>()
            {
            "Produce", "Meat", "Dairy"
            };

            List<Product> productList = new List<Product>()
            {
                {new Product("Potatoes", categoryCode[0], "1lb, Russels", 0.79) },
                {new Product("Onions", categoryCode[0], "1lb, White", 0.90) },
                {new Product("Cabbage", categoryCode[0], "1lb Green", 2.07) },
                {new Product("Tomato", categoryCode[0], "1lb Roma", 0.29) },
                {new Product("Cucumber", categoryCode[0], "1ct Fresh", 0.50) },
                {new Product("Turkey", categoryCode[1], "1lb It's natural ground turkey 93% lean.", 4.49) },
                {new Product("Chicken", categoryCode[1], "1lb It's fresh 100% natural boneless and skinless chicken breast.", 7.05) },
                {new Product("Bacon", categoryCode[1], "16oz slow smoked and hand-trimmed from the finest cuts of pork.", 7.05) },
                {new Product("Sausage", categoryCode[1], "14oz Handcrafted with natural spices and only the finest cuts of meat.", 2.50) },
                {new Product("Pork", categoryCode[1], "1lb Boneless Loin Chops, 3 chops per pack.", 6.14) },
                {new Product("Yogurt", categoryCode[2], "5oz tarts with simple, all natural, non-GMO ingredients.", 1.49) },
                {new Product("Cream Cheese", categoryCode[2], "8oz Cream Cheese always starts with fresh milk and real cream.", 1.67) },
                {new Product("Milk", categoryCode[2], "1/2 gal Enjoy organic 2% reduced fat milk from cows raised without antibiotics.", 2.99) },
                {new Product("Coffee Creamer", categoryCode[2], "28 fl oz creamer with the rich flavors of cinnamon streusel.", 4.99) },
                {new Product("Cheese Slices", categoryCode[2], "8oz Distinctive for its orange rind and mild flavor.", 3.7) },

            };

            List<Product> byCategory = new List<Product>();

            //Calling method from Product class to write to text file
            Product newProduct = new Product();
            newProduct.ProductToFile(productList);

            productList.Sort((a, b) => a.Name.CompareTo(b.Name));

            Console.WriteLine("Welcome to the Fresh Farms Store!");
            Console.WriteLine("What category are you interested in? 1. Produce, 2. Meat, 3. Dairy");

            //for (int i = 0; i < productList.Count; i++)
            //{
            //    Console.WriteLine($"{i + 1}. {productList[i].Name}");
            //    tw.WriteLine($"{i + 1}. {productList[i].Name}");
            //}

            // CategoryList();
            string stringUserInput = Console.ReadLine();
            int intUserInput = Validator.ValidateNum(stringUserInput, productList);


            foreach (Product c in productList)
            {
                if (c.Category == categoryCode[intUserInput])
                {
                    byCategory.Add(c);
                    Console.WriteLine(c.Name);
                }

            }

            Console.WriteLine("Which item would you like to purchase?");
            string stringUserInputTwo = Console.ReadLine();
            int intUserInputTwo= Validator.ValidateNum(stringUserInput, productList);
            //foreach (Product movie in productList)
            //{
            //    if (movie.Name == categoryCode[intUserInputTwo])
            //    {
            //        Console.WriteLine(movie.Name);
            //    }
            //}

            foreach (Product n in byCategory)
            {
                Console.WriteLine(n.Name);
            }

            StreamReader sr = new StreamReader(@"C:\Users\ilona\source\repos\FreshFarms\FreshFarms\Product.txt");
            List<string> tempList = new List<string>();

            string line = "";

            while (line != null)
            {
                //some coding
                line = sr.ReadLine();
                if (line != null)
                {
                    tempList.Add(line);
                }
            }

            sr.Close();


        }

        public static string Input()
        {
            string input = Console.ReadLine();
            return input;
        }


    }
}


//POS TERMINAL(That stands for Point-of-Sale, but what you think of your project is up to you.) 
//Write a cash register or self-service terminal for some kind of retail location.
//Obvious choices include a small store, a coffee shop, or a fast food restaurant. 
//Your solution must include some kind of a product class with a name, category, description, and price for each item. 
//12 items minimum; they must be stored in a text file your program reads in. 
//Present a menu to the user and let them choose an item(by number or letter). 

//Validator = format

//Order


//Allow the user to   choose a quantity for the item ordered.Give the user a line total(item price * quantity). VALIDATION - REGEX (VALIDATOR METHOD)
//Either through the menu or a separate question, allow them to   re-display the menu and to complete the purchase. HOW TO REDISPLAY SELECTED?
//Give the subtotal, sales tax, and grand total. 

//PaymentDetails

//Ask for payment type—cash, credit, or check 
//For cash, ask for amount tendered and provide change. CALCULATE CHANGE AND VALIDATE
//For check, get the check number. VALIDATE
//For credit, get the credit card number, expiration, and CVV.  VALIDATE


//At the end, display a receipt with all items ordered, subtotal, grand total, and appropriate payment info. 
//Return to the original menu for a new order. (Hint: you’ll want an array or ArrayList to keep track of what’s been ordered!) Optional enhancements: 
//(Moderate) Include an option to add to the product list, which then outputs to the product file. POSSIBLY REMOVE as well?


//(Hard) Create a full GUI.