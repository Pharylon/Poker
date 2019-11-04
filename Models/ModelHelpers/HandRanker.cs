using Poker.ExtensionMoethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Models.ModelHelpers
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
            var orderedValues = cards.Select(x => x.CardValue).OrderByDescending(x => x).ToArray();
            var highValue = (int)orderedValues.First();
            var otherValues = orderedValues.Skip(1).Sum(x => (int)x);
            if (highValue*2 - otherValues == orderedValues.Length)
            {
                return true;
            }
            //This is for an ace-low stright. This part WILL fail if we ever 
            //do more than three-card poker!!!
            if (highValue == 14 && otherValues == 5)
            {
                return true;
            }
            return false;
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


}
