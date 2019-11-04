using Poker.Factories;
using Poker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string stdIn = Console.In.ReadToEnd();
                (var winningIds, var errors) = CoreLogic.GetWinners(stdIn);
                if (errors.Length > 0)
                {
                    var errorText = string.Join(", ", errors);
                    Console.Out.WriteLine($"There was an error with your input: {errorText}");
                }
                else
                {
                    var output = string.Join(" ", winningIds.OrderBy(x => x));
                    Console.Out.WriteLine(output);
                }                
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("There was an unexpected error. Please send the following with your trouble ticket:");
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace);
            }
            
        }
    }
}
