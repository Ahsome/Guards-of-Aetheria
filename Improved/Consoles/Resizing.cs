using System;

namespace Improved.Consoles
{
    public static class Resizing
    {
        public static void Maximise()
        {
            Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;
            //TODO: pinvoke window position
        }

        public static void SetFont()
        {
            
        }
    }
}