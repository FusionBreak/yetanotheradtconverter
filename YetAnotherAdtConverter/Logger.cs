using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter
{
    class Logger
    {
        public static void log(string text, Type type, string notice = "")
        {
            Console.ResetColor();
            
            switch (type)
            {
                case Type.READ:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("READ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    break;
                case Type.WRITE:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("WRITE");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    break;
                case Type.CONVERT:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("CONVERT");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    break;
                case Type.LEVEL1:
                    Console.Write(" -> ");
                    break;
                case Type.LEVEL2:
                    Console.Write(" ->  -> ");
                    break;
                case Type.INFO:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Info");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    break;
                case Type.WARNING:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("WARNING");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    break;
                case Type.ERROR:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ERROR");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    break;
            }

            

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(notice);

            Console.Write("\n");
            Console.ResetColor();
        }

        public static void hr()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();
        }

        public enum Type
        {
            READ,
            WRITE,
            CONVERT,
            LEVEL1,
            LEVEL2,
            INFO,
            WARNING,
            ERROR
        }
    }    
}
