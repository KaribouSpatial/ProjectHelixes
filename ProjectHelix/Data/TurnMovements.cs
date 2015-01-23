using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelix.Data
{
    class TurnMovements
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string GameId { get; set; }

        public Int16 Turn { get; set; }

        public float DestinationX { get; set; }

        public float DestinationY { get; set; }

        public Int16 SpellId { get; set; }
    }
}
