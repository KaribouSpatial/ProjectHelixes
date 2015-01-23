using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectHelix.Core.Commands
{
    public class MousePressedCmd: ICommand
    {
        public MousePressedCmd(Vector2 clicPos)
        {
            ClicPos = clicPos;
        }
        public Vector2 ClicPos { get; set; }
        public void Execute()
        {
            MainCore.Instance.StateEngine.CurrentState.ProcessMousePressed();
        }

        private MousePressedCmd() { }
    }
}
