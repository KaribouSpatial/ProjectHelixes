using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.Xna.Framework;
using ProjectHelix.Data;

namespace ProjectHelix.Core.GameState
{
    class WaitState : GameState
    {
        public WaitState()
        {
            SendTurn();
        }

        public async void SendTurn()
        {
            TurnMovements move = new TurnMovements()
            {
                DestinationX = MainCore.Instance.LocalPlayer.Inventory.Ship.TargetPoint.X,
                DestinationY = MainCore.Instance.LocalPlayer.Inventory.Ship.TargetPoint.Y,
                GameId = MainCore.Instance.Game.Id,
                SpellId = 0,
                Turn = MainCore.Instance.Turn,
                UserId = MainCore.Instance.LocalPlayer.UserLinked.Id
            };

            IMobileServiceTable<TurnMovements> moveTables = App.MobileService.GetTable<TurnMovements>();

            await moveTables.InsertAsync(move);
        }

        public override void ProcessMousePressed()
        {
            
        }
        public override void ProcessMouseRelease()
        {
            
        }

        public override void ProcessMouseDrag()
        {
            
        }

        public override void ProcessMouseMove()
        {
            
        }

        public async override void Update(GameTime time)
        {
            IMobileServiceTable<TurnMovements> moveTables = App.MobileService.GetTable<TurnMovements>();
            List<TurnMovements> nextMoves = await moveTables.Where(move => move.GameId == MainCore.Instance.Game.Id
                                                                            && move.Turn == MainCore.Instance.Turn)
                .ToListAsync();


            if (nextMoves.Count >= MainCore.Instance.Game.NbPlayers - MainCore.Instance.Deads)
            {
                //TOUT LE MONDE EST LA

                foreach (var player in MainCore.Instance.Players)
                {
                    var turn = nextMoves.Find(move => move.UserId == player.UserLinked.Id);

                    player.Inventory.Ship.TargetPoint = new Vector2(turn.DestinationX, turn.DestinationY);
                    //turn.SpellId
                    var delta = player.Inventory.Ship.TargetPoint - player.Inventory.Ship.Position;
                    delta /= 5000;
                    player.Inventory.Ship.ThetaDirection = (Math.Abs(delta.Y) > 0.00000001f ? Math.Atan(delta.X / delta.Y) : 0.0f);

                    player.Inventory.Ship.Delta = delta;
                }

                MainCore.Instance.StateEngine.CurrentState = new ShowMovesState();
            }

            
        }
    }
}
