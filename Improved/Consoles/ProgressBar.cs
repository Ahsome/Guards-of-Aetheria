using System;

namespace Improved.Consoles
{
    public class ProgressBar
    {
        public int Initial;
        public int Current;
        public int Maximum;
        public int Width;
        public int Left;
        public int Top;
        public ConsoleColor InitialColour;
        public ConsoleColor NewColour;
        public string Enclosure;

        public ProgressBar() { }

        public ProgressBar(int initial, int current, int maximum, int width, int left = -1, int top = -1, ConsoleColor initialColour = default(ConsoleColor), ConsoleColor newColour = default(ConsoleColor), string enclosure = null)
        {
            Initial = initial;
            Current = current;
            Maximum = maximum;
            Width = width;
            Left = (left < 0 || left >= Console.WindowWidth) ? Console.CursorLeft : left;
            Top = (top < 0 || left >= Console.WindowHeight) ? Console.CursorTop : top;
            InitialColour = (initialColour == default(ConsoleColor)) ? initialColour : ConsoleColor.DarkGray;
            NewColour = (newColour == default(ConsoleColor)) ? newColour : ConsoleColor.Gray;
            Enclosure = enclosure ?? "[]";
        }

        //TODO: multi-row, event trigger, use in spend()

        public void Draw()
        {
            Enclosure[0].WriteAt(Left, Top);
            Enclosure[1].WriteAt(Left + Width + 2, Top, -1);
            Console.BackgroundColor = InitialColour;
            new string(' ', Initial / Maximum * Width).WriteAt(Left + 1, Top, Width);
            Console.BackgroundColor = NewColour;
            new string(' ', (Current - Initial) / Maximum * Width).WriteAt(Left + 1, Top);
        }

        public void Finish()
        {
            Initial = Current;
        }
    }
}