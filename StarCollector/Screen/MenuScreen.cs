using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarCollector.Managers;
using StarCollector.GameObjects;
using System;
using Microsoft.Xna.Framework.Audio;

namespace StarCollector.Screen {
	class MenuScreen : _GameScreen {
        private SpriteFont Arial;
        private Texture2D startButton, startHover, collectionButton, collectionHover ;

        private bool mouseOnstartButton, mouseOncollectionButton, mouseOnMenuClick;
		public void Initial() {

		}
		public override void LoadContent() {
			base.LoadContent();
            Arial = Content.Load<SpriteFont>("Arial");
            startButton = Content.Load<Texture2D>("MenuScreen/start_button");
            startHover = Content.Load<Texture2D>("MenuScreen/start_button_hover");
            collectionButton = Content.Load<Texture2D>("MenuScreen/collection_button");
            collectionHover = Content.Load<Texture2D>("MenuScreen/collection_button_hover");
			Initial();
		}
		public override void UnloadContent() {
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime) {
            // Save Current Mouse Position
            Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
            Singleton.Instance.MouseCurrent = Mouse.GetState();

            if(mouseOnElement(600, 680, 280,300)){
                mouseOnstartButton = true;
                if(isClick()){
                    mouseOnMenuClick = true;
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                } else {
                    mouseOnMenuClick = false;
                }
            } else {
                mouseOnstartButton = false;
            }
            if(mouseOnElement(570, 710, 360,380)){
                mouseOncollectionButton = true;
                if(isClick()){
                    mouseOnMenuClick = true;
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
                } else {
                    mouseOnMenuClick = false;
                }
            } else {
                mouseOncollectionButton = false;
            }

			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X , new Vector2(0,0), Color.Black);
            _spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
            _spriteBatch.DrawString(Arial, "Click ?  " + isClick(), new Vector2(0, 60), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on menu ?  " + mouseOnstartButton, new Vector2(0, 80), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on menu and Click ?  " + mouseOnMenuClick, new Vector2(0, 100), Color.Black);
            if(mouseOnstartButton){
                _spriteBatch.Draw(startHover, centerElementWithHeight(startHover,260) , Color.Black);
            }else{
                _spriteBatch.Draw(startButton, centerElementWithHeight(startButton,260) , Color.Black);
                }
            if(mouseOncollectionButton){
                _spriteBatch.Draw(collectionHover, centerElementWithHeight(collectionHover,340) , Color.Black);
            }else{
                _spriteBatch.Draw(collectionButton, centerElementWithHeight(collectionButton,340) , Color.Black);
                }
		}
        public bool mouseOnElement(int x1, int x2, int y1, int y2){
            return (Singleton.Instance.MouseCurrent.X > x1 && Singleton.Instance.MouseCurrent.Y > y1) && (Singleton.Instance.MouseCurrent.X < x2 && Singleton.Instance.MouseCurrent.Y < y2);
        }
         public bool isClick(){
            return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
        }
        public Vector2 centerElement(Texture2D element){
            return new Vector2(Singleton.Instance.Dimension.X / 2 - (element.Width / 2) , Singleton.Instance.Dimension.Y / 2);
        }
        public Vector2 centerElementWithHeight(Texture2D element,int height){
            return new Vector2(Singleton.Instance.Dimension.X / 2 - (element.Width / 2) ,height );
        }
	}
}
