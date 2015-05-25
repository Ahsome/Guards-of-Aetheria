namespace Improved{
    public static class Maths{
        /// <summary>
        ///     Returns the positive modulus of two numbers.
        /// </summary>
        /// <param name="a">The number</param>
        /// <param name="b">The base</param>
        /// <returns>The modulus</returns>
        public static int Mod(this int a,int b) =>(a%=b)<0?a+b:a;
        /// <summary>
        ///     Returns the positive modulus of two numbers, modifying the first number.
        /// </summary>
        /// <param name="a">The number</param>
        /// <param name="b">The base</param>
        public static void RMod(ref int a,int b) {a=a.Mod(b);}
    }
}
