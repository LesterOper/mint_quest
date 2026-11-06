using System;

namespace Swimmer.Library
{
    public class LargeNumber
    {
        private static readonly string formatString = "0.#";
        private static readonly string formatString2 = "0";
        private static readonly double maxFractional = 0.99;

        private static readonly string[] latin = {"M", "B", "T", "q", "Q", "s", "S", "O", "N"};
        private static readonly string[] ones = {"U", "D", "!", "@", "#", "$", "%", "^", "&"};

        private static readonly string[] tens =
            {"d", "*", "triginta", "quadraginta", "quinquaginta", "sexaginta", "septuaginta", "octoginta", "nonaginta"};

        private static readonly string[] hundreds =
        {
            "centi", "ducenti", "trecenti", "quadringenti", "quingenti", "sescenti", "septingenti", "octingenti", "nongenti"
        };

        public static string ToString(double rawNumber)
        {
            if (rawNumber < 100.0) return rawNumber.ToString(formatString2);
            if (rawNumber < 1000.0) return rawNumber.ToString(formatString2);

            if (rawNumber < 1000000.0 && rawNumber >= 1000.0)
            {
                rawNumber = rawNumber / 1000;
                if (rawNumber >= 100)
                {
                    return rawNumber.ToString(formatString2) + " K";
                }

                return rawNumber.ToString(formatString) + " K";

            }
            var scientificNotation = ScientificNotation.FromDouble(rawNumber);
            var adjustedExponent = (ushort) (scientificNotation.exponent / 3 - 1);
            var prefix = "";

            if (adjustedExponent < 10)
            {
                prefix = latin[adjustedExponent - 1];
            }
            else
            {
                var hundredsPlace = (ushort) (adjustedExponent / 100);
                var tensPlace = (ushort) (adjustedExponent / 10 % 10);
                var onesPlace = (ushort) (adjustedExponent % 10);
                var onesString = onesPlace > 0 ? ones[onesPlace - 1] : "";
               //ToDo  drawing up abbreviations for numbers over * (10^63)

                var tensString = tensPlace > 0 ? tens[tensPlace - 1] : "";
                var hundredsString = hundredsPlace > 0 ? hundreds[hundredsPlace - 1] : "";
                if ((tensString.Equals("d") && onesString.Equals("") )|| (tensString.Equals("*") && onesString.Equals("")))
                {
                    prefix = tensString;
                }
                else
                {
                    prefix = onesString;
                }
             //   prefix = string.Format("{0}{1}", onesString, tensString);
            }
            var adjustedSignificand = scientificNotation.significand * Math.Pow(10, scientificNotation.exponent % 3);
            var integralPart = Math.Truncate(adjustedSignificand);


            var number = (adjustedSignificand - integralPart > maxFractional
                ? integralPart + maxFractional
                : adjustedSignificand);
            
            if (rawNumber >= 100)
            {
                return string.Format("{0} {1}",
                    number.ToString(formatString2), prefix.TrimEnd('a'));
            }

            return string.Format("{0} {1}",
                number.ToString(formatString), prefix.TrimEnd('a'));

        }
    }

    public struct ScientificNotation
    {
        public ushort exponent;
        public double significand;

        public static ScientificNotation FromDouble(double rawNumber)
        {
            var exponent = (ushort) Math.Log10(rawNumber);
            return new ScientificNotation
            {
                exponent = exponent,
                significand = rawNumber * Math.Pow(0.1, exponent)
            };
        }
    }
}