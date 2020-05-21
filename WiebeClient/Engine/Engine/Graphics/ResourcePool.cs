using Engine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics
{
    /// <summary>
    /// A resource pool is a singleton which holds all sprite resources in a central place.
    /// So every system can request a sprite at any time
    /// </summary>
    public class ResourcePool
    {
        /// <summary>
        /// The singleton instance of this ResourcePool
        /// </summary>
        public static ResourcePool Resources { get; private set; }

        /// <summary>
        /// A dictionary of all the textures in this pool
        /// </summary>
        private Dictionary<string, BaseResource> m_resources;

        /// <summary>
        /// The static singleton constructor
        /// </summary>
        static ResourcePool()
        {
            Resources = new ResourcePool();
        }

        /// <summary>
        /// A private constructor to assure that only ONE instance of this public class exists
        /// </summary>
        private ResourcePool()
        {
            m_resources = new Dictionary<string, BaseResource>();
        }

        /// <summary>
        /// Get a single sprite from the pool
        /// </summary>
        /// <param name="a_name">The key of the sprite to fetch</param>
        /// <returns>The requested sprite. If it was not found, it throws a FileNotFoundException</returns>
        public static SpriteResource GetSprite(string a_name)
        {
            BaseResource resource;
            if (!Resources.m_resources.TryGetValue(a_name, out resource))
                throw new FileNotFoundException("File not found: " + a_name);
            return (SpriteResource)resource;
        }

        public static SpriteFontResource GetSpriteFont(string a_name)
        {
            BaseResource resource;
            if (!Resources.m_resources.TryGetValue(a_name, out resource))
                throw new FileNotFoundException("File not found: " + a_name);
            return (SpriteFontResource)resource;
        }

        public static void LoadResource(BaseResource a_resource, string a_name)
        {
            Resources.m_resources.Add(a_name, a_resource);
        }
    }
}
