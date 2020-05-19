using Battleship.Engine;
using Battleship.Engine.Graphics;
using Battleship.MainGame;
using BattleshipOOP.Engine.Networking;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Game.Scenes
{
    class MultiplayerScene : Scene, INetworkScene
    {
        public bool NetworkResync { get; set; }
        public Scene NetworkScene { get; set; }

        private ServerConnection m_connection;

        public MultiplayerScene()
        {
            m_connection = new ServerConnection();
            m_connection.Connect(IPAddress.Parse("127.0.0.1"), 69);
            m_connection.ReceivedPacket += ProcessPacket;

            AddGameObject(new TestObject(new Vector2(0, 0), ResourcePool.GetSprite("tile")));
        }

        public void ProcessPacket(object a_sender, byte[] a_packet)
        {
            GameObjects[0].m_pos += new Vector2(10, 10);
            NetworkResync = true;
            NetworkScene = this;
        }

        public void Sync()
        {
            //int i = 0;
            //NetworkScene.GameObjects.ForEach(obj => GameObjects[i++] = obj);
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
    }
}
