using Engine.Events;
using Engine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Objects
{
    public interface IRenderable
    {
        SpriteResource Sprite { get; set; }

        void Draw(SpriteBatch a_sprBatch);
    }
}
