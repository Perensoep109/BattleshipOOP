using Battleship.Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;
using Battleship.Engine;
using BattleshipOOP.Game.Scenes;

namespace Battleship.MainGame
{
    class BattleshipGame : BaseGame
    {
        protected override void BeginRun()
        {
            base.BeginRun();
            m_currentScene = new MultiplayerScene();
        }

        protected override void LoadContent()
        {
            //ResourcePool.LoadResource(Texture2D.FromStream(m_graphics.GraphicsDevice, File.OpenRead("tile.png")), "tile");
        }
    }
}
