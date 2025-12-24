using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Simple_Blackjack
{
    internal class Game
    {
        public void Start()
        {
            int rng = new Random().Next(32, 128);
            Deck deck = new Deck();

            deck.Shuffle(rng);

            Hand playerHand = new Hand();
            Hand dealerHand = new Hand();

            playerHand.Add(deck.DrawCard());
            dealerHand.Add(deck.DrawCard());

            playerHand.Add(deck.DrawCard());
            dealerHand.Add(deck.DrawCard());

            DrawTable(playerHand, dealerHand, showDealerHoleCard: false);

            while (playerHand.GetTotalValue() < 21)
            {
                Console.Write("Dobierasz (h) czy pasujesz (s)? ");
                string input = Console.ReadLine()?.ToLower();

                if (input == "h")
                {
                    playerHand.Add(deck.DrawCard()); 
                    DrawTable(playerHand, dealerHand, showDealerHoleCard: false); 
                }
                else if (input == "s")
                {
                    break; 
                }
            }

            while (dealerHand.GetTotalValue() < 21)
            {
                if (playerHand.GetTotalValue() > 21) break;
                else if (playerHand.GetTotalValue() == dealerHand.GetTotalValue() && dealerHand.GetTotalValue() < 17)
                {
                    dealerHand.Add(deck.DrawCard());
                    DrawTable(playerHand, dealerHand, showDealerHoleCard: true);
                    Delay(800);
                }
                else if (playerHand.GetTotalValue() > dealerHand.GetTotalValue())
                {
                    dealerHand.Add(deck.DrawCard());
                    DrawTable(playerHand, dealerHand, showDealerHoleCard: true);
                    Delay(800);
                }
                else break;
            }

            DrawTable(playerHand, dealerHand, showDealerHoleCard: true);
            CheckWinner(playerHand.GetTotalValue(), dealerHand.GetTotalValue());
        }

        public void DrawTable(Hand playerHand, Hand dealerHand, bool showDealerHoleCard)
        {
            Console.Clear();
            int score = dealerHand.GetTotalValue(!showDealerHoleCard);

            Console.WriteLine("══════════════════════════════════════════");
            Console.WriteLine("               BLACKJACK");
            Console.WriteLine("══════════════════════════════════════════\n");

            Console.WriteLine($"    KRUPIER:{score}");
            DrawCardsRow(dealerHand.Cards, hideSecondCard: !showDealerHoleCard);
            Console.WriteLine();

            Console.WriteLine($"    TY:{playerHand.GetTotalValue()}");
            DrawCardsRow(playerHand.Cards, hideSecondCard: false);
            Console.WriteLine();

            Console.WriteLine("══════════════════════════════════════════\n");
        }

        private void DrawCardsRow(List<Card> hand, bool hideSecondCard)
        {
            string[][] cardLines = new string[hand.Count][];

            for (int i = 0; i < hand.Count; i++)
            {
                bool hidden = hideSecondCard && i == 1; 
                cardLines[i] = hidden ? GetHiddenCardLines() : GetCardLines(hand[i]);
            }

            for (int line = 0; line < 9; line++)
            {
                Console.Write("    "); 
                for (int c = 0; c < hand.Count; c++)
                {
                    Console.Write(cardLines[c][line]);
                    if (c < hand.Count - 1) Console.Write("  "); 
                }
                Console.WriteLine();
            }
        }

        private string[] GetHiddenCardLines()
        {
            return new string[]
            {
        "┌─────────┐",
        "│░░░░░░░░░│",
        "│░░░░░░░░░│",
        "│░░░░░░░░░│",
        "│░░░░░░░░░│",
        "│░░░░░░░░░│",
        "│░░░░░░░░░│",
        "│░░░░░░░░░│",
        "└─────────┘"
            };
        }

        private string[] GetCardLines(Card card)
        {
            string suitSymbol = card.Suit switch
            {
                "Hearts" => "♥",
                "Diamonds" => "♦",
                "Clubs" => "♣",
                "Spades" => "♠",
                _ => "?"
            };

            Console.ForegroundColor = (card.Suit == "Hearts" || card.Suit == "Diamonds")
                ? ConsoleColor.Red
                : ConsoleColor.White;

            string[] lines = new string[]
            {
        "┌─────────┐",
        $"│{card.Rank.PadLeft(2)} {suitSymbol}     │",
        "│         │",
        "│         │",
        "│         │",
        "│         │",
        "│         │",
        $"│      {card.Rank.PadRight(2)}{suitSymbol}│",
        "└─────────┘"
            };
            return lines;
        }
        public void CheckWinner(int playerScore, int dealerScore)
        {
            if (playerScore > 21) Console.WriteLine("PRZEGRANA! Masz fura (powyżej 21).");
            else if (playerScore > dealerScore) Console.WriteLine("WYGRANA!");
            else if (playerScore <= 21 && dealerScore > 21) Console.WriteLine("WYGRANA!");
            else if (playerScore == dealerScore) Console.WriteLine("REMIS! Dealer ma tyle samo punktów co ty");

            Console.Write("Chcesz zagrać jeszcze raz? y/n: ");
            string input = Console.ReadLine()?.ToLower();
            if (input == "y") Start();
            else Console.Write("99% hazardzistów poddaje się przed swoją największą wygraną, jesteś pewien, że chcesz wyjść? y/n: ");
            input = Console.ReadLine()?.ToLower();
            if (input == "y") Stop();
            else Stop();
        }
        public void Stop() { Console.WriteLine("Game stopped!"); }
        private void Delay(int milliseconds)
        {
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds < milliseconds)
            {
                System.Threading.Thread.Yield();
            }
        }
    }
}
