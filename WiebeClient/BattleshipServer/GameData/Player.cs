using BaseServer.Networking;
using ServerTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer.GameData
{
    class Player
    {
        public BaseClient m_client;
        public uint ID { get; private set; }
        public List<Ship> m_ships;

        public Player(uint a_id, BaseClient a_client)
        {
            m_client = a_client;
            ID = a_id;
            m_ships = new List<Ship>();
        }

        public void CreateShip(byte[] a_data)
        {
            m_ships.Add(new Ship(a_data[0], a_data[1], a_data[2], a_data[3], a_data[4]));
        }

        public bool Hit(byte a_x, byte a_y)
        {
            foreach(Ship ship in m_ships)
            {
                if (ship.Hit(a_x, a_y))
                    return true;
            }

            return false;
        }

        public bool CheckDestroyed()
        {
            foreach (Ship ship in m_ships)
            {
                if (!ship.Destroyed())
                    return false;
            }

            return true;
        }
    }
}
