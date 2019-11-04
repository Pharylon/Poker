using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Factories.FactoryModels
{
    class GameStateBuildResult
    {
        public GameStateBuildResult(GameState state)
        {
            this.Success = true;
            this.GameState = state;
            this.Errors = Array.Empty<string>();
        }

        public GameStateBuildResult(IEnumerable<string> errors)
        {
            this.Success = false;
            this.GameState = new GameState(0, Array.Empty<PlayerHand>());
            this.Errors = errors.ToArray();
        }


        public bool Success { get; private set; }
        public GameState GameState { get; private set; }
        public string[] Errors { get; private set; }
    }
}
