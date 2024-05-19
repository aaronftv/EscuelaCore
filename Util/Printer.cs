using static System.Console;

namespace EscuelaCore.Util
{
    public static class Printer
    {
        public static void DrawLine(int len = 10)
        {
            System.Console.WriteLine("".PadLeft(len, '='));
        }

        public static void PressEnter()
        {
            System.Console.WriteLine("Press ENTER to continue...");
        }

        public static void WriteTitle(string title)
        {
            var length = title.Length + 4;
            DrawLine(length);
            System.Console.WriteLine($"| {title} |");
            DrawLine(length);
        }

        public static void Beep(int hz = 2000, int tiempo=500, int cantidad =1)
        {
            while (cantidad-- > 0)
            {
                System.Console.Beep(hz, tiempo);
            }
        }
    }
}