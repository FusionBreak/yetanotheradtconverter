using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter
{
    class Logger
    {
        static FileInfo logFile = new FileInfo("log.txt");
        public static void writeToFile(string text)
        {
            List<string> lines = new List<string>();

            if (logFile.Exists)
            {
                lines.AddRange(File.ReadAllLines(logFile.FullName));
            }

            lines.Add(DateTime.Now.ToString() + " " + text);

            File.WriteAllLines(logFile.FullName, lines);
            logFile.Refresh();
        }

        public static void log(string text, Type type, string notice = "")
        {
            string typeString = "";

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
                    typeString = "[READ]";
                    break;
                case Type.WRITE:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("WRITE");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    typeString = "[WRITE]";
                    break;
                case Type.CONVERT:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("CONVERT");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    typeString = "[CONVERT]";
                    break;
                case Type.LEVEL1:
                    Console.Write(" -> ");
                    typeString = " -> ";
                    break;
                case Type.LEVEL2:
                    Console.Write(" ->  -> ");
                    typeString = " ->  -> ";
                    break;
                case Type.INFO:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Info");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    typeString = "[Info]";
                    break;
                case Type.WARNING:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("WARNING");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    typeString = "[WARNING]";
                    break;
                case Type.ERROR:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ERROR");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                    typeString = "[ERROR]";
                    break;
            }

            

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(notice);

            Console.Write("\n");
            Console.ResetColor();

            if(Program.config != null)
            {
                if (Program.config.LogFile)
                {
                    if (notice != "")
                        writeToFile(typeString + " " + text + "  |  " + notice);
                    else
                        writeToFile(typeString + " " + text);
                }
            }
                    

            if (type == Type.ERROR)
            {
                Console.Write("Press any key to close...");
                Console.Beep();
                Console.ReadKey();               
            }
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
