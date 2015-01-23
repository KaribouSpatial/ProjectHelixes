using ProjectHelix.Core;

namespace ProjectHelix.Data.BoatComponents
{
    public class Hull : AbstractNode
    {
        private Rarity _rarity;
        public Rarity Rarity
        {
            get { return _rarity; }
            set { _rarity = value; Hp = 5 + ((int) value*3); CurrentHp = Hp; } 
        }

        public int Hp { get; private set; }
        public int CurrentHp { get; set; }

        public Hull(Rarity rarity)
        {
            Rarity = rarity;
            Hp = 5 + ((int) rarity*3);
            CurrentHp = Hp;
        }

        public override void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
