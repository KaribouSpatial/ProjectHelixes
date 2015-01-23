using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.Xna.Framework;
using ProjectHelix.Core.Commands;
using ProjectHelix.Core.Inputs;
using ProjectHelix.Data;
using ProjectHelix.Core.GameState;
using ProjectHelix.Data.BoatComponents;
using SharpDX.Direct2D1;

namespace ProjectHelix.Core
{
    public sealed class MainCore
    {
        public static readonly MainCore Instance = new MainCore();
        public StateEngine StateEngine { get; set; }
        public List<Player> Players { get; set; }

        public Player LocalPlayer { get; private set; }

        public GameData Game { get; set; }

        public Int16 Turn { get; set; }

        public Queue<ICommand> CommandQueue { get; set; }
        private MainCore() { }

        private RenderingTree _tree = new RenderingTree();

        public RenderingTree Tree { get { return _tree; } }

        public enum GameStatus { Ingame, Lost, Win };

        public GameStatus CurrentGameStatus { get; set; }

        public int Deads { get; set; }

        public void Initialise()
        {
            Build();
        }

        public void Reset()
        {
            //Reset des shits
            _tree = new RenderingTree();
            Players = null;
            CommandQueue = null;
            Build();
        }

        private async void Build()
        {
            //Ajoute les objets

            Turn = 0;
            Deads = 0;

            IMobileServiceTable<GameData> gameTables = App.MobileService.GetTable<GameData>();
            Game = (await gameTables.Where(game => game.Id == GamePage.GameId).ToListAsync())[0];

            CommandQueue = new Queue<ICommand>();
            StateEngine = new StateEngine();
            //On recupere les players actifs
            Players = new List<Player>();
            foreach (var user in GamePage.UsersInGame)
            {
                Player player = new Player(user);
                Players.Add(player);

                if (user.Id == GamePage.LocalUser.Id)
                    LocalPlayer = player;
            }


            CurrentGameStatus = GameStatus.Ingame;

            var ordered = Players.OrderBy(player => player.UserLinked.Id);

            int i = 0;
            foreach (var player in ordered)
            {
                float x = 0;
                if (i%2 == 0)
                {
                    x = 100;
                    player.Inventory.Ship.ThetaDirection = Math.PI/2;
                }
                else
                {
                    x = 1820;
                    player.Inventory.Ship.ThetaDirection = - Math.PI / 2;
                }

                float y = (i/2)*200 + 100;
                   
                player.Inventory.Ship.InitPosition(new Vector2(x, y));

                _tree.AddNode(player.Inventory.Ship);
                ++i;
            }
        }

        public void Update(GameTime gameTime)
        {
            MyMouse.Instance.Update(gameTime);
            if(CommandQueue == null)
                CommandQueue = new Queue<ICommand>();
            while (CommandQueue.Count != 0)
            {
                CommandQueue.Dequeue().Execute();
            }

            if (CurrentGameStatus != GameStatus.Ingame)
                return;

            LogicLoop(gameTime);
        }

        private void LogicLoop(GameTime gameTime)
        {
            //TODO::
            if(StateEngine==null)
                StateEngine = new StateEngine();
            StateEngine.CurrentState.Update(gameTime);
        }

    }
}
