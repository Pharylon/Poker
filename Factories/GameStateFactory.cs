using Poker.Factories.FactoryModels;
using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Factories
{
    internal static class GameStateFactory
    {
        internal static GameStateBuildResult Build(string input)
        {
            var lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (!int.TryParse(lines[0], out int numberOfPlayers))
            {
                //Something is really wrong with the input. Return now to give the user a usable error.
                return new GameStateBuildResult(new[] {"Number of players could not be determined from input"});
            }
            var playerHandResults = lines.Skip(1).Select(BuildPlayerHand).ToArray();
            var errors = playerHandResults.Select(x => x.error).Where(x => !string.IsNullOrWhiteSpace(x));
            if (errors.Any())
            {
                return new GameStateBuildResult(errors);
            }
            var gameState = new GameState(numberOfPlayers, playerHandResults.Select(x => x.hand).ToArray());
            var gameStateErrors = ValidateGameState(gameState).ToArray();
            if (gameStateErrors.Any())
            {
                return new GameStateBuildResult(gameStateErrors);
            }
            return new GameStateBuildResult(gameState);
        }

        static IEnumerable<string> ValidateGameState(GameState gameState)
        {
            if (gameState.PlayerCount >= 24 || gameState.PlayerCount <= 0)
            {
                yield return "Number of players should be a a number greater than 0 and less than 24";
            }
            if (gameState.PlayerCount != gameState.PlayerHands.Length)
            {
                yield return "Number of players specified did not match the number supplied";
            }
            //Since .Equals will check by IComparable the equality operator will check if the cards share the same
            //memory location, this will make sure there are no duplicate cards.
            var allCards = gameState.PlayerHands.SelectMany(x => x.Hand);
            var duplicateCards = allCards
                .Where(x => allCards.Any(y => y != x && y.Equals(x)))
                .ToArray();
            if (duplicateCards.Any())
            {
                var dupStrings = duplicateCards.Select(x => x.ToString());
                var dupStringJOined = String.Join(", ", dupStrings);
                yield return $"Duplicates of the following cards were found: {dupStringJOined}";
            }
            var actualPlayerIds = gameState.PlayerHands.Select(x => x.PlayerId);
            foreach (var id in Enumerable.Range(0, gameState.PlayerCount))
            {
                if (!actualPlayerIds.Contains(id))
                {
                    yield return $"Expected a player with id of {id}, but was unable to find one.";
                }
            }
        }

        private static (PlayerHand hand, string error) BuildPlayerHand(string handText)
        {
            var playerHandLine = handText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (playerHandLine.Length != 4)
            {
                return (null, $"The following line line was improperly formatted: {Environment.NewLine } {handText}");
            }
            if (!int.TryParse(playerHandLine[0], out int playerNumber))
            {
                return (null, $"Player number could not be determiend on line: {handText}");
                throw new ArgumentException();
            }
            var cards = playerHandLine.Skip(1).Select(CardFactory.BuildCard);
            var playerHand = new PlayerHand(playerNumber, cards);
            return (playerHand, string.Empty);
        }
    }
}
