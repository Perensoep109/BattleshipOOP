using Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Objects
{
    class EnemyShipGrid : BaseGrid
    {
        private SpriteResource m_hitSprite;
        private SpriteResource m_missSprite;

        public EnemyShipGrid(Vector2 a_pos, int a_width, int a_height, int a_tileDim) : base(a_pos, a_width, a_height, a_tileDim)
        {
            m_hitSprite = ResourcePool.GetSprite("hit");
            m_missSprite = ResourcePool.GetSprite("miss");
            Data = new object[a_width, a_height];
            for(int i = 0; i < a_width; i++)
            {
                for(int j = 0; j < a_height; j++)
                {
                    Data[i, j] = 0;
                }
            }
        }

        public override void Draw(SpriteBatch a_sprBatch)
        {
            base.Draw(a_sprBatch);
            for(int i = 0; i < GridWidth; i++)
            {
                for (int j = 0; j < GridHeight; j++)
                {
                    if ((int)Data[i, j] == 1)
                        a_sprBatch.Draw(m_hitSprite.Sprite, new Rectangle(new Point(i * TileDim + (int)m_pos.X, j * TileDim + (int)m_pos.Y), new Point(TileDim, TileDim)), Color.White);
                    if ((int)Data[i, j] == 2)
                        a_sprBatch.Draw(m_hitSprite.Sprite, new Rectangle(new Point(i * TileDim + (int)m_pos.X, j * TileDim + (int)m_pos.Y), new Point(TileDim, TileDim)), Color.White);
                }
            }
        }
    }
}
