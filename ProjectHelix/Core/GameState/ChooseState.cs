using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHelix.Core.Inputs;

namespace ProjectHelix.Core.GameState
{
    class ChooseState : GameState
    {

        public ChooseState()
        {
            
        }

        public override void ProcessMousePressed()
        {
            MainCore.Instance.LocalPlayer.Inventory.Ship.TargetPoint = MyMouse.ConvertToWorldCoord(MyMouse.Instance.CurrentPos);
        }
        public override void ProcessMouseRelease()
        {
            MainCore.Instance.LocalPlayer.Inventory.Ship.TargetPoint = MyMouse.ConvertToWorldCoord(MyMouse.Instance.CurrentPos);
        }
        public override void ProcessMouseDrag()
        {
            MainCore.Instance.LocalPlayer.Inventory.Ship.TargetPoint = MyMouse.ConvertToWorldCoord(MyMouse.Instance.CurrentPos);
        }
        public override void ProcessMouseMove()
        {
            
        }

        public override void Update(GameTime time)
        {
            ElapsedTime += time.ElapsedGameTime.Milliseconds;
            if (ElapsedTime >= 10000)
            {
                MainCore.Instance.StateEngine.CurrentState = new WaitState();
            }

            ////TODO:: Gestion des commandes 
            //Instance = new WaitState();


            ////Trop lent, commande par defaut
            //if (true)
            //{
            //    //TODO:: Envoyer une commande par defaut

            //    //Montrer les mouvements
            //    Instance = new ShowMovesState();
            //}

        }
    }
}
