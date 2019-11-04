using Poker.Models.ModelHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Models
{
    class PlayerHand : IComparable<PlayerHand>, IEquatable<PlayerHand>, IEnumerable<Card>
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

        private Card _highCard;
        public Card HighCard
        {
            get
            {
                if (_highCard == null)
                {
                    IEnumerable<Card> possibleHighCards = this.Hand;
                    var isFlush = this.HandRank == HandRank.Straight || this.HandRank == HandRank.StraightFlush;
                    if (isFlush && this.Hand.Any(x => x.CardValue == CardValue.Two))
                    {
                        possibleHighCards = possibleHighCards.Where(x => x.CardValue != CardValue.Ace);
                    }
                    _highCard = possibleHighCards.OrderByDescending(x => x.CardValue).First();
                }
                return _highCard;
            }
        }

        public int CompareTo(PlayerHand other)
        {
            if (this.HandRank != other.HandRank)
            {
                return other.HandRank - this.HandRank;
            }
            var thisHighCard = this.HighCard.CardValue;
            var otherHighCard = other.HighCard.CardValue;
            return otherHighCard - thisHighCard;
        }

        public bool Equals(PlayerHand other)
        {
            var comparison = CompareTo(other);
            return comparison == 0;
        }

        public IEnumerator<Card> GetEnumerator()
        {
            foreach (var card in this.Hand)
            {
                yield return card;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Hand.GetEnumerator();
        }


        public override string ToString() => $"Player {PlayerId}: {this.HandRank}";
    }
}
