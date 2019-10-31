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


        private HandRank? _handRank;
        public HandRank HandRank
        {
            get
            {
                if (_handRank == null)
                {
                    _handRank = HandRanker.GetRank(this.Hand);
                }
                return _handRank.Value;
            }
        }

        public int CompareTo(PlayerHand other)
        {
            if (this.HandRank != other.HandRank)
            {
                return other.HandRank - this.HandRank;
            }
            var thisHighCard = this.Hand.Select(x => x.CardValue).OrderByDescending(x => x).First();
            var otherHighCard = other.Hand.Select(x => x.CardValue).OrderByDescending(x => x).First();
            return otherHighCard - thisHighCard;
        }

        public bool Equals(PlayerHand other)
        {
            var comparison = CompareTo(other);
            return comparison == 0;
        }

        public override string ToString() => $"Player {PlayerId}";


    }
}
