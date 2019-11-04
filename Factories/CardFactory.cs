using Poker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public static class CardFactory
    {
        internal static Card BuildCard(string cardText)
        {
            if (cardText.Length != 2)
            {
                throw new Exception("Card text was not the length expected");
            }
            var cardValue = GetCardValue(cardText[0]);
            var suit = GetCarSuit(cardText[1]);
            return new Card(cardValue, suit);
        }

        static Suit GetCarSuit(char cardSuitChar)
        {
            switch (cardSuitChar)
            {
                case 's': return Suit.Spades;
                case 'd': return Suit.Diamonds;
                case 'c': return Suit.Clubs;
                case 'h': return Suit.Hearts;
            }
            throw new ArgumentException($"Could not determine Card suit of {cardSuitChar}");
        }


        static CardValue GetCardValue(char cardValueChar)
        {
            if (int.TryParse(cardValueChar.ToString(), out int numeric))
            {
                if (numeric >= 2 && numeric <= 9)
                {
                    return (CardValue)numeric;
                }
            }
            switch (cardValueChar)
            {
                case 'T': return CardValue.Ten;
                case 'J': return CardValue.Jack;
                case 'Q': return CardValue.Queen;
                case 'K': return CardValue.King;
                case 'A': return CardValue.Ace;
            }
            throw new ArgumentException($"Could not determine Card value of {cardValueChar}");
        }
    }
}
