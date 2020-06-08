using Engine.Scenes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Scenes
{
    class MainMenuScene : GameScene
    {
        public MainMenuScene(GraphicsDevice a_graphicsDevice) : base(a_graphicsDevice)
        {
            UI = new MainMenuUI(m_graphics);
        }

        public override void Initialize()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}
