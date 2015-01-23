using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectHelix.Core.Commands
{
    public class MouseMoveCmd : ICommand
    {
        public MouseMoveCmd(Vector2 v)
        {
            DeltaMoved = v;
        }
        public Vector2 DeltaMoved { get; set; }
        public void Execute()
        {
            MainCore.Instance.StateEngine.CurrentState.ProcessMouseMove();
        }

        private MouseMoveCmd() {}
    }
}
