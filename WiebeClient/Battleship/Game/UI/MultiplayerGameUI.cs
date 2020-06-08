using Engine.Graphics;
using Engine.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Battleship.Game.UI
{
    class MultiplayerGameUI : UILayer
    {
        public UILabel m_label;

        public MultiplayerGameUI(GraphicsDevice a_graphics)
        {
            AddUI(m_label = new UILabel(50, 30, "Ships to place", ResourcePool.GetSpriteFont("font").Font, a_graphics), 6, 10);
        }
    }
}
