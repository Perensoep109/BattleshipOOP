using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Initialize("192.168.2.69", 25565);
            server.Start();
        }
    }
}
