using System;
using Microsoft.Xna.Framework;
using ProjectHelix.Data.BoatComponents;

namespace ProjectHelix.Core.GameState
{
    class ShowMovesState : GameState
    {
        public ShowMovesState()
        {
            _currentCheckpoint = 0;
            TurnFraction = 1;
        }

        public int TurnFraction { get; set; }

        private int _timeInTurn = 0;
        private int _currentCheckpoint = 0;
        private readonly int[] _checkpoints = {25,50,75,100};
        private const int MaxTime = 5000;
        
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

        public override void Update(GameTime time)
        {
            int deltaTemps = time.ElapsedGameTime.Milliseconds;
            if (_timeInTurn + deltaTemps > MaxTime)
                deltaTemps = MaxTime - _timeInTurn;
            _timeInTurn += deltaTemps;

            var v = new VisiteurUpdate {Time = deltaTemps};
            MainCore.Instance.Tree.AcceptVisitor(v);

            foreach (var player in MainCore.Instance.Players)
            {
                player.Inventory.Ship.Position += player.Inventory.Ship.Delta*deltaTemps;
            }

            if (_currentCheckpoint < 4 && _timeInTurn >= _checkpoints[_currentCheckpoint])
            {
                MainCore.Instance.Players[0].Inventory.Ship.Fire();
                ++_currentCheckpoint;
            }
            if (_timeInTurn >= MaxTime)
            {
                ++MainCore.Instance.Turn;
                MainCore.Instance.StateEngine.CurrentState = new ChooseState();
            }
        }
    }
}
