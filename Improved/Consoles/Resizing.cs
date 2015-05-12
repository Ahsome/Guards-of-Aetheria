using static System.Console;
namespace Improved.Consoles{
    public static class Resizing{
        public static void Maximise(){
            WindowHeight=BufferHeight=LargestWindowHeight;
            WindowWidth=BufferWidth=LargestWindowWidth;
            //TODO: pinvoke window position
        }
        public static void SetFont() {}
    }
}
