using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_Blackjack
{
    internal class Deck
    {
        List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle(int numberOfSwaps)
        {
            Random rng = new Random();

            for (int i = 0; i < numberOfSwaps; i++)
            {
                int indexA = rng.Next(cards.Count);
                int indexB = rng.Next(cards.Count);

                var temp = cards[indexA];
                cards[indexA] = cards[indexB];
                cards[indexB] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}
