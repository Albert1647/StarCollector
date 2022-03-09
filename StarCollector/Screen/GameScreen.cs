using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarCollector.GameObjects;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using StarCollector.Managers;
using Microsoft.Xna.Framework.Media;

namespace StarCollector.Screen {
    class GameScreen : _GameScreen {
		private Texture2D GunTexture,StarTexture,Indicator,BG,StarDiscover,WinWindow,LoseWindow,continue_button,continue_button_hover,mainmenu_button_hover,mainmenu_button;
		private Gun gun;
        private Random random = new Random();

        private float Timer = 0f;
		public Star[,] star = new Star[23,8];

		private SpriteFont Arial,scoreFont;
        private int startLengthRow = 3;

        private bool gameOver,gameWin;

        private int leftWallX = 326;
        // private int rightWallX = 600;
        private bool gameComplete;

         private bool MouseOnMainButton;

        public void Initial() {
			// Instantiate gun on start GameScreen 
            gun = new Gun(GunTexture, Indicator, StarTexture) {
				pos = new Vector2(Singleton.Instance.Dimension.X / 2 - GunTexture.Width / 2, 700 - GunTexture.Height),
				_gunColor = Color.White
            };

             switch(Singleton.Instance.currentLevel){
                case 1:
                    Singleton.Instance.ceilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    Singleton.Instance.oldCeilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    for(int i = 0 ; i < getRowOfCurrentLevel(); i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = Singleton.Instance.GetColor()
                        };
                    }
                }
                break;
                case 2:
                    Singleton.Instance.ceilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    Singleton.Instance.oldCeilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    for(int i = 0 ; i < getRowOfCurrentLevel(); i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = Singleton.Instance.GetColor()
                        };
                    }
                }
                break;
                case 3:
                    Singleton.Instance.ceilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    Singleton.Instance.oldCeilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    for(int i = 0 ; i < getRowOfCurrentLevel(); i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = Singleton.Instance.GetColor()
                        };
                    }
                }
                break;
                case 4:
                    Singleton.Instance.ceilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    Singleton.Instance.oldCeilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    for(int i = 0 ; i < getRowOfCurrentLevel(); i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = Singleton.Instance.GetColor()
                        };
                    }
                }
                break;
                case 5:
                    Singleton.Instance.ceilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    Singleton.Instance.oldCeilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    for(int i = 0 ; i < getRowOfCurrentLevel(); i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = Singleton.Instance.GetColor()
                        };
                    }
                }
                break;
                case 6:
                    Singleton.Instance.ceilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    Singleton.Instance.oldCeilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
                    for(int i = 0 ; i < getRowOfCurrentLevel(); i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = Singleton.Instance.GetColor()
                        };
                    }
                }

                break;
            }
        }

        public override void LoadContent() {
            // Load Resource
            base.LoadContent();
			Arial = Content.Load<SpriteFont>("Arial");
            scoreFont = Content.Load<SpriteFont>("score");
            GunTexture = Content.Load<Texture2D>("gameScreen/gun");
			StarTexture = Content.Load<Texture2D>("gameScreen/star");
			Indicator = Content.Load<Texture2D>("gameScreen/indicator");
			BG = Content.Load<Texture2D>("gameScreen/ingame_bg");
            WinWindow = Content.Load<Texture2D>("gameScreen/win");
            LoseWindow = Content.Load<Texture2D>("gameScreen/Lose");
            mainmenu_button = Content.Load<Texture2D>("gameScreen/mainmenu_button");
            continue_button = Content.Load<Texture2D>("gameScreen/continue_button");
            
            switch(Singleton.Instance.currentLevel){
                case 1:
                    StarDiscover = Content.Load<Texture2D>("gameScreen/star_in_discover");
                break;
                case 2:
                    StarDiscover = Content.Load<Texture2D>("gameScreen/gun");
                break;
                case 3:
                    StarDiscover = Content.Load<Texture2D>("gameScreen/star_in_discover");
                break;
                case 4:
                    StarDiscover = Content.Load<Texture2D>("gameScreen/star_in_discover");
                break;
                case 5:
                    StarDiscover = Content.Load<Texture2D>("gameScreen/star_in_discover");
                break;
                case 6:
                    StarDiscover = Content.Load<Texture2D>("gameScreen/star_in_discover");
                break;
            }
            Initial();
        }
        public override void UnloadContent() {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime) {
            // Update star
            if (!gameWin && !gameOver) {
                for(int i = 0 ; i < star.GetLength(0) ; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        if (star[i, j] != null){
                            // If ceiling is moving
                            if(Singleton.Instance.oldCeilingY < Singleton.Instance.ceilingY)
                                star[i,j].pos.Y += Singleton.Instance.ceilingY - Singleton.Instance.oldCeilingY;
                            star[i,j].Update(gameTime, star);
                        }
                    }
                }
                // update/load gun logic
                gun.Update(gameTime, star);
                
                Singleton.Instance.oldCeilingY = Singleton.Instance.ceilingY;
                Timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                if(Timer >= getTimePerUpdate()){
                    Singleton.Instance.ceilingY += StarTexture.Height;
                    Timer -= getTimePerUpdate();
                }
                // if star reach bottom boundary -> GameOver
                for(int i = 0 ; i < star.GetLength(0) ; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        if (star[i, j] != null){
                            if((star[i,j].pos.Y + StarTexture.Width) > 620){
                                gameOver = true;
                            } else {
                                gameOver = false;
                            }
                        }
                    }
                }
                gameWin = true;
                for(int i = 0 ; i < star.GetLength(0) ; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        if (star[i, j] != null){
                            gameWin = false;
                        } 
                    }
                }
                    if (gameWin == true) {                       
                        if (Singleton.Instance.currentLevel < 6 )
                            {
                                Singleton.Instance.currentLevel += 1;
                                Singleton.Instance.ceilingY = 30;
                                Singleton.Instance.oldCeilingY = 30;
                                if(Singleton.Instance.clearStar < 6)
                                {
                                    Singleton.Instance.clearStar += 1;
                                }
                                ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                            } else {
                                gameComplete = true;
                            }                        
                        }
            }
             else {
                Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
                Singleton.Instance.MouseCurrent = Mouse.GetState();
                if(MouseOnElement(395,548,435,452)) {
                    MouseOnMainButton = true;
                    if(IsClick()){
                        ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                    }
                } else {
                        MouseOnMainButton = false;
                    }
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch) {
			_spriteBatch.Draw(BG, Vector2.Zero, Color.White);
            _spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X , new Vector2(0,0), Color.Black);
            _spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
			_spriteBatch.DrawString(Arial, "Is Shooting = " + Singleton.Instance.IsShooting , new Vector2(0,60), Color.Black);
            _spriteBatch.DrawString(Arial, "Ceiling = " + Singleton.Instance.ceilingY, new Vector2(0, 160), Color.Black);
            _spriteBatch.DrawString(Arial, "game is over ?????? = " + gameOver, new Vector2(0, 180), Color.Black);
            _spriteBatch.DrawString(Arial, "game is win ?????? = " + gameWin, new Vector2(0, 200), Color.Black);
            _spriteBatch.DrawString(Arial, "Time : " + Timer.ToString("F"), new Vector2(20, 260), Color.Black);
            _spriteBatch.DrawString(Arial, "MouseOnMainButton ?????? = " + MouseOnMainButton, new Vector2(0, 220), Color.Black);

            // draw star
            for (int i = 0; i < star.GetLength(0); i++) {
                for (int j = 0; j < star.GetLength(1); j++) {
                    if (star[i, j] != null)
                        star[i, j].Draw(_spriteBatch);
                }
            }
            // draw gun
			gun.Draw(_spriteBatch);

            // draw score
            _spriteBatch.DrawString(scoreFont, "score  " + Singleton.Instance.Score, new Vector2(50, 50), Color.Black);

            // check level
            _spriteBatch.DrawString(Arial, "level = " + Singleton.Instance.currentLevel, new Vector2(0, 300), Color.Black);

            _spriteBatch.Draw(StarDiscover, new Vector2(1120, 110),Color.White);

             if ( gameWin ) {
                // draw WinWindow
                _spriteBatch.Draw(WinWindow, new Vector2(325, 100),Color.White);
                 _spriteBatch.Draw(mainmenu_button, new Vector2(272, 412),Color.White);
                 _spriteBatch.Draw(continue_button, new Vector2(600, 412),Color.White);
            }  //gameOver == true
            if (gameOver == true)
            {
                // draw LoseWindow
                _spriteBatch.Draw(LoseWindow, new Vector2(325, 100),Color.White);
                 _spriteBatch.Draw(mainmenu_button, new Vector2(272, 412),Color.White);
                _spriteBatch.Draw(continue_button, new Vector2(600, 412),Color.White);
            }

        }
        public int GetStartCeilingY(int row, int show){
			return ((Singleton.Instance.STARHITBOX * show) + 30) - (row * (Singleton.Instance.STARHITBOX));
		}


        public float getTimePerUpdate(){
            switch(Singleton.Instance.currentLevel){
                case 1:
                    return 12f;
                case 2:
                    return 12f;
                case 3:
                    return 10f;
                case 4:
                    return 10f;
                case 5:
                    return 9f;
                case 6:
                    return 9f;
                default:
                    return 12f;
            }
        }
        public int getRowOfCurrentLevel(){
            switch(Singleton.Instance.currentLevel){
                case 1:
                    return 8;
                case 2:
                    return 10;
                case 3:
                    return 11;
                case 4:
                    return 13;
                case 5:
                    return 14;
                case 6:
                    return 16;
                default:
                    return 8;
            }
        }
        public int getShowRowOfCurrentLevel(){
            switch(Singleton.Instance.currentLevel){
                case 1:
                    return 4;
                case 2: case 3:
                    return 5;
                case 4: case 5:
                    return 6;
                case 6:
                    return 7;
                default:
                    return 4;
            }
        }
         public bool MouseOnElement(int x1, int x2, int y1, int y2){
            return (Singleton.Instance.MouseCurrent.X > x1 && Singleton.Instance.MouseCurrent.Y > y1) && (Singleton.Instance.MouseCurrent.X < x2 && Singleton.Instance.MouseCurrent.Y < y2);
        }
        public bool IsClick(){
            return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
        }
    }
}
