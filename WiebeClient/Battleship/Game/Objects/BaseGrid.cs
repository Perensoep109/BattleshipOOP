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
    abstract class BaseGrid : GameObject, IRenderable, IClickableEvent
    {
        public Rectangle? Bounds { get; set; }

        public event EventHandler<MouseState> OnClick;
        public event EventHandler<Cell> OnCellClick;

        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        public int TileDim { get; private set; }

        public object[,] Data { get; protected set; }
        public SpriteResource Sprite { get; set; }

        private SpriteFontResource m_font;

        public BaseGrid(Vector2 a_pos, int a_width, int a_height, int a_tileDim) : base(a_pos)
        {
            GridWidth = a_width;
            GridHeight = a_height;
            TileDim = a_tileDim;
            Bounds = new Rectangle(new Point((int)m_pos.X, (int)m_pos.Y), new Point(GridWidth * TileDim, GridHeight * TileDim));
            Sprite = ResourcePool.GetSprite("line");
            m_font = ResourcePool.GetSpriteFont("font");

        }

        public void BaseOnClick(MouseState a_state)
        {
            OnCellClick?.Invoke(this, GetCell((int)Math.Floor((decimal)(a_state.X - m_pos.X) / TileDim), (int)Math.Floor((decimal)(a_state.Y - m_pos.Y) / TileDim)));
        }

        public virtual void Draw(SpriteBatch a_sprBatch)
        {
            // Draw grid
            // Vertical lines
            for (int i = 1; i < GridWidth; i++)
            {
                a_sprBatch.Draw(Sprite.Sprite, new Rectangle(new Point((int)m_pos.X, (int)m_pos.Y) + new Point(i * TileDim, 0), new Point(1, GridHeight * TileDim)), Color.White);
            }

            // Horizontal lines
            for (int i = 1; i < GridHeight; i++)
            {
                a_sprBatch.Draw(Sprite.Sprite, new Rectangle(new Point((int)m_pos.X, (int)m_pos.Y) + new Point(0, i * TileDim), new Point(GridWidth * TileDim, 1)), Color.White);
            }

            // Border lines
            a_sprBatch.Draw(Sprite.Sprite, new Rectangle(new Point((int)m_pos.X, (int)m_pos.Y) + new Point(0, 0), new Point(1, GridHeight * TileDim)), Color.White);                         // LEFT
            a_sprBatch.Draw(Sprite.Sprite, new Rectangle(new Point((int)m_pos.X, (int)m_pos.Y) + new Point(GridWidth * TileDim, 0), new Point(1, GridHeight * TileDim)), Color.White);     // RIGHT
            a_sprBatch.Draw(Sprite.Sprite, new Rectangle(new Point((int)m_pos.X, (int)m_pos.Y) + new Point(0, 0), new Point(GridWidth * TileDim, 1)), Color.White);                           // UP
            a_sprBatch.Draw(Sprite.Sprite, new Rectangle(new Point((int)m_pos.X, (int)m_pos.Y) + new Point(0, GridHeight * TileDim), new Point(GridWidth * TileDim, 1)), Color.White);     // DOWN

            // Draw grid position indicators
            for (int i = 0; i < GridWidth; i++)
            {
                a_sprBatch.DrawString(m_font.Font, new string((char)(0x41 + i), 1), m_pos + new Vector2(i * TileDim, 0) - new Vector2(0, TileDim), Color.Black);
            }

            // Horizontal lines
            for (int i = 0; i < GridHeight; i++)
            {
                a_sprBatch.DrawString(m_font.Font, i.ToString(), m_pos + new Vector2(0, i * TileDim) - new Vector2(TileDim, 0), Color.Black);
            }
        }

        protected Cell GetCell(int a_x, int a_y)
        {
            if (a_x < 0 || a_x > GridWidth || a_y < 0 || a_y > GridHeight)
                return new Cell(-1, -1, null);

            return new Cell(a_x, a_y, Data[a_x, a_y]);
        }

        public void SetCell(int a_x, int a_y, object a_data)
        {
            if (a_x < 0 || a_x > GridWidth || a_y < 0 || a_y > GridHeight)
                return;

            Data[a_x, a_y] = a_data;
        }
    }
}
