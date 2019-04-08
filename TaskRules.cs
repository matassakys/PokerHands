using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHands
{
    // https://projecteuler.net/problem=54 rules of this problem
    public class TaskRules
    {
        int player1WinCount = 0;

        public int Player1WinCount { get => player1WinCount; }

        public void BeginNewGame()
        {
            var lines = FileReader.GetAllLines();
            HandsComparer handsComparer = new HandsComparer();

            foreach(var line in lines)
            {
                if (handsComparer.DoesPlayer1Win(line))
                {
                    player1WinCount++;
                }
            }   
        }
    }
}
