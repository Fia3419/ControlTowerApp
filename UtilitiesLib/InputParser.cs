namespace UtilitiesLib
{
    /// <summary>
    /// Utility class for parsing various input types. Provides methods for parsing integers and decimals.
    /// </summary>
    public static class InputParser
    {
        /// <summary>
        /// Tries to parse a string into an integer.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed integer result.</param>
        /// <returns>True if the parsing was successful, false otherwise.</returns>
        public static bool TryParseInt(string input, out int result) =>
            int.TryParse(input, out result);

        /// <summary>
        /// Tries to parse a string into a decimal.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed decimal result.</param>
        /// <returns>True if the parsing was successful, false otherwise.</returns>
        public static bool TryParseDecimal(string input, out decimal result) =>
            decimal.TryParse(input, out result);

        /// <summary>
        /// Tries to parse a string into an integer within a specific range.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="lowLimit">The minimum acceptable value.</param>
        /// <param name="highLimit">The maximum acceptable value.</param>
        /// <param name="result">The parsed integer result.</param>
        /// <returns>True if the parsing was successful and within the specified range, false otherwise.</returns>
        public static bool TryParseIntInRange(string input, int lowLimit, int highLimit, out int result)
        {
            if (int.TryParse(input, out result))
            {
                return result >= lowLimit && result <= highLimit;
            }
            return false;
        }

        /// <summary>
        /// Tries to parse a string into a decimal within a specific range.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="lowLimit">The minimum acceptable value.</param>
        /// <param name="highLimit">The maximum acceptable value.</param>
        /// <param name="result">The parsed decimal result.</param>
        /// <returns>True if the parsing was successful and within the specified range, false otherwise.</returns>
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
