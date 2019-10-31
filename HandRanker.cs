using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    static class HandRanker
    {
        public static HandRank GetRank(IEnumerable<Card> cards)
        {
            var isStraight = IsStraight(cards);
            var isFlush = cards.AreSameValue(x => x.Suit);
            if (isStraight && isFlush)
            {
                return HandRank.StraightFlush;
            }
            if (cards.AreSameValue(x => x.CardValue))
            {
                return HandRank.ThreeOfAKind;
            }
            if (isStraight)
            {
                return HandRank.Straight;
            }
            if (isFlush)
            {
                return HandRank.Flush;
            }
            var isPair = IsPair(cards);
            if (isPair)
            {
                return HandRank.Pair;
            }
            return HandRank.HighCard;
        }

        public static bool IsStraight(IEnumerable<Card> cards)
        {
            Card lastCard = null;
            foreach (var card in cards.OrderByDescending(x => x.CardValue))
            {
                if (lastCard != null && lastCard.CardValue - card.CardValue != 1)
                {
                    return false;
                }
                lastCard = card;
            }
            return true;
        }

        public static bool IsPair(IEnumerable<Card> cards)
        {
            foreach (var card in cards.Skip(1))
            {
                if (cards.Count(x => x.CardValue == card.CardValue) == 2)
                {
                    return true;
                }
            }
            return false;
        }
    }

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
