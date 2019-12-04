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
                {new Product("Onions", categoryCode[0], "1lb, White", 0.99) },
                {new Product("Cabbage", categoryCode[0], "1lb Green", 2.07) },
                {new Product("Tomato", categoryCode[0], "1lb Roma", 0.29) },
                {new Product("Cucumber", categoryCode[0], "1ct Fresh", 0.54) },
                {new Product("Turkey", categoryCode[1], "1lb It's natural ground turkey 93% lean.", 4.49) },
                {new Product("Chicken", categoryCode[1], "1lb It's fresh 100% natural boneless and skinless chicken breast.", 7.05) },
                {new Product("Bacon", categoryCode[1], "16oz slow smoked and hand-trimmed from the finest cuts of pork.", 7.05) },
                {new Product("Sausage", categoryCode[1], "14oz Handcrafted with natural spices and only the finest cuts of meat.", 2.53) },
                {new Product("Pork", categoryCode[1], "1lb Boneless Loin Chops, 3 chops per pack.", 6.14) },
                {new Product("Yogurt", categoryCode[2], "5oz tarts with simple, all natural, non-GMO ingredients.", 1.49) },
                {new Product("Cream Cheese", categoryCode[2], "8oz Cream Cheese always starts with fresh milk and real cream.", 1.67) },
                {new Product("Milk", categoryCode[2], "1/2 gal Enjoy organic 2% reduced fat milk.", 2.99) },
                {new Product("Coffee Creamer", categoryCode[2], "28 fl oz creamer with the rich flavors of cinnamon streusel.", 4.99) },
                {new Product("Cheese Slices", categoryCode[2], "8oz Distinctive for its orange rind and mild flavor.", 3.99) },

            };

            //List for ordered items in order to track what has been ordered 
            List<Product> orderedProducts = new List<Product>();

            //List to save all items within a category
            List<Product> byCategory = new List<Product>();


            //Calling method from Product class to send to text file
            ProductPOS newProduct = new ProductPOS();
            newProduct.ProductToFile(productList);

            ProductPOS.GetCurrentInventory();
            //Sorting list by name
            productList.Sort((a, b) => a.Name.CompareTo(b.Name));

            Console.WriteLine($"{"Welcome to the Fresh Farms grocery store!",+75}");
            bool repeat = true;
            while (repeat)
            {

                Order.GroceryList(productList);

                Console.WriteLine();



                Order.ProductSelection(productList, orderedProducts);

                Console.WriteLine();


                //Checking for ordered products to ensure the products selected have been saved
                int itemTwo = 0;
                foreach (Product b in orderedProducts)
                {
                    Console.WriteLine($"Product: {b.Name}");
                    Console.WriteLine($"Price: ${b.Price}");
                    itemTwo++;
                }
                Console.WriteLine();
                CalculatePayment payment = new CalculatePayment();
                CalculatePayment.DisplayMenu();

                //Placeholder double received to store return of PaymentOptions
                //double received = PaymentValidation.PaymentOptions();

                Console.WriteLine();
                repeat = Order.Repeater();


            }
            ProductPOS.AddProduct(productList);

            
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

//CalculatePayment

//Allow the user to choose a quantity for the item ordered.Give the user a line total(item price * quantity). VALIDATION - REGEX (VALIDATOR METHOD)
//Either through the menu or a separate question, allow them to   re-display the menu and to complete the purchase. HOW TO REDISPLAY SELECTED?
//Give the subtotal, sales tax, and grand total. 

//PaymentValidator

//Ask for payment type—cash, credit, or check 
//For cash, ask for amount tendered and provide change. CALCULATE CHANGE AND VALIDATE
//For check, get the check number. VALIDATE
//For credit, get the credit card number, expiration, and CVV.  VALIDATE

//Order

//At the end, display a receipt with all items ordered, subtotal, grand total, and appropriate payment info. 
//Return to the original menu for a new order. (Hint: you’ll want an array or ArrayList to keep track of what’s been ordered!) 
//Include an option to add to the product list, which then outputs to the product file


//Optional enhancements: 
//(Moderate) . POSSIBLY REMOVE as well?


//(Hard) Create a full GUI.