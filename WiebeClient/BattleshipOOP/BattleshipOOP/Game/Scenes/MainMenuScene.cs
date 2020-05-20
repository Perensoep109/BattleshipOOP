using Battleship.Engine.Graphics;
using BattleshipOOP.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Game.Scenes
{
    class MainMenuScene : UIScene
    {
        UIButton m_testButton;

        public MainMenuScene(GraphicsDevice a_graphics) : base(a_graphics)
        {
            AddElement(m_testButton = new UIButton(new Vector2(0, 0), 50, 50, "Test", ResourcePool.GetSpriteFont("font").Font, a_graphics));
            m_testButton.OnClick += TestButton_OnClick;
        }

        private void TestButton_OnClick(object sender, MouseState e)
        {
            Console.WriteLine("Click");
        }
    }
}
