using ProjectHelix.Core;

namespace ProjectHelix.Data.BoatComponents
{
    public class Canons : AbstractNode
    {
        private Rarity _rarity;
        public Rarity Rarity
        {
            get { return _rarity; }
            set { _rarity = value; Firerate = 2 + (int)value; }
        }

        public int Firerate { get; private set; }

        public Canons(Rarity rarity)
        {
            _rarity = rarity;
            Firerate = 2 + (int) rarity;
        }

        public override void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
