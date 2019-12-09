using System;
using System.Collections.Generic;
using System.Text;

namespace FreshFarms
{
    class Calculations
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
    }
}