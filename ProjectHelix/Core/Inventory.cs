using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Microsoft.Xna.Framework;
using ProjectHelix.Data;
using ProjectHelix.Data.BoatComponents;


namespace ProjectHelix.Core
{
    public class Inventory
    {
        public Ship Ship { get; set; }

        public Inventory(User user)
        {
            AbstractNode.Rarity hull = AbstractNode.Rarity.Common;
            switch(user.HullR)
            {
                case "Common":
                    hull = AbstractNode.Rarity.Common;
                    break;
                case "Uncommon":
                    hull = AbstractNode.Rarity.Uncommon;
                    break;
                case "Rare":
                    hull = AbstractNode.Rarity.Rare;
                    break;
                case "Legendary":
                    hull = AbstractNode.Rarity.Legendary;
                    break;
            }

            AbstractNode.Rarity canons = AbstractNode.Rarity.Common;
            switch(user.HullR)
            {
                case "Common":
                    canons = AbstractNode.Rarity.Common;
                    break;
                case "Uncommon":
                    canons = AbstractNode.Rarity.Uncommon;
                    break;
                case "Rare":
                    canons = AbstractNode.Rarity.Rare;
                    break;
                case "Legendary":
                    canons = AbstractNode.Rarity.Legendary;
                    break;
            }

            AbstractNode.Rarity bullets = AbstractNode.Rarity.Common;
            switch(user.HullR)
            {
                case "Common":
                    bullets = AbstractNode.Rarity.Common;
                    break;
                case "Uncommon":
                    bullets = AbstractNode.Rarity.Uncommon;
                    break;
                case "Rare":
                    bullets = AbstractNode.Rarity.Rare;
                    break;
                case "Legendary":
                    bullets = AbstractNode.Rarity.Legendary;
                    break;
            }

            AbstractNode.Rarity sails = AbstractNode.Rarity.Common;
            switch(user.HullR)
            {
                case "Common":
                    sails = AbstractNode.Rarity.Common;
                    break;
                case "Uncommon":
                    sails = AbstractNode.Rarity.Uncommon;
                    break;
                case "Rare":
                    sails = AbstractNode.Rarity.Rare;
                    break;
                case "Legendary":
                    sails = AbstractNode.Rarity.Legendary;
                    break;
            }

            AbstractNode.Rarity wheel = AbstractNode.Rarity.Common;
            switch(user.HullR)
            {
                case "Common":
                    wheel = AbstractNode.Rarity.Common;
                    break;
                case "Uncommon":
                    wheel = AbstractNode.Rarity.Uncommon;
                    break;
                case "Rare":
                    wheel = AbstractNode.Rarity.Rare;
                    break;
                case "Legendary":
                    wheel = AbstractNode.Rarity.Legendary;
                    break;
            }

            Ship = new Ship()
            {
                Hull = new Hull(hull),
                Canons = new Canons(canons),
                Bullets = new Bullets(bullets),
                Sails = new Sails(sails),
                Wheel = new Wheel(wheel)
            };

            Ship.Hull.Daddy = Ship;
            Ship.Canons.Daddy = Ship;
            Ship.Bullets.Daddy = Ship;
            Ship.Sails.Daddy = Ship;
            Ship.Wheel.Daddy = Ship;
        }
    }
}
