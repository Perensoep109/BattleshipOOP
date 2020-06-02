using Engine.Engine.Events;
using Engine.Events;
using Engine.Events.EventListeners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.UI
{
    public class UILayer
    {
        public List<UIElementBase> UIElements { get; set; }

        public int CellWidth { get; set; } = 0;
        public int CellHeight { get; set; } = 0;

        public UILayer()
        {
            UIElements = new List<UIElementBase>();
        }

        public void AddUI(UIElementBase a_element, int a_gridX, int a_gridY)
        {
            a_element.GridX = a_gridX;
            a_element.GridY = a_gridY;
            if(a_element is IClickableEvent)
            {
                a_element.Width = CellWidth;
                a_element.Height = CellHeight;
                ((IClickableEvent)a_element).Bounds = new Rectangle(a_element.GridX * CellWidth, a_element.GridY * CellHeight, CellWidth, CellHeight);
            }

            UIElements.Add(a_element);

            if (a_element.Width > CellWidth || a_element.Height > CellHeight)
            {
                CellWidth = a_element.Width;
                CellHeight = a_element.Height;
                UpdateBounds();
            }
        }

        private void UpdateBounds()
        {
            UIElements.ForEach(ui =>
            {
                ui.Width = CellWidth;
                ui.Height = CellHeight;
                if (ui is IClickableEvent)
                    ((IClickableEvent)ui).Bounds = new Rectangle(ui.GridX * CellWidth, ui.GridY * CellHeight, CellWidth, CellHeight);
            });
        }

        public void Activate()
        {
            foreach(UIElementBase ui in UIElements)
            {
                if (ui is IClickableEvent)
                    ClickableEventListener.Instance.Attach((IClickableEvent)ui);
                if (ui is IKeyboardEvent)
                    KeyboardEventListener.Instance.Attach((IKeyboardEvent)ui);
            }
        }

        public void Deactivate()
        {
            foreach (UIElementBase ui in UIElements)
            {
                if (ui is IClickableEvent)
                    ClickableEventListener.Instance.Detach((IClickableEvent)ui);
                if (ui is IKeyboardEvent)
                    KeyboardEventListener.Instance.Attach((IKeyboardEvent)ui);
            }
        }
    }
}
