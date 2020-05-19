using Battleship.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Networking
{
    interface INetworkScene
    {
        bool NetworkResync { get; set; }
        Scene NetworkScene { get; set; }
        void Sync();
        void ProcessPacket(object a_sender, Packet a_packet);
    }
}
