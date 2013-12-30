using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Aldos.Global;

namespace Aldos
{
    class Program
    {
        static DateTime start = DateTime.Now;

        static void Main(string[] args)
        {
            Console.Title = "Aldos rev.2 by Blackrush - emulator for Dofus " + Network.Messages.Version.ToString() + ".";
            Console.WindowWidth += 7;
            
            Realm.Server.Instance.Start();
            Game.Server.Instance.Start();
            Utils.DatabaseManager.Instance.Init();

            Utils.MyConsole.WriteLine
                (
                    Global.Account.Load() + " accounts loaded.",
                    ConsoleType.Info, ConsoleWriter.Unknown
                );
            Utils.MyConsole.WriteLine
                (
                    Global.Character.Load() + " characters loaded.",
                    ConsoleType.Info, ConsoleWriter.Unknown
                );

            Utils.MyConsole.WriteLine
                (
                    "Loading time : " + (DateTime.Now - start).TotalMilliseconds + "ms.",
                    ConsoleType.Info, ConsoleWriter.Unknown
                );
            Utils.MyConsole.WriteLine
                (
                    "CTRL + C or \"exit\" command to exit.",
                    ConsoleType.Info, ConsoleWriter.Unknown
                );

            Console.CancelKeyPress += delegate { Exit(); };

            while (Parse(Console.ReadLine())) ;

            Exit();

            Console.ReadLine();
        }

        public static void Exit()
        {
            Realm.Server.Instance.Stop();
            Game.Server.Instance.Stop();

            Character.SaveAll();
            Account.SaveAll();

            Utils.DatabaseManager.Instance.Close();
        }

        static bool Parse(string command)
        {
            switch (command)
            {
                case "database_info":
                    break;

                case "exit":
                    return false;

                case "process_queue":
                    DateTime started = DateTime.Now;
                    Console.WriteLine(" : " + Utils.DatabaseManager.Instance.ProcessQueue() + " commands executed in " + 
                        (DateTime.Now - started).TotalMilliseconds + " ms."); 
                        
                        break;

                case "save_characters":
                    Console.WriteLine(" : " + Global.Character.SaveAll() + " characters were saved."); break;

                case "save_accounts":
                    Console.WriteLine(" : " + Global.Account.SaveAll() + " accounts were saved."); break;

                default:
                    Console.WriteLine(" : invalid command."); break;
            }

            return true;
        }
    }
}
