using Battleship.Engine;
using Battleship.Engine.Graphics;
using BattleshipOOP.Engine.Networking;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.MainGame
{
    class TestScene : Scene
    {
        ServerConnection m_connection;

        public TestScene()
        {
            m_connection = new ServerConnection();
            m_connection.Connect(IPAddress.Parse("127.0.0.1"), 69);
            m_connection.ReceivedPacket += OnPacketReceived;
            AddGameObject(new TestObject(new Vector2(0, 0), ResourcePool.GetSprite("tile")));
        }

        public override void Update()
        {
            //GameObjects[0].m_pos += new Vector2(10, 10);
            Console.WriteLine(GameObjects[0].m_pos);
        }

        private void OnPacketReceived(object sender, byte[] a_packet)
        {
            GameObjects[0].m_pos += new Vector2(100, 100);
        }
    }
}
