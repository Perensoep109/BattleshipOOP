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
using Engine.Engine.UI;
using Engine.Events.EventListeners;

namespace Battleship.Game
{
    class MainMenuUI : UILayer
    {
        public MainMenuUI(GraphicsDevice a_graphicsDevice)
        {
            AddUI(new UIButton(130, 30, "Launch game", ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 0, 1);
            AddUI(new UILabel(130, 30, "Multiplayer battleship", ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 0, 0);
            AddUI(new UILabel(130, 30, "Connect to server", ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 0, 2);
            AddUI(new UIInputTextBox(130, 30, ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 1, 2);

            ((UIButton)UIElements[0]).OnClick += MainMenuUI_OnClick;
        }

        private void MainMenuUI_OnClick(object sender, MouseStateEventArgs e)
        {
            SceneSwitcher.LoadScene("GameScene");
        }
    }
}
