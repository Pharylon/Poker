using Poker.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Poker
{
    public class CoreLogic
    {
        public static (int[] ids, string[] errors) GetWinners(string input)
        {
            var gameStateBuildResult = GameStateFactory.Build(input);
            if (gameStateBuildResult.Success)
            {
                var bestToWorstHands = gameStateBuildResult.GameState.PlayerHands.OrderBy(x => x).ToArray();
                var winningHands = bestToWorstHands.Where(x => x.Equals(bestToWorstHands.First()));
                var winningIds = winningHands.Select(x => x.PlayerId).OrderBy(x => x).ToArray();
                return (winningIds, Array.Empty<string>());
            }
            else
            {
                return (Array.Empty<int>(), gameStateBuildResult.Errors);
            }
        }
    }
}
