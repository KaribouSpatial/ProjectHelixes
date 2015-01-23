using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHelix.Core;

namespace ProjectHelix.Data.BoatComponents
{
    public class Bullets : AbstractNode
    {
        private Rarity _rarity;
        public Rarity Rarity
        {
            get { return _rarity; }
            set { _rarity = value; Damage = 2 + (int)value; }
        }

        public int Damage { get; private set; }

        public Bullets(Rarity rarity)
        {
            _rarity = rarity;
            Damage = 2 + (int) rarity;
        }

        public override void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
