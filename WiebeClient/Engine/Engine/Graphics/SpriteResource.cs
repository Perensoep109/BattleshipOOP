using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Engine.Graphics;

namespace Engine.Graphics
{
    /// <summary>
    /// A SpriteResource is a 2D sprite which is loaded from the file system
    /// It stores the width, height and area bounds of a sprite
    /// </summary>
    public class SpriteResource : BaseResource
    {
        /// <summary>
        /// The 2D texture which represents this sprite
        /// </summary>
        public Texture2D Sprite { get; private set; }
        /// <summary>
        /// The width of this sprite
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// The height of this sprite
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// The area of this sprite
        /// </summary>
        public Rectangle Area { get; private set; }

        public SpriteResource(Texture2D a_sprite)
        {
            Sprite = a_sprite;
            Width = Sprite.Width;
            Height = Sprite.Height;
            Area = new Rectangle(0, 0, Width, Height);
        }
    }
}
