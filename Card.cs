using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Card
    {
        public Card(CardValue cardValue, Suit suit)
        {
            this.CardValue = cardValue;
            this.Suit = suit;
        }

        public CardValue CardValue { get; private set; }
        public Suit Suit { get; private set; }


        public override string ToString() => $"{CardValue} {Suit}";
    }
}
