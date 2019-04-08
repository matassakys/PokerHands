using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHands
{
    //I implemented this alhorithm http://nsayer.blogspot.com/2007/07/algorithm-for-evaluating-poker-hands.html
    //This class has one public method "GetHandValues" which takes List of 5 Cards and returns PokerHand object.
    public class PokerAlgorithm
    {
        PokerHand hand;
        bool possibleFlushOrStraight = false;
        public PokerHand GetHandValues(List<Card> handCards)
        {
            hand = new PokerHand();
            //sort cards from best to worst
            handCards.Sort((r1, r2) => r2.Rank.CompareTo(r1.Rank));
            
            CheckForPairs(handCards);

            if (possibleFlushOrStraight)
            {
                bool isFlush = CheckForFlush(handCards);
                bool isStraight = CheckForStraight(handCards);
                if (isFlush && isStraight)
                {
                    HandIsStraightFlush(handCards);
                }
                else if (isFlush)
                {
                    HandIsFlush(handCards);
                }
                else if (isStraight)
                {
                    HandIsStraight(handCards);
                }
                else
                {
                    hand.HandRanks = handCards.Select(x => x.Rank).ToList();
                }
            }
            return hand;
        }

        private void HandIsStraight(List<Card> handCards)
        {
            hand.HandValue = PokerHand.PokerHandValue.Straight;
            hand.HandRanks.Add(handCards[0].Rank);
        }

        private void HandIsFlush(List<Card> handCards)
        {
            hand.HandValue = PokerHand.PokerHandValue.Flush;
            hand.HandRanks.AddRange(handCards.Select(x => x.Rank));
        }

        private void HandIsStraightFlush(List<Card> handCards)
        {
            if (handCards[0].Rank == 14 && handCards[1].Rank == 13)
            {
                hand.HandValue = PokerHand.PokerHandValue.RoyalFlush;
            }
            else
            {
                hand.HandValue = PokerHand.PokerHandValue.StraightFlush;
                hand.HandRanks.Add(handCards[0].Rank);
            }
        }

        private bool CheckForStraight(List<Card> handCards)
        {
            if (handCards[0].Rank - handCards[4].Rank == 4) return true;

            if (handCards[0].Rank == 14 && handCards[1].Rank == 5 && handCards[4].Rank == 2) return true;

            return false;
        }

        private bool CheckForFlush(List<Card> handCards)
        {
            var color = handCards[0].Color;
            foreach (var card in handCards)
            {
                if (card.Color != color) return false;
            }
            return true;
        }

        private void CheckForPairs(List<Card> handCards)
        {
            int[] distinctRanks = handCards.Select(x => x.Rank).Distinct().ToArray();

            if (distinctRanks.Length == 5)
            {
                possibleFlushOrStraight = true;
                return;
            }
            possibleFlushOrStraight = false;
            int[] rankCount = new int[4];

            int i = 0;
            foreach (var rank in distinctRanks)
            {
                rankCount[i] = handCards.Count(x => x.Rank == rank);
                i++;
            }

            if (rankCount[3] != 0)
            {
                HandIsOnePair(distinctRanks, rankCount);
            }
            else if ((rankCount[0] == 2 && rankCount[1] == 2) || (rankCount[0] == 2 && rankCount[2] == 2) || (rankCount[2] == 2 && rankCount[1] == 2))
            {
                HandIsTwoPairs(distinctRanks, rankCount);
            }
            else if ((rankCount[0] == 3 || rankCount[1] == 3 || rankCount[2] == 3) && (rankCount[0] != 2 && rankCount[1] != 2 && rankCount[2] != 2))
            {
                HandIsThreeOfAKind(distinctRanks, rankCount);
            }            
            else if (rankCount[0] == 3 || rankCount[1] == 3 || rankCount[2] == 3)
            {
                HandIsFullHouse(distinctRanks, rankCount);
            }
            else if (rankCount[0] == 4 || rankCount[1] == 4)
            {
                HandIsFourOfAKind(distinctRanks, rankCount);
            }
        }

        private void HandIsOnePair(int[] distinctRanks, int[] rankCount)
        {
            hand.HandValue = PokerHand.PokerHandValue.OnePair;
            for (int i = 0; i < 4; i++)
            {
                if (rankCount[i] == 2)
                {
                    hand.HandRanks.Add(distinctRanks[i]);
                }
                else
                {
                    hand.HandHighCards.Add(distinctRanks[i]);
                }
            };
        }

        private void HandIsTwoPairs(int[] distinctRanks, int[] rankCount)
        {
            hand.HandValue = PokerHand.PokerHandValue.TwoPairs;
            for (int i = 0; i < 3; i++)
            {
                if (rankCount[i] == 2)
                {
                    hand.HandRanks.Add(distinctRanks[i]);
                }
                else
                {
                    hand.HandHighCards.Add(distinctRanks[i]);
                }
            }
        }

        private void HandIsThreeOfAKind(int[] distinctRanks, int[] rankCount)
        {
            hand.HandValue = PokerHand.PokerHandValue.ThreeOfAKind;
            for (int i = 0; i < 3; i++)
            {
                if (rankCount[i] == 3)
                {
                    hand.HandRanks.Add(distinctRanks[i]);
                }
                else
                {
                    hand.HandHighCards.Add(distinctRanks[i]);
                }
            }
        }

        private void HandIsFullHouse(int[] distinctRanks, int[] rankCount)
        {
            hand.HandValue = PokerHand.PokerHandValue.FullHouse;
            if (rankCount[0] == 3)
            {
                hand.HandRanks.Add(distinctRanks[0]);
                hand.HandRanks.Add(distinctRanks[1]);
            }
            else if (rankCount[1] == 3)
            {
                hand.HandRanks.Add(distinctRanks[1]);
                hand.HandRanks.Add(distinctRanks[0]);
            }
        }

        private void HandIsFourOfAKind(int[] distinctRanks, int[] rankCount)
        {
            hand.HandValue = PokerHand.PokerHandValue.FourOfAKind;
            if (rankCount[0] == 4)
            {
                hand.HandRanks.Add(distinctRanks[0]);
                hand.HandHighCards.Add(distinctRanks[1]);
            }
            else if (rankCount[1] == 4)
            {
                hand.HandRanks.Add(distinctRanks[1]);
                hand.HandHighCards.Add(distinctRanks[0]);
            }            
        }
    }
}
