using Engine;
using Engine.Events;
using Engine.Graphics;
using Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Objects
{
    class ShipGrid : BaseGrid
    {
        public ShipGrid(Vector2 a_pos, int a_width, int a_height, int a_tileWidth, int a_tileHeight) : base(a_pos, a_width, a_height, a_tileWidth, a_tileHeight)
        {
            Data = new object[a_width, a_height];
            for (int i = 0; i < a_width; i++)
            {
                for (int j = 0; j < a_height; j++)
                {
                    Data[i, j] = 0;
                }
            }
        }

        public override void Draw(SpriteBatch a_spriteBatch)
        {
            base.Draw(a_spriteBatch);
        }
    }
}
