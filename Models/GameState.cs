using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Models
{
    public class GameState
    {
        internal GameState(int playerCount, PlayerHand[] playerHands)
        {
            this.PlayerCount = playerCount;
            this.PlayerHands = playerHands;
        }

        internal int PlayerCount { get; private set; }
        internal PlayerHand[] PlayerHands { get; private set; }


        public override string ToString() => $"{PlayerCount} players";
    }
}
