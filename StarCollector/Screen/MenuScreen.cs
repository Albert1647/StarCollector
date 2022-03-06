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
        private Texture2D StartButton, StartHover, CollectionButton, CollectionHover ;

        private bool MouseOnStartButton, MouseOnCollectionButton, MouseOnMenuClick;
		public void Initial() {

		}
		public override void LoadContent() {
			base.LoadContent();
            Arial = Content.Load<SpriteFont>("Arial");
            StartButton = Content.Load<Texture2D>("MenuScreen/start_button");
            StartHover = Content.Load<Texture2D>("MenuScreen/start_button_hover");
            CollectionButton = Content.Load<Texture2D>("MenuScreen/collection_button");
            CollectionHover = Content.Load<Texture2D>("MenuScreen/collection_button_hover");
			Initial();
		}
		public override void UnloadContent() {
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime) {
            // Save Current Mouse Position
            Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
            Singleton.Instance.MouseCurrent = Mouse.GetState();

            // Check mouse on UI
            if(MouseOnElement(600, 680, 280,300)){
                MouseOnStartButton = true;
                if(IsClick()){
                    MouseOnMenuClick = true;
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                } else {
                    MouseOnMenuClick = false;
                }
            } else {
                MouseOnStartButton = false;
            }
            if(MouseOnElement(570, 710, 360,380)){
                MouseOnCollectionButton = true;
                if(IsClick()){
                    MouseOnMenuClick = true;
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
                } else {
                    MouseOnMenuClick = false;
                }
            } else {
                MouseOnCollectionButton = false;
            }

			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X , new Vector2(0,0), Color.Black);
            _spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
            _spriteBatch.DrawString(Arial, "Click ?  " + IsClick(), new Vector2(0, 60), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on menu ?  " + MouseOnStartButton, new Vector2(0, 80), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on menu and Click ?  " + MouseOnMenuClick, new Vector2(0, 100), Color.Black);
            // Swap Texture If mouseHover 
            if(MouseOnStartButton)
                _spriteBatch.Draw(StartHover, CenterElementWithHeight(StartHover,260) , Color.Black);
            else
                _spriteBatch.Draw(StartButton, CenterElementWithHeight(StartButton,260) , Color.Black);
            
            if(MouseOnCollectionButton)
                _spriteBatch.Draw(CollectionHover, CenterElementWithHeight(CollectionHover,340) , Color.Black);
            else
                _spriteBatch.Draw(CollectionButton, CenterElementWithHeight(CollectionButton,340) , Color.Black);
            
		}
        // if mouse on specify position
        public bool MouseOnElement(int x1, int x2, int y1, int y2){
            return (Singleton.Instance.MouseCurrent.X > x1 && Singleton.Instance.MouseCurrent.Y > y1) && (Singleton.Instance.MouseCurrent.X < x2 && Singleton.Instance.MouseCurrent.Y < y2);
        }
         public bool IsClick(){
            return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
        }
        // CenterElement at specify height
        public Vector2 CenterElementWithHeight(Texture2D element,int height){
            return new Vector2(Singleton.Instance.Dimension.X / 2 - (element.Width / 2) ,height );
        }
	}
}
