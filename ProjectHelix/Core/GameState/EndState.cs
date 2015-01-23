using System;
using Microsoft.Xna.Framework;

namespace ProjectHelix.Core.GameState
{
    class EndState : GameState
    {
        public override void ProcessMousePressed()
        {
            throw new NotImplementedException();
        }
        public override void ProcessMouseRelease()
        {
            throw new NotImplementedException();
        }

        public override void ProcessMouseDrag()
        {
            throw new NotImplementedException();
        }

        public override void ProcessMouseMove()
        {
            throw new NotImplementedException();
        }
        public override void Update(GameTime time)
        {
            //Recevoir les mouvements et continuer a afficher le jeu
            //TODO:: faire un peu comme ShowMovesState

            //TODO:: Retourner au menu
        }
    }
}
