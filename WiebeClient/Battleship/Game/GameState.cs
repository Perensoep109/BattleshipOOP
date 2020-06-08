using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    enum GameState
    {
        Invalid = 0,
        ShipPlacement = 1,
        Shoot = 2,
        OtherPlayerTurn = 3
    }
}
