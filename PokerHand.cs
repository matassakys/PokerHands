using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHands
{
    //PokerHand after evaluation. etc. if cards are "JS JD 8S 8D 9S" this object will be structured:
    // handValue = TwoPairs
    // handRanks = J and 8 (two pairs)
    // handHighCards = 9 (remaining high card)
    public class PokerHand
    {
        public PokerHand()
        {
            handRanks = new List<int>();
            handHighCards = new List<int>();
            handValue = PokerHandValue.HighCard;
        }
        PokerHandValue handValue;

        List<int> handRanks, handHighCards;

        public PokerHandValue HandValue { get => handValue; set => handValue = value; }
        public List<int> HandRanks { get => handRanks; set => handRanks = value; }
        public List<int> HandHighCards { get => handHighCards; set => handHighCards = value; }

        public enum PokerHandValue
        {
            HighCard = 1,
            OnePair = 2,
            TwoPairs = 3,
            ThreeOfAKind = 4,
            Straight = 5,
            Flush = 6,
            FullHouse = 7,
            FourOfAKind = 8,
            StraightFlush = 9,
            RoyalFlush = 10
        }
    }
    
}
