using Battleship.Engine;
using Battleship.Engine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.MainGame
{
    class TestScene : Scene
    {
        public TestScene()
        {
            AddGameObject(new TestObject(new Vector2(0, 0), ResourcePool.GetSprite("love")));
        }
    }
}
