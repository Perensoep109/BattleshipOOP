using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    struct Cell
    {
        public int m_xPos;
        public int m_yPos;
        public object m_data;

        public Cell(int a_x, int a_y, object a_data)
        {
            m_xPos = a_x;
            m_yPos = a_y;
            m_data = a_data;
        }
    }
}
