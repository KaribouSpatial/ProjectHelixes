using System.Threading;
using Microsoft.Xna.Framework;
using ProjectHelix.Core;

namespace ProjectHelix.Data
{
    public abstract class AbstractNode
    {
        public enum Rarity
        {
            Common = 0,
            Uncommon,
            Rare,
            Legendary
        };

        public Vector2 Position { get; set; }

        public AbstractNode Daddy { get; set; }

        public virtual void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public virtual void DestroyNode(AbstractNode node, ref bool Return) {}
    }
}
