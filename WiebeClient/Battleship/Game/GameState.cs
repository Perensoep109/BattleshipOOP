using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    enum GameState
    {
        ShipPlacement = 0,
        Shoot = 1,
        OtherPlayerTurn = 2
    }
}
