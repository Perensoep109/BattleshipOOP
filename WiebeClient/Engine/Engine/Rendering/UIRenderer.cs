using Engine;
using Engine.Scenes;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Rendering
{
    public class UIRenderer
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
