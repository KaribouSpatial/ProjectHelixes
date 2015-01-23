using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHelix.Data;

namespace ProjectHelix.Core.Commands
{
    class DestroyCmd: ICommand
    {
        public AbstractNode NodeToDestroy { get; set; }
        public void Execute()
        {
            var Return = new bool();
            MainCore.Instance.Tree.DestroyNode(NodeToDestroy, ref Return);
        }
    }
}
