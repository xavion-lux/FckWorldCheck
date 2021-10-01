using System;

namespace FckWorldCheck
{
    internal class FckLogger
    {
        internal static string Name;
        private static string Time() => "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] ";
        internal static void Init()
        {
            Name = "[" + typeof(FckWorldCheck).Assembly.GetName().Name + "]";
        }

        internal static void Msg(string Msg)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(Time() + Name + " " + Msg);
            Console.ForegroundColor = c;
        }

        internal static void Warn(string Msg)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Time() + Name + " [Warning] " + Msg);
            Console.ForegroundColor = c;
        }

        internal static void Error(string Msg)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Time() + Name + " [Error] " + Msg);
            Console.ForegroundColor = c;
        }

        internal static void Magenta(string Msg)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Time() + Name + " " + Msg);
            Console.ForegroundColor = c;
        }

        internal static void Cyan(string Msg)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Time() + Name + " " + Msg);
            Console.ForegroundColor = c;
        }

        internal static void Green(string Msg)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Time() + Name + " " + Msg);
            Console.ForegroundColor = c;
        }

        internal static void Blue(string Msg)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Time() + Name + " " + Msg);
            Console.ForegroundColor = c;
        }
    }
}
