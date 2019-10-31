using System;
using System.IO;
using System.Linq;

namespace Poker
{
    class Program
    {

        //Hi! This is probalby over-engineered and under-tested. I did what I could given the time constraints. Don't judge me too harsly!

        static void Main(string[] args)
        {
            try
            {
                string stdIn = Console.In.ReadToEnd();
                var gameState = GetGameState(stdIn);
                var bestToWorstHands = gameState.PlayerHands.OrderBy(x => x).ToArray();
                var winningHands = bestToWorstHands.Where(x => x.Equals(bestToWorstHands.First()));
                var winningIds = winningHands.Select(x => x.PlayerId).OrderBy(x => x);
                Console.Out.WriteLine(string.Join(" ", winningIds));
            }
            catch (ArgumentException ex)
            {
                Console.Out.WriteLine($"There was an error with your input: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("There was an unexpected error. Please send the following with your trouble ticket:");
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace);
            }
            
        }

        static GameState GetGameState(string input)
        {
            var lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (!int.TryParse(lines[0], out int numberOfPlayers))
            {
                throw new ArgumentException($"Number of players could not be determined from input");
            }
            if (numberOfPlayers >= 24 || numberOfPlayers <= 0)
            {
                throw new ArgumentException("Number of players should be a a number greater than 0 and less than 24");
            }
            var playerHands = lines.Skip(1).Select(BuildPlayerHand).ToArray();
            if (playerHands.Length != numberOfPlayers)
            {
                throw new ArgumentException("Number of players specified did not match the number supplied");
            }
            return new GameState(numberOfPlayers, playerHands);
        }

        static PlayerHand BuildPlayerHand(string handText)
        {
            var playerHandLine = handText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (playerHandLine.Length != 4)
            {
                throw new ArgumentException($"The following line line was improperly formatted: {Environment.NewLine } {handText}");
            }
            if (!int.TryParse(playerHandLine[0], out int playerNumber))
            {
                throw new ArgumentException($"Player number could not be determiend on line: {handText}");
            }
            var cards = playerHandLine.Skip(1).Select(CardHelper.BuildCard);
            return new PlayerHand(playerNumber, cards);
        }

        
    }
}
