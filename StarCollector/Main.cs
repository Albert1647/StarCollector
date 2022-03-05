using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StarCollector
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont Arial;
        private Texture2D startButton;
        private Texture2D startHover;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = (int)Singleton.Instance.Dimension.X;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = (int)Singleton.Instance.Dimension.Y;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Arial = Content.Load<SpriteFont>("Alien");

            startButton = Content.Load<Texture2D>("MenuScreen/start_button");
            startHover = Content.Load<Texture2D>("MenuScreen/start_button_hovering");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.Dimension.X , new Vector2(0,0), Color.Black);
            _spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.Dimension.Y, new Vector2(0, 40), Color.Black);
            _spriteBatch.Draw(startButton, new Vector2(Singleton.Instance.Dimension.X / 2 - startButton.Width , Singleton.Instance.Dimension.Y / 2) , Color.Black);
            _spriteBatch.Draw(startHover, new Vector2(Singleton.Instance.Dimension.X / 2 - startButton.Width , Singleton.Instance.Dimension.Y / 2) , Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
