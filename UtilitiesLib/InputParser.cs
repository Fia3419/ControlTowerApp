namespace UtilitiesLib
{
    public static class InputParser
    {
        public static bool TryParseInt(string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        public static bool TryParseDecimal(string input, out decimal result)
        {
            return decimal.TryParse(input, out result);
        }

        public static bool TryParseIntInRange(string input, int lowLimit, int highLimit, out int result)
        {
            if (int.TryParse(input, out result))
            {
                return result >= lowLimit && result <= highLimit;
            }
            return false;
        }

        public static bool TryParseDecimalInRange(string input, decimal lowLimit, decimal highLimit, out decimal result)
        {
            if (decimal.TryParse(input, out result))
            {
                return result >= lowLimit && result <= highLimit;
            }
            return false;
        }
    }
}