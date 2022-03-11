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
        private Texture2D StartButton, StartHover, CollectionButton, CollectionHover,
                            StarRotate,Menu_bg;
        private bool MouseOnStartButton, MouseOnCollectionButton;
        private float rotate = 0;
        private int counter = 0;
        private bool reRotate;
		public void Initial() {

		}
		public override void LoadContent() {
			base.LoadContent();
            Arial = Content.Load<SpriteFont>("Arial");
            StartButton = Content.Load<Texture2D>("MenuScreen/start_button");
            StartHover = Content.Load<Texture2D>("MenuScreen/start_button_hover");
            CollectionButton = Content.Load<Texture2D>("MenuScreen/collection_button");
            CollectionHover = Content.Load<Texture2D>("MenuScreen/collection_button_hover");
            StarRotate = Content.Load<Texture2D>("MenuScreen/star_rotate");
            Menu_bg = Content.Load<Texture2D>("MenuScreen/menu_bg");
			Initial();
		}
		public override void UnloadContent() {
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime) {
            // Save Current Mouse Position
            Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
            Singleton.Instance.MouseCurrent = Mouse.GetState();

            if(!reRotate){
                counter += 1;
                if(counter > 50){
                    rotate = 0;
                    reRotate = !reRotate;
                    counter = 0;
                }
            }

            if(reRotate){
                counter += 1;
                if(counter > 50){
                    rotate = 60;
                    reRotate = !reRotate;
                    counter = 0;
                }
            }

            // if(!reRotate){
            //     rotate += 3;
            //     if(rotate >= 360)
            //         reRotate = !reRotate;
            // }

            // if(reRotate){
            //     rotate -= 3;
            //     if(rotate <= 0)
            //         reRotate = !reRotate;
            // }

            // Check mouse on UI
            if(MouseOnElement(600, 680, 430,450)){
                MouseOnStartButton = true;
                if(IsClick()){
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                }
            } else {
                MouseOnStartButton = false;
            }
            if(MouseOnElement(570, 710, 520,540)){
                MouseOnCollectionButton = true;
                if(IsClick()){
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
                }
            } else {
                MouseOnCollectionButton = false;
            }

			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(Menu_bg, new Vector2(0, 0),Color.White);
            _spriteBatch.Draw(StarRotate, new Vector2(305, 230), null, Color.White, MathHelper.ToRadians(rotate) , new Vector2(StarRotate.Width / 2, StarRotate.Height/2), 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X , new Vector2(0,0), Color.Black);
            _spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
            _spriteBatch.DrawString(Arial, "Click ?  " + IsClick(), new Vector2(0, 60), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on Start ?  " + MouseOnStartButton, new Vector2(0, 80), Color.Black);
            _spriteBatch.DrawString(Arial, "Mouse on Collection ?  " + MouseOnCollectionButton, new Vector2(0, 100), Color.Black);
            _spriteBatch.DrawString(Arial, "Roatation ?  " + rotate, new Vector2(0, 120), Color.Black);

            // Swap Texture If mouseHover 
            if(MouseOnStartButton)
                _spriteBatch.Draw(StartHover, CenterElementWithHeight(StartHover,410) , Color.White);
            else
                _spriteBatch.Draw(StartButton, CenterElementWithHeight(StartButton,410) , Color.White);
            
            if(MouseOnCollectionButton)
                _spriteBatch.Draw(CollectionHover, CenterElementWithHeight(CollectionHover,500) , Color.White);
            else
                _spriteBatch.Draw(CollectionButton, CenterElementWithHeight(CollectionButton,500) , Color.White);
		}
        
        // if mouse on specify 'location/position'
        public bool MouseOnElement(int x1, int x2, int y1, int y2){
            return (Singleton.Instance.MouseCurrent.X > x1 && Singleton.Instance.MouseCurrent.Y > y1) && (Singleton.Instance.MouseCurrent.X < x2 && Singleton.Instance.MouseCurrent.Y < y2);
        }
        // helper function to shorten singleton calling
         public bool IsClick(){
            return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
        }
        // CenterElement at specify height
        public Vector2 CenterElementWithHeight(Texture2D element,int height){
            return new Vector2(Singleton.Instance.Dimension.X / 2 - (element.Width / 2) ,height );
        }
	}
}
