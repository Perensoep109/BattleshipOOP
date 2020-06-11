using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerTesting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            bool verbose = (args.Length > 0 && args[0] == "--verbose");

            Server server = new Server(verbose);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "stop")
                    break;
                if (input == "clear")
                    Console.Clear();
                if (input == "help" || input == "h")
                    Console.WriteLine("Commands:\n'stop' stops the server\n'clear' clears the console\n'verbose' toggles verbose logging mode\n'help' shows all commands");
                if (input == "verbose")
                    server.ToggleVerbose();
            }
        }
    }
}
