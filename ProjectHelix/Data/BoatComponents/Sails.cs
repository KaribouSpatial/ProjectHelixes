using ProjectHelix.Core;

namespace ProjectHelix.Data.BoatComponents
{
    public class Sails : AbstractNode
    {
        private Rarity _rarity;
        public Rarity Rarity
        {
            get { return _rarity; }
            set { _rarity = value; Speed = 2 + (int)value; }
        }

        public int Speed { get; private set; }

        public Sails(Rarity rarity)
        {
            _rarity = rarity;
            Speed = 2 + (int) rarity;
        }

        public override void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
