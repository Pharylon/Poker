using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Models
{
    class Card: IEquatable<Card>
    {
        public Card(CardValue cardValue, Suit suit)
        {
            this.CardValue = cardValue;
            this.Suit = suit;
        }

        public CardValue CardValue { get; private set; }
        public Suit Suit { get; private set; }


        public bool Equals(Card other)
        {
            return (other.CardValue == this.CardValue && other.Suit == this.Suit);
        }

        public override string ToString() => $"{CardValue} {Suit}";
    }
}
