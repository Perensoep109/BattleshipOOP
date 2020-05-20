using Battleship.Engine;
using Battleship.Engine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.UI
{
    class UITextbox : UIComponent, IClickable
    {
        public UITextbox(GraphicsDevice a_graphics) : base(a_graphics)
        {
        }

        public override void Draw(SpriteBatch a_spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
