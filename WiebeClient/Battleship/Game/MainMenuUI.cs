using Engine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Engine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Engine.Scenes;

namespace Battleship.Game
{
    class MainMenuUI : UILayer
    {
        public MainMenuUI(GraphicsDevice a_graphicsDevice)
        {
            AddUI(new UIButton(130, 30, "Launch game", ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 0, 1);
            AddUI(new UILabel(130, 30, "Multiplayer battleship", ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 0, 0);
            ((UIButton)UIElements[0]).OnClick += MainMenuUI_OnClick;
        }

        private void MainMenuUI_OnClick(object sender, Microsoft.Xna.Framework.Input.MouseState e)
        {
            SceneSwitcher.LoadScene("GameScene");
        }
    }
}
