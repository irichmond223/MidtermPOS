using System;
using System.Collections.Generic;
using System.Text;

namespace FreshFarms
{
    class CalculatePayment
    {
        #region Methods
        //takes in users input for quantity
        public static int GetQuantity(int amount)
        {
            return amount;
        }

        //Modified to accept two lists and calculate total by multiplying the item price by quantity and adding together all of the results
        public static double GetSubTotal(List<Product> productList, List<int> quantities)
        {
            double subTotal= 0;

            for (int index = 0; index < productList.Count; index++)
            {
                subTotal += productList[index].Price * quantities[index];
            }

            return subTotal;
        }
        //takes in subtotal, returns sales tax amount
        public static double GetSalesTax(double subTotal)
        {
            double salesTax = Math.Round(0.06 * subTotal, 2);
            return salesTax;
        }
        //grand total
        public static double GetGrandTotal(double subTotal, double salesTax)
        {
            double grandTotal = Math.Round(subTotal + salesTax, 2);
            return grandTotal;
        }
        #endregion
        //a lot of this will eventually have to change I believe. unless we run the customers input through here but...
        //I think the item price at the lease might have to be changed to reflect the price directly from the List<Product>
        //I didn't want to mess with the program file until we were able to get together
        //Anyway, this calls all the methods and then asks the customer if they'd like to repeat the dispaly or quit
        //COMMENTED OUT DUE TO CHANGE IN OTHER METHODS
        //public static void DisplayMenu()
        //{
        //    bool repeat = true;

        //    while (repeat)
        //    {
        //        Console.WriteLine("Enter Quantity.");
        //        string input = Console.ReadLine();
        //        int quantity = CalculatePayment.GetQuantity(Validator.ValidateNum(input));
        //        Console.WriteLine($"Quantity: {quantity}");

        //        Console.WriteLine("Item price?");
        //        double itemPrice = double.Parse(Console.ReadLine());
        //        double subTotal = Math.Round(CalculatePayment.GetSubTotal(itemPrice, quantity), 2);
        //        Console.WriteLine($"Subtotal: {0:$}{CalculatePayment.GetSubTotal(itemPrice, quantity)}");

        //        double salesTax = Math.Round(CalculatePayment.GetSalesTax(subTotal), 2);
        //        Console.WriteLine($"Sales tax: {0:$}{CalculatePayment.GetSalesTax(subTotal)}");

        //        Console.WriteLine($"Grand total: {0:$}{CalculatePayment.GetGrandTotal(subTotal, salesTax)}");

        //        Console.WriteLine("Would you like to see the display again? [Y/N]");
        //        string seeDisplayAgain = Console.ReadLine();

        //        if (seeDisplayAgain == "N" || seeDisplayAgain == "n")
        //        {
        //            repeat = false;
        //        }
        //        else if (seeDisplayAgain == "Y" || seeDisplayAgain == "y")
        //        {
        //            repeat = true;
        //        }
        //        else
        //        {
        //            repeat = false;
        //        }
        //    }
        //}
    }
}
//Allow the user to choose a quantity for the item ordered.Give the user a line total(item price * quantity).
//Either through the menu or a separate question, allow them to re-display the menu and to complete the purchase.
//Give the subtotal, sales tax, and grand total.