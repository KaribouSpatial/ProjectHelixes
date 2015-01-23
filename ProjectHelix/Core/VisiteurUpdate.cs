using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using ProjectHelix.Core.Commands;
using ProjectHelix.Data;
using ProjectHelix.Data.BoatComponents;

namespace ProjectHelix.Core
{
    public class VisiteurUpdate : IVisitor
    {
        public float Time { get; set; }

        public void Visit(AbstractNode node)
        {
        }

        public void Visit(CompositeNode node)
        {
        }

        public void Visit(RenderingTree node)
        {
        }

        public void Visit(Hull node)
        {
        }

        public void Visit(Canons node)
        {
        }

        public void Visit(Sails node)
        {
        }

        public void Visit(Wheel node)
        {
        }

        public void Visit(Ship node)
        {
        }

        public async void Visit(Canonball node)
        {
            node.Position += (Time*node.Vitesse/5000);
            if ((node.Timer += Time) >= 1000.0f)
            {
                var cmd = new DestroyCmd {NodeToDestroy = node};
                MainCore.Instance.CommandQueue.Enqueue(cmd);
                return;
            }
            if (Time <= 1000)
                return;
            foreach (var player in MainCore.Instance.Players)
            {
                if (player.Inventory.Ship.Collide(node))
                {
                    player.Inventory.Ship.Hull.CurrentHp -= node.Damage;
                    var cmd2 = new DestroyCmd { NodeToDestroy = node };
                    MainCore.Instance.CommandQueue.Enqueue(cmd2);

                    if (player.Inventory.Ship.Hull.CurrentHp <= 0)
                    {
                        var cmd3 = new DestroyCmd { NodeToDestroy = node };
                         MainCore.Instance.CommandQueue.Enqueue(cmd3);

                        if (MainCore.Instance.LocalPlayer == player)
                        {
                            //Tests
                            //Pour linstant on perd toujours, vérifier la condition de fin de jeu
                            //if (e)
                            var endMessage = new MessageDialog("You lost ") { Title = "** Game over **" };
                            //else
                            //    endMessage = new MessageDialog("You won!! ");

                            //Premiere commande = Retry
                            endMessage.DefaultCommandIndex = 0;
                            //Troisieme commande = Main Menu
                            endMessage.CancelCommandIndex = 2;

                            await endMessage.ShowAsync();
                        }
                    }
                }
            }
        }
    }
}
