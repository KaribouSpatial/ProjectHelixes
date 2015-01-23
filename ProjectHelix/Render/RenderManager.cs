using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectHelix.Core;
using ProjectHelix.Core.GameState;
using ProjectHelix.Data;
using ProjectHelix.Data.BoatComponents;
using SharpDX.DirectWrite;

namespace ProjectHelix.Render
{
    public class RenderManager : IVisitor
    {
        public static readonly RenderManager Instance = new RenderManager();

        private RenderManager() { }

        private GraphicsDeviceManager _graphics;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        
        private SpriteFont _spriteFont;
        
        private Texture2D _cBoatTexture;
        private Texture2D _uBoatTexture;
        private Texture2D _rBoatTexture;
        private Texture2D _lBoatTexture;
        private Texture2D _cuCanonTexture;
        private Texture2D _rlCanonTexture;
        private Texture2D _cuSailsTexture;
        private Texture2D _rlSailsTexture;
        private Texture2D _cusWheelTexture;
        private Texture2D _curWheelTexture;
        private Texture2D _culWheelTexture;
        private Texture2D _rlsWheelTexture;
        private Texture2D _rlrWheelTexture;
        private Texture2D _rllWheelTexture;
        private Texture2D _bulletTexture;

        private Vector2 _originCBoatTexture;
        private Vector2 _originUBoatTexture;
        private Vector2 _originRBoatTexture;
        private Vector2 _originLBoatTexture;

        private Vector2 _originCUCanonTexture;
        private Vector2 _originRLCanonTexture;
        private Vector2 _originCUSailsTexture;
        private Vector2 _originRLSailsTexture;

        private Vector2 _originCUSWheelTexture;
        private Vector2 _originCURWheelTexture;
        private Vector2 _originCULWheelTexture;
        private Vector2 _originRLSWheelTexture;
        private Vector2 _originRLRWheelTexture;
        private Vector2 _originRLLWheelTexture;

        
        private readonly MainCore _theGame = MainCore.Instance;
        public GameTime GameTime { get; set; }

        public Vector2 GetViewport()
        {
            return new Vector2(_graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
        }

        public void Initialise(ProjectHelix game)
        {
            _graphics = new GraphicsDeviceManager(game)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080
            };
            _graphics.ApplyChanges();

            game.Content.RootDirectory = "Assets";
            _content = game.Content;

        }

        public void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

            //_spriteFont = _content.Load<SpriteFont>("Fonts/arial");
            //_spriteFont = _content.Load<SpriteFont>(@Fonts/kootenay");
            _cBoatTexture = _content.Load<Texture2D>(@"Textures/Ships/cBoat");
            _uBoatTexture = _content.Load<Texture2D>(@"Textures/Ships/uBoat");
            _rBoatTexture = _content.Load<Texture2D>(@"Textures/Ships/rBoat");
            _lBoatTexture = _content.Load<Texture2D>(@"Textures/Ships/lBoat");
            _cuCanonTexture = _content.Load<Texture2D>(@"Textures/Ships/cuCanon");
            _rlCanonTexture = _content.Load<Texture2D>(@"Textures/Ships/rlCanon");
            _cuSailsTexture = _content.Load<Texture2D>(@"Textures/Ships/cuSails");
            _rlSailsTexture = _content.Load<Texture2D>(@"Textures/Ships/rlSails");
            _cusWheelTexture = _content.Load<Texture2D>(@"Textures/Ships/cusWheel");
            _curWheelTexture = _content.Load<Texture2D>(@"Textures/Ships/curWheel");
            _culWheelTexture = _content.Load<Texture2D>(@"Textures/Ships/culWheel");
            _rlsWheelTexture = _content.Load<Texture2D>(@"Textures/Ships/rlsWheel");
            _rlrWheelTexture = _content.Load<Texture2D>(@"Textures/Ships/rlrWheel");
            _rllWheelTexture = _content.Load<Texture2D>(@"Textures/Ships/rllWheel");
            _bulletTexture = _content.Load<Texture2D>(@"Textures/Missiles/bullet");

            _originCBoatTexture = new Vector2(_cBoatTexture.Width / 2, _cBoatTexture.Height / 2);
            _originUBoatTexture = new Vector2(_uBoatTexture.Width / 2, _uBoatTexture.Height / 2);
            _originRBoatTexture = new Vector2(_rBoatTexture.Width / 2, _rBoatTexture.Height / 2);
            _originLBoatTexture = new Vector2(_lBoatTexture.Width / 2, _lBoatTexture.Height / 2);

            _originCUCanonTexture = new Vector2(_cuCanonTexture.Width / 2, _cuCanonTexture.Height / 2);
            _originRLCanonTexture = new Vector2(_rlCanonTexture.Width / 2, _rlCanonTexture.Height / 2);
            _originCUSailsTexture = new Vector2(_cuSailsTexture.Width / 2, _cuSailsTexture.Height / 2);
            _originRLSailsTexture = new Vector2(_rlSailsTexture.Width / 2, _rlSailsTexture.Height / 2);

            _originCUSWheelTexture = new Vector2(_cusWheelTexture.Width / 2, _cusWheelTexture.Height / 2);
            _originCURWheelTexture = new Vector2(_curWheelTexture.Width / 2, _curWheelTexture.Height / 2);
            _originCULWheelTexture = new Vector2(_culWheelTexture.Width / 2, _culWheelTexture.Height / 2);
            _originRLSWheelTexture = new Vector2(_rlsWheelTexture.Width / 2, _rlsWheelTexture.Height / 2);
            _originRLRWheelTexture = new Vector2(_rlrWheelTexture.Width / 2, _rlrWheelTexture.Height / 2);
            _originRLLWheelTexture = new Vector2(_rllWheelTexture.Width / 2, _rllWheelTexture.Height / 2);
        }

