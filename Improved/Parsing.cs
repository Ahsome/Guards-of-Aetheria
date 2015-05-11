using System.Linq;

namespace Improved {
    public static class Parsing {
        //public enum Base : byte { Binary = 2, Octal = 8, Decimal = 10, Hexadecimal = 16 }

        /// <summary>
        /// Converts a string guaranteed to be an integer to an integer.
        /// </summary>
        /// <param name="s">The string to be parsed</param>
        /// <returns>The parsed integer</returns>
        public static int ToNum(this string s) { return s.Aggregate(0,(c,t) => 10*c+(t-48)); }
        //TODO: advanced base int parse, accounting for all possibilities
        /* public static int IntParse(this string s, Base @base = Base.Decimal) {
            int sign = 1;
            if (s[0] == '-') { sign = -1; s = s.Remove(0, 1); }
            return sign * s.Aggregate(0, (current, t) => (int) @base * current + (Char.IsLetter(t) ? (t - 55) : (Char.IsNumber(t) ? (t - 48) : 0)));
        }*/
    }
}