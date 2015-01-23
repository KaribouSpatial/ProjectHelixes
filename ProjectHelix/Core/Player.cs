using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectHelix.Core.Commands;
using ProjectHelix.Data;
using ProjectHelix.Data.BoatComponents;
using SharpDX.Direct3D11;

namespace ProjectHelix.Core
{
    public class Player
    {

        public Player(User user)
        {
            UserLinked = user;
            Inventory = new Inventory(user);  
        }

        public User UserLinked { get; private set; }
        public Inventory Inventory { get; private set; }
    }
}
