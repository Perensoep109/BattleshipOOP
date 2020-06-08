using Engine.Graphics;
using Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Objects
{
    class ShipPreview : IRenderable
    {
        public SpriteResource Sprite { get; set; }
        public int ScrollIndex { set => SetScrollIndex(value); get { return m_scrollIndex; } }

        public int m_length;
        public Vector2 m_dir;
        public Vector2 m_pos;
        public bool m_validPos;
        private int m_scrollIndex;
        private SpriteResource m_validSprite;
        private SpriteResource m_invalidSprite;

        public ShipPreview()
        {
            m_validSprite = ResourcePool.GetSprite("valid");
            m_invalidSprite = ResourcePool.GetSprite("invalid");
        }

        public void Draw(SpriteBatch a_sprBatch)
        {
            for(int i = 0; i < m_length; i++)
            {
                Vector2 pos = m_pos + m_dir * i * 16;
                if(m_validPos)
                    a_sprBatch.Draw(m_validSprite.Sprite, new Rectangle(new Point((int)pos.X + 1, (int)pos.Y + 1), new Point(15, 15)), Color.White);
                else
                    a_sprBatch.Draw(m_invalidSprite.Sprite, new Rectangle(new Point((int)pos.X + 1, (int)pos.Y + 1), new Point(15, 15)), Color.White);
            }
        }

        private void SetScrollIndex(int a_value)
        {
            m_scrollIndex += a_value;
            switch(m_scrollIndex % 2)
            {
                case 0:
                    m_dir.X = 1;
                    m_dir.Y = 0;
                    break;
                case 1:
                    m_dir.X = 0;
                    m_dir.Y = 1;
                    break;
            }
        }
    }
}
