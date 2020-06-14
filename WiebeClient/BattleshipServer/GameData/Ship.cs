using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer.GameData
{
    class Ship
    {
        public byte m_xPos, m_yPos, m_xDir, m_yDir, m_length;
        public bool[] m_hits;
        public Ship(byte a_xPos, byte a_yPos, byte a_xDir, byte a_yDir, byte a_length)
        {
            m_xPos = a_xPos;
            m_yPos = a_yPos;
            m_xDir = a_xDir;
            m_yDir = a_yDir;
            m_length = a_length;
            m_hits = new bool[m_length];
        }

        public bool Hit(byte a_x, byte a_y)
        {
            for(int i = 0; i < m_length; i++)
            {
                if(a_x == m_xPos + m_xDir * i && a_y == m_yPos + m_yDir * i)
                {
                    m_hits[i] = true;
                    return true;
                }
            }

            return false;
        }

        public bool Destroyed()
        {
            for (int i = 0; i < m_length; i++)
            {
                if (!m_hits[i])
                    return false;
            }

            return true;
        }
    }
}
