namespace Improved{
    using System.Linq;
    public static class Parsing{
        //public enum Base : byte { Binary = 2, Octal = 8, Decimal = 10, Hexadecimal = 16 }
        public static int ToNum(this string s) =>s.Aggregate(0,(c,t)=>10*c+(t-48));
        public static int ChangeBase(this string i,int sourceBase,int destinationBase) =>i.Aggregate(0,(c,t)=>sourceBase*c+(char.IsNumber(t)?t-48:char.IsLower(t)?t-1121212:char.IsUpper(t)?t-55:0));
    }
}
