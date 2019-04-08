using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PokerHands
{
    //Compares hands from 10 card string and returns true if Player1 won
    class HandsComparer
    {
        List<Card> hand1Cards, hand2Cards;
        PokerHand hand1, hand2;
        PokerAlgorithm pokerAlgorithm;

        public HandsComparer()
        {
            pokerAlgorithm = new PokerAlgorithm();
            hand1Cards = new List<Card>();
            hand2Cards = new List<Card>();
        }
        public bool DoesPlayer1Win(string line)
        {
            hand1Cards.Clear();
            hand2Cards.Clear();
            ComposeCards(line);
            hand1 = pokerAlgorithm.GetHandValues(hand1Cards);
            hand2 = pokerAlgorithm.GetHandValues(hand2Cards);
            return CompareHands(hand1, hand2);
        }

        //Composes Card object from string data
        private void ComposeCards(string line)
        {
            int i = 0;
            string[] cards = line.Split();
            foreach (string c in cards)
            {
                Card card = new Card();
                switch (c[0])
                {
                    case 'T': //Ten
                        card.Rank = 10;
                        break;
                    case 'J': //Jack
                        card.Rank = 11;
                        break;
                    case 'Q': //Queen
                        card.Rank = 12;
                        break;
                    case 'K': //King
                        card.Rank = 13;
                        break;
                    case 'A': //Ace
                        card.Rank = 14;
                        break;
                    default:
                        card.Rank = Int32.Parse(c[0].ToString());
                        break;
                }
                card.Color = c[1].ToString();
                //This might be considered magic number? first 5 cards to one hand, next 5 cards to other hand
                if (i < 5)
                {
                    hand1Cards.Add(card);
                }
                else
                {
                    hand2Cards.Add(card);
                }
                i++;
            }
        }

        private bool CompareHands(PokerHand hand1, PokerHand hand2)
        {
            if (hand1.HandValue > hand2.HandValue)
            {
                return true;
            }
            else if (hand1.HandValue == hand2.HandValue)
            {
                int i = 0;
                foreach (var rank in hand1.HandRanks)
                {
                    if (rank > hand2.HandRanks[i])
                    {
                        return true;
                    }
                    else if (rank < hand2.HandRanks[i])
                    {
                        return false;
                    }
                    i++;
                }
                i = 0;
                foreach (var highCard in hand1.HandHighCards)
                {
                    if (highCard > hand2.HandHighCards[i])
                    {
                        return true;
                    }
                    else if (highCard < hand2.HandHighCards[i])
                    {
                        return false;
                    }
                    i++;
                }
            }            
            return false;
        }
    }
}
