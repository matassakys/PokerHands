using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHands
{
    // Ranks (2, 3, 4, 5, 6, 7, 8, 9, T(Ten = 10), J(Jack = 11)... A(Ace = 14))
    // Colors S - Spades, D - Diamonds, H - Hearts, C - Clubs
    public class Card
    {
        int rank;
        string color;

        public int Rank { get => rank; set => rank = value; }
        public string Color { get => color; set => color = value; }
    }
}
