using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple_Blackjack
{
    internal class Hand
    {
        public List<Card> Cards { get; } = new List<Card>();

        public void Add(Card card)
        {
            Cards.Add(card);
        }

        public int GetTotalValue(bool onlyFirstCard = false)
        {   
            if (Cards.Count == 0) return 0;

            if (onlyFirstCard)
            {
                return Cards[0].Value;
            }

            return Cards.Sum(c => c.Value);
        }
    }
}
