using Battleship;
using Engine;
using Engine.Networking;
using Engine.Scenes;
using Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Scenes
{
    class MultiplayerScene : GameScene, INetworkScene
    {
        public bool NetworkResync { get; set; }
        public GameScene NetworkScene { get; set; }

        private ServerConnection m_connection;

        public void ProcessPacket(object a_sender, Packet a_packet)
        {
            if (a_packet.m_type == PacketType.Ping)
            {
                GameObjects[0].m_pos += new Vector2(10, 10);
                NetworkResync = true;
                NetworkScene = this;
            }
        }

        public void Sync()
        {
            for(int i = 0; i < NetworkScene.GameObjects.Count; i++)
            {
                GameObjects[i] = NetworkScene.GameObjects[i];
            }
            NetworkScene = null;
            NetworkResync = false;
        }

        public override void Update()
        {
            
        }

        public override void Initialize()
        {
            m_connection = new ServerConnection();
            m_connection.Connect(IPAddress.Parse("127.0.0.1"), 69);
            m_connection.ReceivedPacket += ProcessPacket;
            GameObjects.Add(new TestGameObject(Vector2.Zero));
        }
    }
}
