using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_Blackjack
{
    internal class Card
    {
        public string Suit { get; } // Hearts, Diamonds, Clubs, Spades
        public string Rank { get; } // 2-10, J, Q, K, A
        public int Value { get; set; } // 2-10 for numbered cards, 10 for face cards, 1 for Ace

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
            Value = DetermineValue(rank);
        }

        private int DetermineValue(string rank)
        {
            return rank switch
            {
                "2" => 2,
                "3" => 3,
                "4" => 4,
                "5" => 5,
                "6" => 6,
                "7" => 7,
                "8" => 8,
                "9" => 9,
                "10" => 10,
                "J" => 10,
                "Q" => 10,
                "K" => 10,
                "A" => 1,
                _ => throw new ArgumentException("Invalid card rank")
            };
        }
    }
}
