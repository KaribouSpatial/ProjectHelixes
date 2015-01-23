using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ProjectHelix.Core;
using ProjectHelix.Core.Inputs;
using ProjectHelix.Data;
using ProjectHelix.Data.BoatComponents;
using ProjectHelix.Render;
using Microsoft.Xna.Framework.Input;

namespace ProjectHelix
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ProjectHelix : Game
    {
        readonly RenderManager _renderManager = RenderManager.Instance;
        readonly MainCore _core = MainCore.Instance;

        public ProjectHelix()
        {
            _core.Initialise();
            _renderManager.Initialise(this);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _renderManager.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            _core.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _renderManager.GameTime = gameTime;

            _core.Tree.AcceptVisitor(_renderManager);

            base.Draw(gameTime);
        }
    }
}
