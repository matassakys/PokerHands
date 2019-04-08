using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHands
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskRules taskRules = new TaskRules();
            taskRules.BeginNewGame();
            System.Console.WriteLine(taskRules.Player1WinCount);
            System.Console.ReadLine();
        }
    }
}
