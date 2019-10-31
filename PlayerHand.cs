using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    class PlayerHand : IComparable<PlayerHand>, IEquatable<PlayerHand>
    {
        public PlayerHand(int playerId, IEnumerable<Card> cards)
        {
            this.PlayerId = playerId;
            this.Hand = cards.ToArray();
        }

        public int PlayerId { get; private set; }
        public Card[] Hand { get; private set; }

        public HandRank GetRank() => HandRanker.GetRank(this.Hand);

        public int CompareTo(PlayerHand other)
        {
            var thisRank = HandRanker.GetRank(this.Hand);
            var otherRank = HandRanker.GetRank(other.Hand);
            if (thisRank != otherRank)
            {
                return otherRank - thisRank;
            }
            var thisHighCard = this.Hand.Select(x => x.CardValue).OrderByDescending(x => x).First();
            var otherHighCard = other.Hand.Select(x => x.CardValue).OrderByDescending(x => x).First();
            return thisHighCard - otherHighCard;
        }

        public bool Equals(PlayerHand other)
        {
            var comparison = CompareTo(other);
            return comparison == 0;
        }

        public override string ToString() => $"Player {PlayerId}";


    }
}
