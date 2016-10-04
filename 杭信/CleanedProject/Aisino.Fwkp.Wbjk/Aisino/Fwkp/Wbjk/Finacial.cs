namespace Aisino.Fwkp.Wbjk
{
    using System;
    using System.Runtime.InteropServices;

    public static class Finacial
    {
        public static double Add(double value1, double value2, int digits = 15)
        {
            decimal num = new decimal(value1);
            decimal num2 = new decimal(value2);
            double round = Convert.ToDouble(decimal.Add(num, num2));
            if (digits != 0x10)
            {
                round = GetRound(round, digits);
            }
            return round;
        }

        public static double Div(double value1, double value2, int digits = 15)
        {
            if (value2 == 0.0)
            {
                return 0.0;
            }
            decimal num = new decimal(value1);
            decimal num2 = new decimal(value2);
            double round = Convert.ToDouble(decimal.Divide(num, num2));
            if (digits != 0x10)
            {
                round = GetRound(round, digits);
            }
            return round;
        }

        public static bool Equal(double value1, double value2)
        {
            double num = value1 - value2;
            return (Math.Abs(num) < 1E-15);
        }

        public static bool EqualA(double value1, double value2)
        {
            double num = value1 - value2;
            return (Math.Abs(num) < 0.01);
        }

        public static decimal GetRound(decimal value, int digits = 2)
        {
            return Math.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        public static double GetRound(double value, int digits = 2)
        {
            return Math.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        public static double Mul(double value1, double value2, int digits = 15)
        {
            decimal num = new decimal(value1);
            decimal num2 = new decimal(value2);
            double round = Convert.ToDouble(decimal.Multiply(num, num2));
            if (digits != 0x10)
            {
                round = GetRound(round, digits);
            }
            return round;
        }

        public static double Subtract(double value1, double value2, int digits = 15)
        {
            decimal num = new decimal(value1);
            decimal num2 = new decimal(value2);
            double round = Convert.ToDouble(decimal.Subtract(num, num2));
            if (digits != 0x10)
            {
                round = GetRound(round, digits);
            }
            return round;
        }
    }
}

