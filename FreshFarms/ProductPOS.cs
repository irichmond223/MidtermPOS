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
        public static void DisplayMenu()
        {

            Options();
        }


        #region Options
        public static void Options()
        {
            Console.WriteLine($"{"Welcome to the Fresh Farms grocery store!",+75}");


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

            //List for ordered items in order to track what has been ordered 
            List<Product> orderedProducts = new List<Product>();
            //Calling method to send to text file
            


            bool repeat = true;
            bool repeatTwo = true;
            bool repeatThree = true;

            while (repeat)
            {
                while (repeatTwo)
                {

                    Order.GroceryList(productList);

                    Console.WriteLine();
                    //Writes to the Product.txt
                    ProductToFile(productList);
                    //Reads from the Product.txt
                    GetCurrentInventory();

                    Order.ProductSelection(productList, orderedProducts);

                    Console.WriteLine();
                    repeatTwo = Order.Repeater();


                }
                while (repeatThree)
                {
                    //Holds the inputed item quantities
                    List<int> quantities = new List<int>();
                    //Adds user input to a quantities list
                    Console.Write("Please enter a quantity: ");
                    quantities.Add(Validator.ValidateNum(Console.ReadLine()));

                    Console.WriteLine();
                    Console.WriteLine("Order Summary:");
                    Console.WriteLine();
                    double subTotal = Math.Round(CalculatePayment.GetSubTotal(orderedProducts, quantities), 2);
                    Console.WriteLine();
                    Console.WriteLine($"The SubTotal of all ordered items is: {subTotal.ToString("C", CultureInfo.CurrentCulture)}");

                    double salesTax = CalculatePayment.GetSalesTax(subTotal);
                    Console.WriteLine();
                    Console.WriteLine($"The sales tax is: {salesTax.ToString("C", CultureInfo.CurrentCulture)}");

                    double grandTotal = CalculatePayment.GetGrandTotal(subTotal, salesTax);
                    Console.WriteLine();
                    Console.WriteLine($"The grand total is: {grandTotal.ToString("C", CultureInfo.CurrentCulture)}");

                    Console.WriteLine();
                    double cashReturned = PaymentValidation.PaymentOptions(grandTotal);
                
                //COMMENTED OUT FOR TESTING
                //CalculatePayment payment = new CalculatePayment();
                //CalculatePayment.DisplayMenu();

                //Placeholder double received to store return of PaymentOptions
                //double received = PaymentValidation.PaymentOptions();


                //COmmented out for now
                //ProductPOS.AddProduct(productList);

                Console.WriteLine("Would you like to review your purchase? (y) or (n)");
                string receipt = Validator.TestValidity();

                    if (receipt == "y" || receipt.ToLower() == "Y")
                    {
                        int input = 0;
                        int items = 0;
                        foreach (Product b in orderedProducts)
                        {
                            Console.WriteLine("Order Details:");
                            Console.WriteLine();
                            Console.WriteLine($"Product: {b.Name}");
                            Console.WriteLine($"Price: ${b.Price}");
                            Console.WriteLine($"Quantity: {quantities[input]}");
                            Console.WriteLine($"Subtotal: ${subTotal}");
                            Console.WriteLine($"Grand Total: ${grandTotal}");
                            Console.WriteLine($"Payment type: ${cashReturned}");
                            //Still working on getting Payment info to display
                            Console.WriteLine($"Payment type: ");
                            Console.WriteLine($"Amount owed: ");
                            Console.WriteLine($"Amount paid: ");
                            Console.WriteLine($"Change: ");
                            items++;
                        }

                        Console.WriteLine("Would you like to confirm your purchase? (y) or (n)");
                        string confirm = Validator.TestValidity();

                        if (confirm == "y" || confirm.ToLower() == "Y")
                        {
                            Console.WriteLine("Your order has been placed. Thank you for your purchase!");
                            repeatTwo = false;
                            repeatThree = false;
                            repeat = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Would you like to start over? (y) or (n)");
                        string startOver = Validator.TestValidity();

                        if (startOver == "y" || startOver.ToLower() == "Y")
                        {
                            subTotal = 0;
                            grandTotal = 0;
                            orderedProducts.Clear();
                            repeat = true;
                            repeatThree = false;
                            repeatTwo = true;
                           
                        }
                        else if (startOver == "n" || startOver.ToLower() == "N")
                        {
                            Console.WriteLine("Come back soon.");
                            repeatTwo = false;
                            repeatThree = false;
                            repeat = false;
                        }
                        else
                        {
                            Console.WriteLine("Come back soon.");

                            repeatTwo = false;
                            repeat = false;
                        }
                    }
                }
            }
           
           
        }
        #endregion

        #region TextFile
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

        public static void ProductToFile(List<Product> productList)
        {

            // create a writer and open the file
            TextWriter tw = new StreamWriter(@"C:..\..\..\Product.txt");

            tw.WriteLine($"\n{"#",-3} {"Name",-15} {"Category",-15} {"Description",-90} {"Price",-70}\n");
            for (int i = 0; i < productList.Count; i++)
            {

                tw.WriteLine($"{i + 1,-3} {productList[i].Name,-15} {productList[i].Category,-15} {productList[i].Description,-90} ${productList[i].Price,-70}");
            }

            tw.Close();
        }

        #endregion
        

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

        //Calling method from Product class to send to text file
        // ProductPOS newProduct = new ProductPOS();
        //ProductToFile(productList);



        //Checking for ordered products to ensure the products selected have been saved
        //int itemTwo = 0;
        //foreach (Product b in orderedProducts)
        //{
        //    Console.WriteLine($"Product: {b.Name}");
        //    Console.WriteLine($"Price: ${b.Price}");
        //    itemTwo++;
        //}
        //Console.WriteLine();
    }
}
