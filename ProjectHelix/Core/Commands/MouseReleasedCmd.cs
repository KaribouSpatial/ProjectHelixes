using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectHelix.Core.Commands
{
    class MouseReleasedCmd : ICommand
    {
        public MouseReleasedCmd(Vector2 clicPos)
        {
            ClicPos = clicPos;
        }
        public Vector2 ClicPos { get; set; }
        public void Execute()
        {
            MainCore.Instance.StateEngine.CurrentState.ProcessMouseRelease();
        }

        private MouseReleasedCmd() { }
    }
}
