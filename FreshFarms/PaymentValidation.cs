using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFarms
{
    class PaymentValidation
    {
        //takes in the total payment
        //displays total payment, amount paid, change
        //no return
        public static void CashPayment(double totalCost)
        {
            double received, change;

            Console.WriteLine("Cash payment option selected.");

            Console.Write("Please enter cash amount received: ");
            received = Math.Round(Validator.ValidateDouble(), 2);

            change = Math.Round(received - totalCost);

            Console.WriteLine($"Amount owed: {totalCost}");
            Console.WriteLine($"Amount paid: {received}");
            Console.WriteLine($"Change: {change}");
        }

        //takes in total cost
        //validates check info through regex
        //displays total cost, amount paid, change
        //no return
        public static void CheckPayment(double totalCost)
        {
            double received, change;

            Console.WriteLine("Check payment option selected.");

            Console.Write("Please enter cash amount written: ");
            received = Math.Round(Validator.ValidateDouble(), 2);

            //9 digits, first digit only from 0-3
            Console.Write("Please enter the routing number (9 digits): ");
            RegexValidate(@"(^[0-3]{1}[0-9]{8}$)");

            //10-12 digits
            Console.Write("Please enter the account number (10-12 digits): ");
            RegexValidate(@"(^[0-9]{10,12}$)");

            //4 digits
            Console.Write("Please enter the check number (4 digits): ");
            RegexValidate(@"(^[0-9]{4}$)");

            change = Math.Round(received - totalCost);

            Console.WriteLine($"Amount owed: {totalCost}");
            Console.WriteLine($"Amount paid: {received}");
            Console.WriteLine($"Change: {change}");
        }

        //takes in total cost
        //validates card number using regex (3 types)
        //asks for cash back
        //displays total cost, cash back, amount paid
        //no return
        public static void CardPayment(double totalCost)
        {
            string cashBackSelect;
            double received, cashBack = 0.00;

            Console.WriteLine("Card payment option selected.");
            Console.WriteLine("Please be aware that only Mastercard, Discover, and American Express are accepted at this time.");

            //Mastercard: first digit 5, 16 digits long
            //American Express: first digit 3, second digit 4 or 7, 15 digits long
            //Discover: first digit 6, 16 digits long
            Console.Write("Please enter the card number: ");
            RegexValidate(@"(^[5]{1}[0-9]{15}$|^[3]{1}[4,7]{1}[0-9]{13}$|^[6]{1}[0-9]{15}$)");

            Console.Write("Cash back requested? (y/n): ");
            cashBackSelect = Console.ReadLine().ToLower();

            while (cashBackSelect != "y" && cashBackSelect != "n")
            {
                Console.Write("Invalid input. Please select 'y' or 'n': ");
                cashBackSelect = Console.ReadLine().ToLower();
            }

            if (cashBackSelect == "y")
            {
                Console.Write("Please write cash back amount: ");
                cashBack = Validator.ValidateDouble();
            }

            received = totalCost + cashBack;

            Console.WriteLine($"Amount owed: {totalCost}");
            Console.WriteLine($"Cash back: {cashBack}");
            Console.WriteLine($"Amount paid: {received}");
        }

        //takes in a string for a regex expression
        //asks for input
        //returns input string if successfully matched
        public static string RegexValidate(string regex)
        {
            string input = Console.ReadLine();

            while (!Regex.Match(input, regex).Success)
            {
                Console.Write("Please enter a valid number: ");
                input = Console.ReadLine();
            }

            return input;
        }
    }
}
