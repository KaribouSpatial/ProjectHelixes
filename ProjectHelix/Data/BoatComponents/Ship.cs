using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectHelix.Core;
using SharpDX.Direct2D1;

namespace ProjectHelix.Data.BoatComponents
{
    public class Ship : CompositeNode
    {
        public void Fire()
        {
            int[] tableau = {65,83,97,115};
            for (int i = 0; i < 8; ++i)
            {
                double theta;
                if(i < 4)
                    theta = ThetaDirection + tableau[i];
                else
                    theta = ThetaDirection - tableau[i - 4];
                var vitesse = new Vector2((float)(720 * Math.Sin(theta)), (float)(720 * Math.Cos(theta)));
                var canonball = new Canonball { Position = Position, Vitesse = vitesse , Damage = Bullets.Damage};
                MainCore.Instance.Tree.AddNode(canonball);
            }
        }

        public const int Rayon = 30;

        private Vector2 _targetPoint = new Vector2(0.0f,0.0f);
        public Vector2 TargetPoint
        {
            get { return _targetPoint; }
            set { _targetPoint = GetClosestPointInArea(value); }
        }

        public double ThetaDirection { get; set; }

        public float AngleForDirection
        {
            get
            {
                float angle = (float)(Math.PI - ThetaDirection);
                if (Delta.Y < 0)
                    angle += (float) Math.PI;
                return angle;
            }
        }

        public Vector2 Delta { get; set; }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Hull.Position = value;
                Wheel.Position = value;
                Canons.Position = value;
                Sails.Position = value;
            }
        }

        public bool Collide(Canonball canonball)
        {
            if (Math.Abs(canonball.Position.X - Position.X) < Rayon && Math.Abs(canonball.Position.Y - Position.Y) < Rayon)
            {
                return true;
            }
            return false;
        }
        public bool Collide(Ship ship)
        {
            if (Math.Abs(ship.Position.X - Position.X) < 2*Rayon && Math.Abs(ship.Position.Y - Position.Y) < 2*Rayon)
            {
                return true;
            }
            return false;
        }

        private Vector2 GetClosestPointInArea(Vector2 targetPointBeforeAdjust)
        {
            var droite = targetPointBeforeAdjust - Position;
            var newDroite = new Vector2((float) (droite.X*Math.Cos(ThetaDirection) - droite.Y*Math.Sin(ThetaDirection)),(float) (droite.X*Math.Sin(ThetaDirection) - droite.Y*Math.Cos(ThetaDirection)));
            var pente = newDroite.Y / newDroite.X;
            var root = (float)Math.Sqrt(Math.Pow(pente, 2) + 4 * Sails.Speed * 120 );
            float xPrime = 0;
            if (newDroite.X < 0)
                xPrime = (-(pente + root)/2);
            else
                xPrime = -(pente - root) / 2;

            var yPrime = pente*xPrime;

            var pointRelativeToShip =
                new Vector2((float) (xPrime*Math.Cos(-1*ThetaDirection) - yPrime*Math.Sin(-1*ThetaDirection)),
                    (float) (xPrime*Math.Sin(-1*ThetaDirection) - yPrime*Math.Cos(-1*ThetaDirection)));

            //return pointRelativeToShip + Position;
            return targetPointBeforeAdjust;
        }

        public Hull Hull { get; set; }
        public Canons Canons { get; set; }
        public Bullets Bullets { get; set; }
        public Sails Sails { get; set; }
        public Wheel Wheel { get; set; }

        public Ship()
        {
            ThetaDirection = 0.0;
        }

        public void InitPosition(Vector2 pos)
        {
            Position = pos;
            _targetPoint = pos;
        }

        public override void AcceptVisitor(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
