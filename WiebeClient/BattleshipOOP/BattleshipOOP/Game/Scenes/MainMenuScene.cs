using BattleshipOOP.Engine.Scenes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Game.Scenes
{
    class MainMenuScene : GameScene
    {
        public MainMenuScene(GraphicsDevice a_graphicsDevice)
        {
            UI = new MainMenuUI(a_graphicsDevice);
        }

        public override void Update()
        {
            
        }
    }
}
