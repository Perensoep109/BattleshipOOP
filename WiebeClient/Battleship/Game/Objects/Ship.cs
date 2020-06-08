using Engine;
using Engine.Graphics;
using Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Battleship.Game.Objects
{
    class Ship : GameObject, IRenderable
    {
        public SpriteResource Sprite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Tuple<Vector2, bool>> Positions { get; private set; }

        private SpriteResource m_boatHitSprite;
        private SpriteResource m_boatCellSprite;
        private Vector2 m_dir, m_gridStartPos;

        public Ship(Vector2 a_pos, Vector2 a_dir, Vector2 a_gridStartPos, int a_length) : base(a_pos)
        {
            m_dir = a_dir;
            m_gridStartPos = a_gridStartPos;
            Positions = new List<Tuple<Vector2, bool>>();
            m_boatHitSprite = ResourcePool.GetSprite("hit");
            m_boatCellSprite = ResourcePool.GetSprite("ship");

            // Create positions
            for(int i = 0; i < a_length; i++)
                Positions.Add(new Tuple<Vector2, bool>(m_pos + m_dir * i, false));
        }

        public void Hit(int a_xPos, int a_yPos)
        {
            for(int i = 0; i < Positions.Count; i++)
            {
                Vector2 pos = Positions[i].Item1;
                if((int)pos.X == a_xPos && (int)pos.Y == a_yPos)
                {
                    Positions[i] = new Tuple<Vector2, bool>(Positions[i].Item1, true);
                    return;
                }
            }
        }

        public void Draw(SpriteBatch a_sprBatch)
        {
            foreach(Tuple<Vector2, bool> cell in Positions)
            {
                Vector2 pos = m_gridStartPos + cell.Item1 * 16;
                
                if (cell.Item2)
                    a_sprBatch.Draw(m_boatHitSprite.Sprite, new Rectangle(new Point((int)pos.X + 1, (int)pos.Y + 1), new Point(15, 15)), Color.White);
                else
                    a_sprBatch.Draw(m_boatCellSprite.Sprite, new Rectangle(new Point((int)pos.X + 1, (int)pos.Y + 1), new Point(15, 15)), Color.White);
            }
        }
    }
}
