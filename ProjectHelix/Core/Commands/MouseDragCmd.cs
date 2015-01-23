using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectHelix.Core.Commands
{
    class MouseDragCmd : ICommand
    {
        public MouseDragCmd(Vector2 v)
        {
            DeltaMoved = v;
        }
        public Vector2 DeltaMoved { get; set; }
        public void Execute()
        {
            MainCore.Instance.StateEngine.CurrentState.ProcessMouseDrag();
        }

        private MouseDragCmd() { }
    }
}
