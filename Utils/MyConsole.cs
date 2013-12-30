using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Utils
{
    class MyConsole
    {
        private static void WriteLine(object value, ConsoleColor color, ConsoleWriter writer)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;

            switch (writer)
            {
                case ConsoleWriter.Realm:
                    Console.WriteLine("Realm:" + value); break;

                case ConsoleWriter.Game:
                    Console.WriteLine("Game: " + value); break;

                default:
                    Console.WriteLine(value); break;
            }

            Console.ForegroundColor = old;
        }

        public static void WriteLine(object value, ConsoleType type, ConsoleWriter writer)
        {
            switch (type)
            {
                case ConsoleType.Info:
                    WriteLine("  [INFO] " + value, ConsoleColor.Yellow, writer); break;

                case ConsoleType.Error:
                    WriteLine("  [ERROR] " + value, ConsoleColor.Red, writer); break;
                    
                case ConsoleType.Connect:
                    if (!GlobalConfig.Debug) return;
                    WriteLine("    [CONNECTION] " + value, ConsoleColor.Cyan, writer); break;

                case ConsoleType.Disconnect:
                    if (!GlobalConfig.Debug) return;
                    WriteLine("    [DISCONNNECTION] " + value, ConsoleColor.DarkCyan, writer); break;

                case ConsoleType.Receive:
                    if (!GlobalConfig.Debug) return;
                    WriteLine("      [RECV] " + value, ConsoleColor.Green, writer); break;

                case ConsoleType.Send:
                    if (!GlobalConfig.Debug) return;
                    WriteLine("      [SEND] " + value, ConsoleColor.DarkGreen, writer); break;

                default:
                    WriteLine(value, ConsoleColor.White, writer); break;
            }
        }

        public static void WriteLine(Exception ex, ConsoleWriter writer)
        {
            WriteLine(ex.Message + "\n" + ex.StackTrace, ConsoleType.Error, writer);
        }
    }
}
