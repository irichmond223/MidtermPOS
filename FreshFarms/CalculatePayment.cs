using System;
using System.Collections.Generic;
using System.Text;

namespace FreshFarms
{
    class CalculatePayment
    {
        //takes in users input for quantity
        public static int Quantity(int quantity)
        {
            return quantity;
        }
        //calculates all items selected and multiplies by quantity, returns double Subtotal
        public static double SubTotal(double itemPrice, int quantity)
        {
            double subTotal = Math.Round(itemPrice * quantity, 2);
            return subTotal;
        }
        //takes in subtotal, returns sales tax amount
        public static double SalesTax(double subTotal)
        {
            double salesTax = Math.Round(0.06 * subTotal, 2);
            return salesTax;
        }
        //grand total
        public static double GrandTotal(double subTotal, double salesTax)
        {
            double grandTotal = Math.Round(subTotal + salesTax, 2);
            return grandTotal;
        }
        //a lot of this will eventually have to change I believe. unless we run the customers input through here but...
        //I think the item price at the lease might have to be changed to reflect the price directly from the List<Product>
        //I didn't want to mess with the program file until we were able to get together
        //Anyway, this calls all the methods and then asks the customer if they'd like to repeat the dispaly or quit
        public static void DisplayMenu()
        {
            bool repeat = true;

            while (repeat)
            {
                Console.WriteLine("Enter a qauntity.");
                int customersQuantity = int.Parse(Console.ReadLine());
                int quantity = Quantity(customersQuantity);
                Console.WriteLine($"Quantity: {Quantity(customersQuantity)}");

                Console.WriteLine("Item price?");
                double itemPrice = double.Parse(Console.ReadLine());
                double subTotal = Math.Round(SubTotal(itemPrice, quantity), 2);
                Console.WriteLine($"Subtotal: {0:$}{SubTotal(itemPrice, quantity)}");

                double salesTax = Math.Round(SalesTax(subTotal), 2);
                Console.WriteLine($"Sales tax: {0:$}{SalesTax(subTotal)}");

                Console.WriteLine($"Grand total: {0:$}{GrandTotal(subTotal, salesTax)}");

                Console.WriteLine("Would you like to see the display again? [Y/N]");
                string seeDisplayAgain = Console.ReadLine();

                if (seeDisplayAgain == "N" || seeDisplayAgain == "n")
                {
                    repeat = false;
                }
                else if (seeDisplayAgain == "Y" || seeDisplayAgain == "y")
                {
                    repeat = true;
                }
                else
                {
                    repeat = false;
                }
            }
        }
    }
}
//Allow the user to choose a quantity for the item ordered.Give the user a line total(item price * quantity).
//Either through the menu or a separate question, allow them to re-display the menu and to complete the purchase.
//Give the subtotal, sales tax, and grand total.