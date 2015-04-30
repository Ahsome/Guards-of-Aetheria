using System.Linq;

namespace Improved
{
    public static class Parsing
    {
        /*public enum Base : byte
        {
            Binary = 2,
            Octal = 8,
            Decimal = 10,
            Hexadecimal = 16
        }*/
        
        /// <summary>
        /// Converts a string guaranteed to be an integer to an integer.
        /// </summary>
        /// <param name="value">The string to be parsed</param>
        /// <returns>The parsed integer</returns>
        public static int ToInt(this string value) { return value.Aggregate(0, (current, t) => 10 * current + (t - 48)); }

        /* public static int IntParse(this string value, Base @base = Base.Decimal)
        {
            int sign = 1;
            if (value[0] == '-') { sign = -1; value = value.Remove(0, 1); }
            return sign * value.Aggregate(0, (current, t) => (int) @base * current + (Char.IsLetter(t) ? (t - 55) : (Char.IsNumber(t) ? (t - 48) : 0)));
        }*/
    }
}