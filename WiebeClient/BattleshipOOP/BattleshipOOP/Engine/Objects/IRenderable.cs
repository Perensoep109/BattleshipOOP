using Battleship.Engine.Events;
using Battleship.Engine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Objects
{
    interface IRenderable
    {
        SpriteResource Sprite { get; set; }

        void Draw(SpriteBatch a_sprBatch);
    }
}
