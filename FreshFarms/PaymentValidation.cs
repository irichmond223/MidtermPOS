using System;
using System.Collections.Generic;
using System.Text;

namespace FreshFarms
{
    class PaymentValidation
    {
        public static void CashPayment(double totalCost)
        {
            double received, change;

            Console.WriteLine("Cash payment option selected.");
            Console.Write("Please enter cash amount received: ");

            received = Math.Round(Validator.ValidateDouble(), 2);

            change = received - totalCost;

            Console.WriteLine($"Amount owed: {totalCost}");
            Console.WriteLine($"Amount paid: {received}");
            Console.WriteLine($"Change: {change}");
        }

        public static void CheckPayment(double totalCost)
        {
            double received, change;

            Console.WriteLine("Check payment option selected.");
            Console.Write("Please enter routing number: ");
        }
    }
}
