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
            //Greets user
            DisplayGreet();

            //Holds Main list
            List<Product> productList = new List<Product>();

            //List for ordered items in order to track what has been selected 
            List<Product> orderedProducts = new List<Product>();

            //Holds the inputed item quantities
            List<int> quantities = new List<int>();

            bool repeat = true, repeatTwo = true, repeatThree = true;

            while (repeat)
            {
                while (repeatTwo)
                {
                    //Holds inventory
                    productList = GroceryList();

                    //Creates headers and displays inventory 
                    Order.DisplayInventory(productList);
                    
                    Console.WriteLine();

                    //Writes to the Product.txt
                    TextFile.WritesTextFile(productList);

                    //Reads from the Product.txt
                    TextFile.ReadsTextFile();

                    //Asks for a user to select a product to purchase
                    Order.GetProductSelection(productList, orderedProducts);

                    //Adds user input to a quantities list
                    Console.Write("Please enter a quantity: ");
                    quantities.Add(Validator.ValidateNum(Console.ReadLine()));

                    //Displays specific info for the user to see what has been selected so far
                    Order.DisplayProductSelection(orderedProducts, quantities);

                    Console.WriteLine();
                    //Asks user if they would like to select another product to purchase
                    repeatTwo = Order.AddAnotherOrder();
                }
                while (repeatThree)
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

                    Console.Write("Would you like to review your purchase? (y/n): ");
                    string receipt = Validator.TestValidity();
                    Console.WriteLine();

                    if (receipt == "y" || receipt.ToLower() == "Y")
                    {
                        DisplayReceipt(orderedProducts, quantities, subTotal, grandTotal, paymentType, cashReceived);

                        Console.Write("Would you like to confirm your purchase? (y/n):");
                        string confirm = Validator.TestValidity();

                        if (confirm == "y" || confirm.ToLower() == "Y")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Your order has been placed. Thank you for your purchase!");
                            repeatTwo = false;
                            repeatThree = false;
                            repeat = false;
                        }
                    }
                    else if (receipt == "n" || receipt.ToLower() == "N")
                    {
                        repeat = DisplayEmptyCart(subTotal, grandTotal, orderedProducts);
                        repeatThree = false;
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
                {new Product("Potatoes", "Produce", "1lb, Russels", 0.79) },
                {new Product("Onions", "Produce", "1lb, White", 0.99) },
                {new Product("Cabbage", "Produce", "1lb Green", 2.07) },
                {new Product("Tomato", "Produce", "1lb Roma", 0.29) },
                {new Product("Cucumber", "Produce", "1ct Fresh", 0.54) },
                {new Product("Turkey", "Meat", "1lb It's natural ground turkey 93% lean.", 4.49) },
                {new Product("Chicken", "Meat", "1lb It's fresh 100% natural boneless and skinless chicken breast.", 7.05) },
                {new Product("Bacon", "Meat", "16oz slow smoked and hand-trimmed from the finest cuts of pork.", 7.05) },
                {new Product("Sausage", "Meat", "14oz Handcrafted with natural spices and only the finest cuts of meat.", 2.53) },
                {new Product("Pork", "Meat", "1lb Boneless Loin Chops, 3 chops per pack.", 6.14) },
                {new Product("Yogurt", "Dairy", "5oz tarts with simple, all natural, non-GMO ingredients.", 1.49) },
                {new Product("Cream Cheese", "Dairy", "8oz Cream Cheese always starts with fresh milk and real cream.", 1.67) },
                {new Product("Milk", "Dairy", "1/2 gal Enjoy organic 2% reduced fat milk.", 2.99) },
                {new Product("Coffee Creamer", "Dairy", "28 fl oz creamer with the rich flavors of cinnamon streusel.", 4.99) },
                {new Product("Cheese Slices", "Dairy", "8oz Delicious natural cheeses blended together to create a whole new flavor experience.", 3.99) },

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

        public static bool DisplayEmptyCart(double subTotal, double grandTotal, List<Product> orderedProducts)
        {
            Console.Write("Would you like to empty your cart and start over? (y/n):");
            string startOver = Validator.ValidateString();
            bool repeat = true;
            while (repeat)
            {
                if (startOver == "y" || startOver.ToLower() == "Y")
                {
                    subTotal = 0;
                    grandTotal = 0;
                    orderedProducts.Clear();
                    repeat = true;
                    return true;
                }
                else if (startOver == "n" || startOver.ToLower() == "N")
                {
                    Console.WriteLine("Come back soon.");
                    repeat = false;
                    return false;
                }
            }
            return repeat;
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
        }
        #endregion
        #endregion
    }
}

