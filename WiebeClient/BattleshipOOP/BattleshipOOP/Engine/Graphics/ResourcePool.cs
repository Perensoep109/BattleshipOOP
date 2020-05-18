using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Graphics
{


    class ResourcePool
    {
        public static ResourcePool Resources { get; private set; }

        private Dictionary<string, SpriteResource> m_textures;

        static ResourcePool()
        {
            Resources = new ResourcePool();
        }

        private ResourcePool()
        {
            m_textures = new Dictionary<string, SpriteResource>();
        }

        public static SpriteResource GetSprite(string a_name)
        {
            SpriteResource sprite;
            if (!Resources.m_textures.TryGetValue(a_name, out sprite))
                throw new FileNotFoundException("File not found: " + a_name);
            return sprite;
        }

        public static void LoadSprite(Texture2D a_texture, string a_name)
        {
            Resources.m_textures.Add(a_name, new SpriteResource(a_texture));
        }
    }
}
