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

        private bool mouseOnMenu, mouseOnMenuClick;

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
            // Save Current Mouse Position
            Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
            Singleton.Instance.MouseCurrent = Mouse.GetState();

            // Check If mouse on an element
            if(mouseOnElement(600, 680, 360,380)){
                mouseOnMenu = true;
                if(isClick()){
                    mouseOnMenuClick = true;
                    

                } else {
                    mouseOnMenuClick = false;
                }
            } else {
                mouseOnMenu = false;
            }




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            _spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X , new Vector2(0,0), Color.Black);
            _spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
            _spriteBatch.DrawString(Arial, "Click ?  " + isClick(), new Vector2(0, 60), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on menu ?  " + mouseOnMenu, new Vector2(0, 80), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on menu and Click ?  " + mouseOnMenuClick, new Vector2(0, 100), Color.Black);
            _spriteBatch.Draw(startButton, centerElement(startButton) , Color.Black);
            if(mouseOnMenu){
                _spriteBatch.Draw(startHover, centerElement(startHover) , Color.Black);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool isClick(){
            return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
        }
        public Vector2 centerElement(Texture2D element){
            return new Vector2(Singleton.Instance.Dimension.X / 2 - (element.Width / 2) , Singleton.Instance.Dimension.Y / 2);
        }

        public bool mouseOnElement(int x1, int x2, int y1, int y2){
            return (Singleton.Instance.MouseCurrent.X > x1 && Singleton.Instance.MouseCurrent.Y > y1) && (Singleton.Instance.MouseCurrent.X < x2 && Singleton.Instance.MouseCurrent.Y < y2);
        }
    }
}
