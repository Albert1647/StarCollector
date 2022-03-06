﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarCollector.Managers;

namespace StarCollector
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Apply Game resolution
            _graphics.PreferredBackBufferWidth = (int)Singleton.Instance.Dimension.X;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = (int)Singleton.Instance.Dimension.Y;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.LoadContent(Content);
        }

        protected override void UnloadContent() {
			ScreenManager.Instance.UnloadContent();
		}

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Update logic base on CurrentScreen

            ScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // Draw Screen base on CurrentScreen
            ScreenManager.Instance.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        


    }
}
