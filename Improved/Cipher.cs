﻿namespace Improved{
    using System.Linq;
    using System.Text;
    public class Str{
        public const string NonLetters="-'";
        public const string Digits="0123456789";
        public const string Separators=" \t\r\n";
        public const string Lowercase="abcdefghijklmnopqrstuvwxyz";
        public const string Uppercase="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
    public class Cipher{
        public static string Caesar(string s,int rot) =>Vigenere(s,Str.Lowercase[rot].ToString());
        //TODO: case comparison, transposition cipher, hill cipher, pre-vigenere cipher - trisomething - key = lowercase, binary cipher, frequency analysis
        public static string Vigenere(string s,string key){
            var j=-1;
            return s.Aggregate("",(a,b)=>a+(char.IsLetter(b)?Str.Lowercase[(b+key[++j%key.Length])%26]:b));
        } //TODO: one-line-ify, convert letters to alphabet representations
        public static string Transposition(string s,int columns){
            var sb=new StringBuilder(s.Length);
            var letters=new string(s.Where(char.IsLetter).ToArray());
            var columnLength=letters.Length/columns;
            for(int i=0,j=0;i<s.Length;) sb.Append(char.IsLetter(s[i])?letters[j%columnLength/3+j++/columnLength]:s[i]);
            //TODO: test
            return sb.ToString();
        }
    }
}