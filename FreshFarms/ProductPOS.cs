using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFarms
{
    class ProductPOS
    {
        #region DisplayMain
        public static void DisplayMain()
        {
            //Greets the user
            DisplayGreet();

            //Holds Main list
            List<Product> productList = new List<Product>();

            //List for ordered items in order to track what has been selected 
            List<Product> orderedProducts = new List<Product>();

            //Holds the inputed item quantities
            List<int> quantities = new List<int>();

            bool repeat = true, repeatTwo = true, repeatThree = true, mainRepeat = true;

            while (mainRepeat)
            {
                do
                {
                    //Holds inventory
                    productList = GroceryList();

                    //Creates headers and displays inventory 
                    Order.DisplayInventory(productList);

                    Console.WriteLine();
                    //Asks for a user to select a product to purchase
                    Order.GetProductSelection(productList, orderedProducts);

                    //Adds user input to a quantities list
                    Console.Write("Please enter a quantity: ");
                    quantities.Add(Validator.ValidateNum(Console.ReadLine()));

                    //Displays specific info for the user to see what has been selected so far
                    Order.DisplayProductSelection(orderedProducts, quantities);

                    Console.WriteLine();
                    //Asks user if they would like to select another product to purchase
                    repeat = Order.AddAnotherOrder();

                    //Writes to the Inventory.txt
                    TextFile.WritesTextFile(productList);

                    //Reads from the Inventory.txt
                    TextFile.ReadsTextFile();
                }
                while (repeat);
                do
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Order Summary:");

                    Console.WriteLine();
                    Console.WriteLine("*************************************************");

                    double subTotal = DisplaySubTotal(orderedProducts, quantities);

                    double salesTax = DisplaySalesTax(subTotal);
                    double grandTotal = DisplayGrandTotal(subTotal, salesTax);
                    Console.WriteLine("*************************************************");
                    Console.WriteLine();

                    string paymentSelection = Payment.PaymentOptions();
                    double cashReceived = Payment.ProcessPayment(grandTotal, paymentSelection);
                    string paymentType = GetPaymentType(paymentSelection);

                    Console.Write("Would you like to confirm your order? (y/n): ");
                    string receipt = Validator.TestStringValidity();
                    Console.WriteLine();

                    if (receipt == "y" || receipt.ToLower() == "Y")
                    {
                        DisplayReceipt(orderedProducts, quantities, subTotal, grandTotal, paymentType, cashReceived);
                        repeatTwo = false;
                        repeatThree = false;
                        mainRepeat = false;
                    }
                    else
                    {
                        repeatTwo = false;
                    }
                } while (repeatTwo);
                while (repeatThree)
                {
                    //Displays options for selecting a new order or exiting the program
                    DisplayOptions();

                    int decision = Validator.ValidateInt();

                    if (decision == 1)
                    {
                        //Clears the previous selections and starts a new order
                        orderedProducts.Clear();
                        repeatThree = false;
                        mainRepeat = true;
                    }
                    else if (decision == 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Come back soon!");
                        repeatThree = false;
                        mainRepeat = false;
                    }
                }
            }
        }


        #endregion

        #region Methods

        public static void DisplayGreet()
        {
            Console.WriteLine($"{"Welcome to the Fresh Farms grocery store!",+75}");
        }
        public static List<Product> GroceryList()
        {
            List<Product> productList = new List<Product>()
            {
                {new Product("Potatoes", "Produce", "1lb potatoes are best when sliced, baked, and served with your favorite toppings.", 0.41) },
                {new Product("Onions", "Produce", "1lb versatile onions can add an extra layer of flavor to your favorite dishes.", 1.09) },
                {new Product("Cabbage", "Produce", "1lb a great compliment to fish tacos! The large leaves can also be used for cabbage rolls.", 2.07) },
                {new Product("Tomato", "Produce", "1lb tomatoes add amazing depth to a variety of dishes with ease.", 0.29) },
                {new Product("Cucumber", "Produce", "1ct deliciously crunchy cucumbers bring a twinkle to the eye of anyone who beholds them.", 0.54) },
                {new Product("Turkey", "Meat", "1lb all-natural with only 8 grams of fat per serving and no gluten.", 4.49) },
                {new Product("Chicken", "Meat", "1lb It's fresh 100% natural boneless and skinless chicken breast.", 7.05) },
                {new Product("Bacon", "Meat", "16oz slow smoked and hand-trimmed from the finest cuts of pork.", 7.05) },
                {new Product("Sausage", "Meat", "14oz handcrafted with natural spices and only the finest cuts of meat.", 2.53) },
                {new Product("Pork", "Meat", "1lb our pork chops are full of savory meat that falls off the bone as you eat.", 6.14) },
                {new Product("Yogurt", "Dairy", "5oz each spoonful is a smooth and creamy escape from ordinary.", 1.49) },
                {new Product("Cream Cheese", "Dairy", "8oz cream Cheese always starts with fresh milk and real cream.", 1.67) },
                {new Product("Milk", "Dairy", "1/2 gal organic milk contains no antibiotics, synthetic hormones or GMOs.", 2.99) },
                {new Product("Coffee Creamer", "Dairy", "28 fl oz creamer with the rich flavors of cinnamon streusel.", 4.99) },
                {new Product("Cheese Slices", "Dairy", "8oz delicious natural cheeses blended together to create a whole new flavor experience.", 3.99) },

            };
            productList.Sort((a, b) => a.Name.CompareTo(b.Name));
            return productList;
        }
        public static string GetPaymentType(string selection)
        {
            if (selection == "1")
            {
                return "Cash";
            }
            else if (selection == "2")
            {
                return "Check";
            }
            else if (selection == "3")
            {
                return "Card";
            }
            else
            {
                return "Invalid Payment Type";
            }
        }

        public static void DisplayOptions()
        {
            Console.WriteLine("Choose from the options below:");
            Console.WriteLine();
            Console.WriteLine("1. Would you like to start a new order?");
            Console.WriteLine("2. Would you like to exit the program?");
            Console.WriteLine();
            Console.Write("Please enter your selection:");
        }

        #region DisplayCalculations
        public static void DisplayTotals(double subTotal, List<Product> orderedProducts, List<int> quantities, double salesTax, double grandTotal)
        {
            Console.WriteLine("Order Summary:");

            Console.WriteLine("*************************************************");

            subTotal = Math.Round(Calculations.GetSubTotal(orderedProducts, quantities), 2);

            Console.WriteLine($"The SubTotal of all ordered items is: {subTotal.ToString("C", CultureInfo.CurrentCulture)}");

            salesTax = Calculations.GetSalesTax(subTotal);

            Console.WriteLine($"The sales tax is: {salesTax.ToString("C", CultureInfo.CurrentCulture)}");

            grandTotal = Calculations.GetGrandTotal(subTotal, salesTax);

            Console.WriteLine($"The grand total is: {grandTotal.ToString("C", CultureInfo.CurrentCulture)}");
        }
        public static double DisplayGrandTotal(double subTotal, double salesTax)
        {
            double grandTotal = Calculations.GetGrandTotal(subTotal, salesTax);
            Console.WriteLine($"The grand total is: {grandTotal.ToString("C", CultureInfo.CurrentCulture)}");
            return grandTotal;
        }
        public static double DisplaySubTotal(List<Product> orderedProducts, List<int> quantities)
        {
            double subTotal = Math.Round(Calculations.GetSubTotal(orderedProducts, quantities), 2);
            Console.WriteLine($"The SubTotal of all ordered items is: {subTotal.ToString("C", CultureInfo.CurrentCulture)}");
            return subTotal;
        }

        public static double DisplaySalesTax(double subTotal)
        {
            double salesTax = Calculations.GetSalesTax(subTotal);

            Console.WriteLine($"The sales tax is: {salesTax.ToString("C", CultureInfo.CurrentCulture)}");
            return salesTax;
        }

        public static void DisplayReceipt(List<Product> orderedProducts, List<int> quantities, double subTotal, double grandTotal, string paymentType, double cashReceived)
        {
            Console.WriteLine();
            Console.WriteLine("Your Total Order:");
            Console.WriteLine();

            for (int index = 0; index < orderedProducts.Count; index++)
            {
                Console.WriteLine("*********************************************");
                Console.WriteLine($"Product: {orderedProducts[index].Name}");
                Console.WriteLine($"Price: ${orderedProducts[index].Price,-10} Quantity: {quantities[index],-5} Total: ${quantities[index] * orderedProducts[index].Price}");
                Console.WriteLine();
            }

            Console.WriteLine("*********************************************");
            Console.WriteLine();
            Console.WriteLine($"Subtotal: ${subTotal}");
            Console.WriteLine($"Grand Total: ${grandTotal}");
            Console.WriteLine($"Payment type: {paymentType}");
            Console.WriteLine($"Amount paid: ${cashReceived}");
            Console.WriteLine($"Amount owed: ${grandTotal}");
            Console.WriteLine();
            Console.WriteLine("Your order has been placed. Thank you for your purchase!");
        }
        #endregion
        #endregion
    }
}

