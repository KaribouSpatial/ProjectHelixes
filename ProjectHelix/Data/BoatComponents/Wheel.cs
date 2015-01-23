using System.Dynamic;
using ProjectHelix.Core;

namespace ProjectHelix.Data.BoatComponents
{
    public class Wheel : AbstractNode
    {
        public enum Direction
        {
            Straight,
            Right, 
            Left
        };

        public Direction DirectionC { get; set; }
        private Rarity _rarity;
        public Rarity Rarity
        {
            get { return _rarity; }
            set { _rarity = value; ManoeuverDif = 100 + ((int)value*15); }
        }

        public int ManoeuverDif { get; private set; }

        public Wheel(Rarity rarity)
        {
            _rarity = rarity;
            DirectionC = Direction.Straight;
            ManoeuverDif = 100 + ((int) rarity*15);
        }

        public override void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
