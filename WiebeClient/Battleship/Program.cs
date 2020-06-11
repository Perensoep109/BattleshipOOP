using Battleship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine("Args {0}", args);
                using (BattleshipGame game = new BattleshipGame(args[0]))
                {
                    game.Run();
                }
            }
            else
            {
                Console.WriteLine("No command line arguments found, reverting to base settings");
                using (BattleshipGame game = new BattleshipGame("0"))
                {
                    game.Run();
                }
            }
            
        }
    }
}
