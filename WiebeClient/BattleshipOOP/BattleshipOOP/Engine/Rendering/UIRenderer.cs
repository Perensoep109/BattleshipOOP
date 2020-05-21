using Battleship.Engine;
using Battleship.Engine.Scenes;
using BattleshipOOP.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Rendering
{
    class UIRenderer
    {
        public UIRenderer()
        {

        }

        public void Draw(UILayer a_layer, SpriteBatch a_spriteBatch)
        {
            a_layer.UIElements.ForEach(component => {
                component.Draw(a_spriteBatch);
            });
        }
    }
}
