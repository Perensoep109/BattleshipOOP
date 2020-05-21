using Engine;
using Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Networking
{
    public interface INetworkScene
    {
        bool NetworkResync { get; set; }
        GameScene NetworkScene { get; set; }
        void Sync();
        void ProcessPacket(object a_sender, Packet a_packet);
    }
}
