using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectHelix.Core;

namespace ProjectHelix.Data
{
    public class Canonball : AbstractNode
    {
        public Vector2 Vitesse { get; set; }
        public float Timer { get; set; }
        public int Damage { get; set; }

        public override void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