        private void DrawText()
        {
            //_spriteBatch.DrawString(_spriteFont, "Time before next move: " + (10000-GameState.Instance.ElapsedTime)/1000, new Vector2(20, 20), Color.Black);
        }

        public void Visit(RenderingTree node)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        public void Visit(CompositeNode node)
        {
        }

        public void Visit(AbstractNode node)
        {
        }

        public void Visit(Hull node)
        {
            _spriteBatch.Begin();
            //_spriteBatch.DrawString(_spriteFont,"caca",_originCBoatTexture,Color.Black);
            switch (node.Rarity)
            {
                case CompositeNode.Rarity.Common:
                    _spriteBatch.Draw(_cBoatTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originCBoatTexture, 1.0f, SpriteEffects.None, 0.0f);
                    break;
                case CompositeNode.Rarity.Uncommon:
                    _spriteBatch.Draw(_uBoatTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originUBoatTexture, 1.0f, SpriteEffects.None, 0.0f);
                    break;
                case CompositeNode.Rarity.Rare:
                    _spriteBatch.Draw(_rBoatTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originRBoatTexture, 1.0f, SpriteEffects.None, 0.0f);
                    break;
                case CompositeNode.Rarity.Legendary:
                    _spriteBatch.Draw(_lBoatTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originLBoatTexture, 1.0f, SpriteEffects.None, 0.0f);
                    break;
            }
            _spriteBatch.End();
            DrawText();
        }

        public void Visit(Canons node)
        {
            _spriteBatch.Begin();
            switch (node.Rarity)
            {
                case CompositeNode.Rarity.Common:
                case CompositeNode.Rarity.Uncommon:
                    _spriteBatch.Draw(_cuCanonTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originCUCanonTexture, 1.0f, SpriteEffects.None, 0.0f);
                    break;
                case CompositeNode.Rarity.Rare:
                case CompositeNode.Rarity.Legendary:
                    _spriteBatch.Draw(_rlCanonTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originRLCanonTexture, 1.0f, SpriteEffects.None, 0.0f);
                    break;
            }
            _spriteBatch.End();
        }

        public void Visit(Sails node)
        {
            _spriteBatch.Begin();
            switch (node.Rarity)
            {
                case CompositeNode.Rarity.Common:
                case CompositeNode.Rarity.Uncommon:
                    _spriteBatch.Draw(_cuSailsTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originCUSailsTexture, 1.0f, SpriteEffects.None, 0.0f);
                    break;
                case CompositeNode.Rarity.Rare:
                case CompositeNode.Rarity.Legendary:
                    _spriteBatch.Draw(_rlSailsTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originRLSailsTexture, 1.0f, SpriteEffects.None, 0.0f);
                    break;
            }
            _spriteBatch.End();
        }

        public void Visit(Wheel node)
        {
            _spriteBatch.Begin();
            switch (node.Rarity)
            {
                case CompositeNode.Rarity.Common:
                case CompositeNode.Rarity.Uncommon:
                    switch (node.DirectionC)
                    {
                        case Wheel.Direction.Straight:
                            _spriteBatch.Draw(_cusWheelTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originCUSWheelTexture, 1.0f, SpriteEffects.None, 0.0f);
                            break;
                        case Wheel.Direction.Right:
                            _spriteBatch.Draw(_curWheelTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originCURWheelTexture, 1.0f, SpriteEffects.None, 0.0f);
                            break;
                        case Wheel.Direction.Left:
                            _spriteBatch.Draw(_culWheelTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originCULWheelTexture, 1.0f, SpriteEffects.None, 0.0f);
                            break;
                    }
                    break;
                case CompositeNode.Rarity.Rare:
                case CompositeNode.Rarity.Legendary:
                    switch (node.DirectionC)
                    {
                        case Wheel.Direction.Straight:
                            _spriteBatch.Draw(_rlsWheelTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originRLSWheelTexture, 1.0f, SpriteEffects.None, 0.0f);
                            break;
                        case Wheel.Direction.Right:
                            _spriteBatch.Draw(_rlrWheelTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originRLRWheelTexture, 1.0f, SpriteEffects.None, 0.0f);
                            break;
                        case Wheel.Direction.Left:
                            _spriteBatch.Draw(_rllWheelTexture, node.Position, null, Color.White, ((Ship)node.Daddy).AngleForDirection, _originRLLWheelTexture, 1.0f, SpriteEffects.None, 0.0f);
                            break;
                    }
                    break;
            }
            _spriteBatch.End();
        }

        public void Visit(Ship node)
        {
            Visit(node.Hull);
            Visit(node.Canons);
            Visit(node.Sails);
            Visit(node.Wheel);
        }

        public void Visit(Canonball node)
        {
            //TODO DRAW LES CANONBALL
            //SAUACE AU PAIN, WATCHOUT
            _spriteBatch.Begin();
            _spriteBatch.Draw(_bulletTexture, node.Position, null, Color.White, 0, _originCUCanonTexture, 1.0f, SpriteEffects.None, 0.0f);
            _spriteBatch.End();
            

        }
    }
}
