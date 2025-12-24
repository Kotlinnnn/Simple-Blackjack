import random
import time

class Card:
    def __init__(self, suit, rank):
        self.suit = suit
        self.rank = rank
        self.value = self.determine_value(rank)

    def determine_value(self, rank):
        if rank in ["J", "Q", "K"]:
            return 10
        elif rank == "A":
            return 1
        return int(rank)

    def __str__(self):
        return f"[{self.rank} {self.suit}]"

class Deck:
    def __init__(self):
        suits = ["Hearts", "Diamonds", "Clubs", "Spades"]
        ranks = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"]
        self.cards = [Card(s, r) for s in suits for r in ranks]

    def shuffle(self):
        random.shuffle(self.cards)

    def draw_card(self):
        return self.cards.pop(0)

class Hand:
    def __init__(self):
        self.cards = []

    def add(self, card):
        self.cards.append(card)

    def get_total(self, only_first=False):
        if not self.cards: return 0
        if only_first:
            return self.cards[0].value
        return sum(c.value for c in self.cards)

def draw_table(player_hand, dealer_hand, hide_dealer):
    print("\n" + "="*30)
    dealer_score = dealer_hand.get_total(only_first=hide_dealer)
    print(f"KRUPIER: {dealer_score}")
    
    if hide_dealer:
        print(f" {dealer_hand.cards[0]} [????]")
    else:
        print(" " + " ".join(str(c) for c in dealer_hand.cards))
    
    print(f"\nTY: {player_hand.get_total()}")
    print(" " + " ".join(str(c) for c in player_hand.cards))
    print("="*30 + "\n")

def start_game():
    deck = Deck()
    deck.shuffle()
    
    player_hand = Hand()
    dealer_hand = Hand()

    for _ in range(2):
        player_hand.add(deck.draw_card())
        dealer_hand.add(deck.draw_card())

    while player_hand.get_total() < 21:
        draw_table(player_hand, dealer_hand, True)
        move = input("Dobierasz (h) czy pasujesz (s)? ").lower()
        if move == 'h':
            player_hand.add(deck.draw_card())
        else:
            break

    if player_hand.get_total() <= 21:
        while dealer_hand.get_total() < 17 or dealer_hand.get_total() < player_hand.get_total():
            dealer_hand.add(deck.draw_card())
            draw_table(player_hand, dealer_hand, False)
            time.sleep(1)
            if dealer_hand.get_total() > 21: break

    draw_table(player_hand, dealer_hand, False)
    
    p_score = player_hand.get_total()
    d_score = dealer_hand.get_total()

    if p_score > 21:
        print("PRZEGRANA! Fura (powyzej 21).")
    elif d_score > 21 or p_score > d_score:
        print("WYGRANA!")
    elif p_score == d_score:
        print("REMIS!")
    else:
        print("PRZEGRANA! Krupier ma wiecej punktow.")

if __name__ == "__main__":
    start_game()
