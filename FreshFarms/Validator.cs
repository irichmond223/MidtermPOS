using System;
using System.Collections.Generic;
using System.Text;

namespace FreshFarms
{
    class Validator
    {

        public static int ValidateNum(string input, List<Product> listInput)
        {


            int item = 0;
            bool again = true;
            string test = "";
            while (again)
            {

                try
                {

                    item = int.Parse(input) - 1;
                    test = listInput[item].Name;
                    again = false;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Please enter a number between 1 and {listInput.Count}!");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
            }
            return item;

        }

        public static int ValidateIndex(string input, List<Product> listInput)
        {


            int item = 0;
            bool again = true;
            string test = "";
            while (again)
            {

                try
                {

                    item = int.Parse(input);
                    test = listInput[item].Name;
                    again = false;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Please enter a number between 1 and {listInput.Count}!");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
            }
            return item;

        }

        public static double ValidateDouble()
        {
            double validDouble = 0;
            bool valid;

            do
            {
                valid = true;
                try
                {
                    validDouble = double.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write("Please enter a valid value: ");
                    valid = false;
                }
            } while (!valid);

            return validDouble;
        }

        public static string TestValidity()

        {
            string stringUserInput = "";
            do
            {
                try
                {

                    stringUserInput = Console.ReadLine().ToLower();

                    if (string.IsNullOrEmpty(stringUserInput))
                    {
                        Console.WriteLine("Empty input, please try again.");
                    }
                }
                catch (FormatException) //Used when the user typed in in a wrong format
                {
                    Console.WriteLine("Please use the correct format. Please try again.");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Please use the correct format. Please try again.");
                }
                catch (Exception)
                {
                    Console.WriteLine("An error has occurred. Please try again.");
                }

            } while (string.IsNullOrEmpty(stringUserInput));

            return stringUserInput;
        }

    }
}
