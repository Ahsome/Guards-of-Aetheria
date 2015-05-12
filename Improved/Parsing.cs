namespace Improved{
    using System.Linq;
    public static class Parsing{
        //public enum Base : byte { Binary = 2, Octal = 8, Decimal = 10, Hexadecimal = 16 }
        /// <summary>
        ///     Converts a string guaranteed to be an integer to an integer.
        /// </summary>
        /// <param name="s">The string to be parsed</param>
        /// <returns>The parsed integer</returns>
        public static int ToNum(this string s) {return s.Aggregate(0,(c,t)=>10*c+(t-48));}
    }
}
