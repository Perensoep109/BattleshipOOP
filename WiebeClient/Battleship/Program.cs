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
            string ip = "127.0.0.1";
            int port = 69;
            string gameID = "0";

            if (args.Contains("--ip"))
                ip = args[Array.FindIndex(args, arg => arg == "--ip") + 1];
            if (args.Contains("--port"))
                port = Convert.ToInt32(args[Array.FindIndex(args, arg => arg == "--port") + 1]);
            if (args.Contains("--gameid"))
                gameID = args[Array.FindIndex(args, arg => arg == "--gameid") + 1];
            using (BattleshipGame game = new BattleshipGame(ip, port, gameID))
            {
                game.Run();
            }
        }
    }
}
