using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conversionFixedPointDecimal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int curentBase, laterBase;
            string num;
            num = Console.ReadLine();
            curentBase = int.Parse(Console.ReadLine());
            laterBase = int.Parse(Console.ReadLine());

            if (curentBase == laterBase) Console.WriteLine(num);
            else
            {
                if (curentBase != 10 && laterBase == 10) Console.WriteLine(ConversionToBase10(curentBase, num));
                else
                {
                    double base10Num = ConversionToBase10(curentBase, num);
                    Console.WriteLine(ConversionToLaterBase(laterBase, base10Num));
                }
            }
        }

        private static double ConversionToBase10(int curentBase, string num)
        {
            string[] parts = num.Split('.');
            string integerPart = parts[0];
            string fractionalPart = parts[1];

            double integerPartResult = 0;
            for (int i = 0; i < integerPart.Length; i += 1)
                integerPartResult = integerPartResult + (CharToDigit(integerPart[i]) * toPow(curentBase, integerPart.Length - i - 1));

            double fractionalPartResult = 0;
            for (int i = 0; i < fractionalPart.Length; i += 1)
                fractionalPartResult = fractionalPartResult + (CharToDigit(fractionalPart[i]) * toPow(curentBase, -(i + 1)));

            return integerPartResult + fractionalPartResult;
        }

        private static string ConversionToLaterBase(int laterBase, double num)
        {
            int integerValue = (int)num;
            string result = "";

            while (integerValue > 0)
            {
                result = DigitToChar(integerValue % laterBase) + result;
                integerValue /= laterBase;
            }

            double fractionalPart = num - (int)num;
            if (fractionalPart > 0)
            {
                result += ".";

                for (int i = 0; fractionalPart > 0 && i < 10; i++)
                {
                    fractionalPart *= laterBase;
                    int digit = (int)fractionalPart;
                    result += digit.ToString();
                    fractionalPart -= digit;
                }
            }
            return result;
        }
        private static double toPow(int num, int power)
        {
            double result = 1;
            int powerTo = Math.Abs(power);
            while (powerTo > 0)
            {
                result *= num;
                powerTo -= 1;
            }
            if (power >= 0) return result;

            return (1 / result);
        }

        private static int CharToDigit(char c)
        {
            if (char.IsDigit(c))
            {
                return int.Parse(c.ToString());
            }
            else
            {
                return c - 'A' + 10;
            }
        }

        private static char DigitToChar(int digit)
        {
            if (digit < 10)
            {
                return (char)(digit + '0');
            }
            else
            {
                return (char)(digit - 10 + 'A');
            }
        }
    }
}