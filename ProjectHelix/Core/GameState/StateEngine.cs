using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelix.Core.GameState
{
    public class StateEngine
    {
        public StateEngine()
        {
            CurrentState = new ChooseState();
        }

        public GameState CurrentState { get; set; }
    }
}
