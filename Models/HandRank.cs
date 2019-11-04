using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Models
{
    public enum HandRank
    {
        HighCard = 1,
        Pair = 2,
        Flush = 3,
        Straight = 4,
        ThreeOfAKind = 5,
        StraightFlush = 6,
    }
}
