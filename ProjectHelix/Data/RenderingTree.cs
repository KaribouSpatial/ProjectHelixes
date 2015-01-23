
using ProjectHelix.Core;
using ProjectHelix.Data.BoatComponents;

namespace ProjectHelix.Data
{
    public class RenderingTree : CompositeNode
    {

        public RenderingTree()
        {
            Daddy = null;
        }

        public override void AcceptVisitor(IVisitor visitor) 
        {
            visitor.Visit(this);

            foreach (var kid in _kids)
                kid.AcceptVisitor(visitor);
        }
    }
}
