using BattleshipOOP.Engine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Battleship.Engine.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace BattleshipOOP.Game
{
    class MainMenuUI : UILayer
    {
        public MainMenuUI(GraphicsDevice a_graphicsDevice)
        {
            AddUI(new UIButton(130, 50, "Launch game", ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 0, 0);
        }
    }
}
