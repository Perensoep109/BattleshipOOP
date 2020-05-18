using BattleshipOOP.Engine;
using BattleshipOOP.Engine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP
{
    class TestScene : Scene
    {
        public TestScene()
        {
            AddGameObject(new TestObject(new Vector2(0, 0), ResourcePool.GetSprite("love")));
        }
    }
}
