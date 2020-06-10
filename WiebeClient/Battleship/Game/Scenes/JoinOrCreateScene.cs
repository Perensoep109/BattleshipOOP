using Engine.Networking;
using Engine.Scenes;
using Engine.UI;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Scenes
{
    class JoinOrCreateScene : GameScene
    {
        private class UI : UILayer
        {

        }

        private ServerConnection m_con;

        public JoinOrCreateScene(GraphicsDevice a_device) : base(a_device)
        {
            
        }

        public override void Initialize(params object[] a_initialData)
        {
            UiLayer = new UI();
        }

        public override void OnSwitchFrom()
        {
            
        }

        public override void OnSwitchTo(params object[] a_data)
        {
            m_con = (ServerConnection)a_data[0];
        }

        public override void Update()
        {
            
        }
    }
}
