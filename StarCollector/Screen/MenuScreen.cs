using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarCollector.Managers;
using System;
using Microsoft.Xna.Framework.Audio;

namespace StarCollector.Screen {
	class MenuScreen : _GameScreen {
        private SpriteFont Arial;
        private Texture2D startButton;
        private Texture2D startHover;

        private bool mouseOnMenu, mouseOnMenuClick;
		public void Initial() {

		}
		public override void LoadContent() {
			base.LoadContent();
            Arial = Content.Load<SpriteFont>("Alien");
            startButton = Content.Load<Texture2D>("MenuScreen/start_button");
            startHover = Content.Load<Texture2D>("MenuScreen/start_button_hovering");
			Initial();
		}
		public override void UnloadContent() {
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime) {
            // Save Current Mouse Position
            Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
            Singleton.Instance.MouseCurrent = Mouse.GetState();

            if(mouseOnElement(600, 680, 360,380)){
                mouseOnMenu = true;
                if(isClick()){
                    mouseOnMenuClick = true;
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                } else {
                    mouseOnMenuClick = false;
                }
            } else {
                mouseOnMenu = false;
            }

			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X , new Vector2(0,0), Color.Black);
            _spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
            _spriteBatch.DrawString(Arial, "Click ?  " + isClick(), new Vector2(0, 60), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on menu ?  " + mouseOnMenu, new Vector2(0, 80), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on menu and Click ?  " + mouseOnMenuClick, new Vector2(0, 100), Color.Black);
            _spriteBatch.Draw(startButton, centerElement(startButton) , Color.Black);
            if(mouseOnMenu){
                _spriteBatch.Draw(startHover, centerElement(startHover) , Color.Black);
            }

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
