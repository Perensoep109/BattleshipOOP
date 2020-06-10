using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Battleship.Game.Objects
{
    class ShipGrid : BaseGrid
    {
        private List<Ship> m_ships;
        public ShipPreview ShipPreview { get; private set; }
        public ShipGrid(Vector2 a_pos, int a_width, int a_height, int a_tileDim) : base(a_pos, a_width, a_height, a_tileDim)
        {
            m_ships = new List<Ship>();
            ShipPreview = new ShipPreview();
            ShipPreview.m_length = 5;
            ShipPreview.m_dir = new Vector2(1, 0);
            Data = new object[a_width, a_height];
        }

        public void CheckHit(int a_xPos, int a_yPos)
        {
            m_ships.ForEach(e => e.Hit(a_xPos, a_yPos));
        }

        public override void Draw(SpriteBatch a_spriteBatch)
        {
            base.Draw(a_spriteBatch);
            m_ships.ForEach(s => s.Draw(a_spriteBatch));

            // Draw the ship preview
            // Restrict X position
            if(ShipPreview.m_pos.X >= m_pos.X && ShipPreview.m_pos.X < m_pos.X + GridWidth * TileDim)
                // Restrict Y position
                if(ShipPreview.m_pos.Y >= m_pos.Y && ShipPreview.m_pos.Y <= m_pos.Y + GridHeight * TileDim)
                    ShipPreview.Draw(a_spriteBatch);
        }

        public void CreateShip()
        {
            if (ShipPreview.m_validPos)
                m_ships.Add(new Ship(GetGridCoord(ShipPreview.m_pos), ShipPreview.m_dir, m_pos, ShipPreview.m_length));
        }

        public bool ValidateShipPos(Vector2 a_pos, Vector2 a_dir, int a_length, bool a_computePos = true)
        {
            for(int i = 0; i < a_length; i++)
            {
                Vector2 pos = a_pos;
                if (a_computePos)
                {
                    pos.X = (a_pos.X - m_pos.X) / TileDim + (a_dir.X * i);
                    pos.Y = (a_pos.Y - m_pos.Y) / TileDim + (a_dir.Y * i);
                }

                //  Check if the ship goes out of bounds
                if (pos.X < 0 || pos.X >= GridWidth || pos.Y < 0 || pos.Y >= GridHeight)
                    return false;

                // Check if this pos collides with any other ship
                foreach (Ship ship in m_ships)
                {
                    foreach (Tuple<Vector2, bool> position in ship.Positions)
                    {
                        if (pos == position.Item1)
                            return false;
                    }
                }
            }
            
            return true;
        }

        public void UpdateShipPreview(int a_mouseX, int a_mouseY, int a_scrollVal)
        {
            ShipPreview.ScrollIndex = a_scrollVal;
            ShipPreview.m_validPos = ValidateShipPos(ShipPreview.m_pos, ShipPreview.m_dir, ShipPreview.m_length);
            ShipPreview.m_pos = new Vector2((int)Math.Floor((decimal)(a_mouseX / TileDim)) * TileDim, (int)Math.Floor((decimal)(a_mouseY / TileDim)) * TileDim);
        }

        private Vector2 GetGridCoord(Vector2 a_pos)
        {
            return new Vector2((int)Math.Floor((decimal)((a_pos.X - m_pos.X) / TileDim)), (int)Math.Floor((decimal)((a_pos.Y - m_pos.Y) / TileDim)));
        }
    }
}
