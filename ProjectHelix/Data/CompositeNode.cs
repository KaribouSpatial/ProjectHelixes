using System.Collections.Generic;
using Windows.Storage.Search;
using ProjectHelix.Core;
using SharpDX.Direct2D1.Effects;

namespace ProjectHelix.Data
{
    public class CompositeNode : AbstractNode
    {
        
        protected List<AbstractNode> _kids = new List<AbstractNode>();

        public override void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);

            foreach (AbstractNode kid in _kids)
                kid.AcceptVisitor(visitor);
        }

        public void AddNode (AbstractNode node)
        {
            node.Daddy = this;
            _kids.Add(node);
        }

        public override void DestroyNode(AbstractNode node, ref bool Return)
        {
            if (_kids.Remove(node))
                Return = true;
            if(Return)
                return;
            foreach (var abstractNode in _kids)
            {
                abstractNode.DestroyNode(node, ref Return);
                if (Return)
                    return;
            }
        }
    }
}
