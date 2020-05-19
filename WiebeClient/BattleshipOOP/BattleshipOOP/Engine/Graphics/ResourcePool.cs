using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Graphics
{
    /// <summary>
    /// A resource pool is a singleton which holds all sprite resources in a central place.
    /// So every system can request a sprite at any time
    /// </summary>
    class ResourcePool
    {
        /// <summary>
        /// The singleton instance of this ResourcePool
        /// </summary>
        public static ResourcePool Resources { get; private set; }

        /// <summary>
        /// A dictionary of all the textures in this pool
        /// </summary>
        private Dictionary<string, SpriteResource> m_textures;

        /// <summary>
        /// The static singleton constructor
        /// </summary>
        static ResourcePool()
        {
            Resources = new ResourcePool();
        }

        /// <summary>
        /// A private constructor to assure that only ONE instance of this class exists
        /// </summary>
        private ResourcePool()
        {
            m_textures = new Dictionary<string, SpriteResource>();
        }

        /// <summary>
        /// Get a single sprite from the pool
        /// </summary>
        /// <param name="a_name">The key of the sprite to fetch</param>
        /// <returns>The requested sprite. If it was not found, it throws a FileNotFoundException</returns>
        public static SpriteResource GetSprite(string a_name)
        {
            SpriteResource sprite;
            if (!Resources.m_textures.TryGetValue(a_name, out sprite))
                throw new FileNotFoundException("File not found: " + a_name);
            return sprite;
        }

        /// <summary>
        /// Convert a Texture2D to a sprite and add it to the pool
        /// </summary>
        /// <param name="a_texture">The Texture2D to load</param>
        /// <param name="a_name">The name to store this sprite with</param>
        public static void LoadSprite(Texture2D a_texture, string a_name)
        {
            Resources.m_textures.Add(a_name, new SpriteResource(a_texture));
        }
    }
}
