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
		private Texture2D GunTexture,StarTexture,Indicator,BG, Ceiling,
                            WinWindow,LoseWindow,Mainmenu_button,Continue_button,Continue_button_hover,
                            Mainmenu_button_hover,Retry_button,Retry_button_hover,
                            Ok_button,Ok_button_hover,Discover_Frame,Ship,Score_Board,Star_Collect,
                            Warp;
		private Gun gun;
        private float Timer = 0f;
		public Star[,] star = new Star[23,8];
		private SpriteFont Arial,scoreFont;
        private bool gameOver,gameWin;
        private int leftWallX = 326;

        private bool dialog;
        // private int rightWallX = 600;
        private bool gameComplete,Dialog;
        private bool MouseOnMainButton,MouseOnRetryButton,MouseOnContinueButton,MouseOnOkButton;
        private Vector2 FontWidth;

        public void Initial() {
			// Instantiate gun on start GameScreen 
            gun = new Gun(GunTexture, Indicator, StarTexture) {
				pos = new Vector2(Singleton.Instance.Dimension.X / 2 - GunTexture.Width / 2, 700 - GunTexture.Height),
				_gunColor = Color.White
            };

            Singleton.Instance.ceilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
            Singleton.Instance.oldCeilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
            for(int i = 0 ; i < getRowOfCurrentLevel(); i++){
            for(int j = 0 ; j < star.GetLength(1) ; j++){
                star[i,j] = new Star(StarTexture){
                    IsActive = false,
                    pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-Singleton.Instance.rowGapClosing)))),
                    _starColor = Singleton.Instance.GetColor()
                };
                }
            }
        }

        public override void LoadContent() {
            // Load Resource
            base.LoadContent();
			Arial = Content.Load<SpriteFont>("Arial");
            scoreFont = Content.Load<SpriteFont>("kor_bau");
            GunTexture = Content.Load<Texture2D>("gameScreen/gun");
			StarTexture = Content.Load<Texture2D>("gameScreen/star");
			Ceiling = Content.Load<Texture2D>("gameScreen/ceiling");
			Indicator = Content.Load<Texture2D>("gameScreen/indicator");
			BG = Content.Load<Texture2D>("gameScreen/ingame_bg3");
            WinWindow = Content.Load<Texture2D>("gameScreen/win");
            LoseWindow = Content.Load<Texture2D>("gameScreen/Lose");
            Mainmenu_button = Content.Load<Texture2D>("gameScreen/mainmenu_button");
            Mainmenu_button_hover = Content.Load<Texture2D>("gameScreen/mainmenu_button_hover");
            Continue_button = Content.Load<Texture2D>("gameScreen/continue_button");
            Continue_button_hover = Content.Load<Texture2D>("gameScreen/continue_button_hover");
            Retry_button = Content.Load<Texture2D>("gameScreen/retry_button");
            Retry_button_hover = Content.Load<Texture2D>("gameScreen/retry_button_hover");
            Ok_button = Content.Load<Texture2D>("gameScreen/ok_button");
            Ok_button_hover = Content.Load<Texture2D>("gameScreen/ok_button_hover");
            Discover_Frame = Content.Load<Texture2D>("gameScreen/discover_frame");
            Ship = Content.Load<Texture2D>("gameScreen/ship");
            Score_Board = Content.Load<Texture2D>("gameScreen/score_board");
            Star_Collect = Content.Load<Texture2D>("gameScreen/star_Collect");
            
            switch(Singleton.Instance.currentLevel){
                case 1:
                    Warp = Content.Load<Texture2D>("CollectionScreen/warp_One");
                break;
                case 2:
                    Warp = Content.Load<Texture2D>("CollectionScreen/warp_Two");
                break;
                case 3:
                    Warp = Content.Load<Texture2D>("CollectionScreen/warp_Three");
                break;
                case 4:
                    Warp = Content.Load<Texture2D>("CollectionScreen/warp_Four");
                break;
                case 5:
                    Warp = Content.Load<Texture2D>("CollectionScreen/warp_Five");
                break;
                case 6:
                    Warp = Content.Load<Texture2D>("CollectionScreen/warp_Six");
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
                if(Timer >= getLevelTimePerUpdate()){
                    Singleton.Instance.ceilingY += StarTexture.Height;
                    Timer = 0;
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
                // CheckGamewin
                gameWin = true;
                for(int i = 0 ; i < star.GetLength(0) ; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        if (star[i, j] != null){
                            gameWin = false;
                        } 
                    }
                }
                if(gameWin && Singleton.Instance.currentLevel == 6){
                    gameComplete = true;
                }
            } else {
                //  On Dialog
                 Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
                 Singleton.Instance.MouseCurrent = Mouse.GetState();
                //checkDialog
                if(gameOver){
                    // dont show dialogue
                    dialog = true;
                }

                if(!dialog){
                    if(MouseOnTexture(395,435,Ok_button)) {
                        MouseOnOkButton = true;
                        if(IsClick()){
                            dialog = true;
                        }
                    } else {
                            MouseOnOkButton = false;
                        }

                } else 
                if(gameWin && gameComplete){
                    if(MouseOnTexture(395,435,Ok_button)) {
                        MouseOnOkButton = true;
                        if(IsClick()){
                            Singleton.Instance.currentLevel = 1;
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                        }
                    } else {
                        MouseOnOkButton = false;
                    }
                 }else if (gameWin) {
                    // MainMenu Button
                     if(MouseOnElement(395,535,435,452)) {
                        MouseOnMainButton = true;
                        if(IsClick()){
                            if(Singleton.Instance.currentLevel < 6)
                            {
                                Singleton.Instance.currentLevel += 1;
                                if(Singleton.Instance.clearStar < 6) {
                                    Singleton.Instance.clearStar += 1;
                                }
                            }
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                        }
                    } else {
                        MouseOnMainButton = false;
                    }
                    // Continue Button
                    if(MouseOnElement(736,862,435,452)) {
                        MouseOnContinueButton = true;
                        if(IsClick()){
                            if (Singleton.Instance.currentLevel < 6 ) {
                                Singleton.Instance.currentLevel += 1;
                                Singleton.Instance.ceilingY = 30;
                                Singleton.Instance.oldCeilingY = 30;
                                if(Singleton.Instance.clearStar < 6) {
                                    Singleton.Instance.clearStar += 1;
                                }
                                ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                            } else {
                                gameComplete = true;
                            }                        
                        }
                    } else {
                        MouseOnContinueButton = false;
                    }  
                 } else {
                    // MainMenu Button
                    if(MouseOnElement(395,548,435,452)) {
                        MouseOnMainButton = true;
                        if(IsClick()){
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                        }
                    } else {
                        MouseOnMainButton = false;
                    }
                    // Retry Button
                    if(MouseOnElement(758,835,425,448)) {
                        MouseOnRetryButton = true;
                        if(IsClick()){
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                        }
                    } else {
                        MouseOnRetryButton = false;
                    }
                }    
            }
        base.Update(gameTime);
    }
        public override void Draw(SpriteBatch _spriteBatch) {
			_spriteBatch.Draw(BG, Vector2.Zero, Color.White);
            _spriteBatch.Draw(Ceiling, new Vector2(0, Singleton.Instance.ceilingY - Ceiling.Height),Color.White);
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
            FontWidth = scoreFont.MeasureString(Singleton.Instance.Score.ToString());

            // draw score
            _spriteBatch.Draw(Score_Board, new Vector2(42, 54),Color.White);
            _spriteBatch.DrawString(scoreFont,Singleton.Instance.Score.ToString(), new Vector2(150-FontWidth.X/2, 80), Color.Black);
            
            // draw Discover_Frame
            _spriteBatch.Draw(Discover_Frame, new Vector2(1000, 60),Color.White);
            _spriteBatch.Draw(Ship, new Vector2(1095, 370),Color.White);
            _spriteBatch.Draw(Warp, new Vector2(1055, 85),Color.White);

            // check level
            _spriteBatch.DrawString(Arial, "level = " + Singleton.Instance.currentLevel, new Vector2(0, 300), Color.Black);

            
            if(gameWin && !dialog){
                _spriteBatch.Draw(Star_Collect, new Vector2(325, 100),Color.White);
                if (MouseOnOkButton)
                {
                    _spriteBatch.Draw(Ok_button_hover, new Vector2(452, 412),Color.White);
                }
                else {
                    _spriteBatch.Draw(Ok_button, new Vector2(452, 412),Color.White);
                }
            }
            else if (gameWin && gameComplete) { 
                // draw WinWindow
                _spriteBatch.Draw(WinWindow, new Vector2(325, 100),Color.White);
                if (MouseOnOkButton)
                {
                    _spriteBatch.Draw(Ok_button_hover, new Vector2(452, 412),Color.White);
                }
                else {
                    _spriteBatch.Draw(Ok_button, new Vector2(452, 412),Color.White);
                }
            } else if (gameWin) {
                // draw WinWindow
                _spriteBatch.Draw(WinWindow, new Vector2(325, 100),Color.White);
                if (MouseOnMainButton)
                {
                    _spriteBatch.Draw(Mainmenu_button_hover, new Vector2(272, 412),Color.White);
                }
                else {
                    _spriteBatch.Draw(Mainmenu_button, new Vector2(272, 412),Color.White);
                }
                if (MouseOnContinueButton)
                {
                    _spriteBatch.Draw(Continue_button_hover, new Vector2(600, 412),Color.White);
                }
                else {
                    _spriteBatch.Draw(Continue_button, new Vector2(600, 412),Color.White);
                }
            }  
            //gameOver == true
            if (gameOver){
                // draw LoseWindow
                _spriteBatch.Draw(LoseWindow, new Vector2(325, 100),Color.White);
                if (MouseOnMainButton)
                {
                    _spriteBatch.Draw(Mainmenu_button_hover, new Vector2(272, 412),Color.White);
                }
                else {
                    _spriteBatch.Draw(Mainmenu_button, new Vector2(272, 412),Color.White);

                }
                if (MouseOnRetryButton)
                {
                    _spriteBatch.Draw(Retry_button_hover, new Vector2(600, 412),Color.White);
                }
                else {
                    _spriteBatch.Draw(Retry_button, new Vector2(600, 412),Color.White);
                }
            }
        }
        public int GetStartCeilingY(int row, int show){
			return ((Singleton.Instance.STARHITBOX * show) + 30) - (row * (Singleton.Instance.STARHITBOX));
		}


        public float getLevelTimePerUpdate(){
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
                    return 1;
                case 2:
                    return 10;
                case 3:
                    return 11;
                case 4:
                    return 13;
                case 5:
                    return 14;
                case 6:
                    return 1;
                default:
                    return 8;
            }
        }
        public int getShowRowOfCurrentLevel(){
            switch(Singleton.Instance.currentLevel){
                case 1:
                    return 1;
                case 2: case 3:
                    return 5;
                case 4: case 5:
                    return 6;
                case 6:
                    return 1;
                default:
                    return 4;
            }
        }

        public bool MouseOnTexture(int StartX, int StartY, Texture2D texture){
            return (Singleton.Instance.MouseCurrent.X > StartX && Singleton.Instance.MouseCurrent.Y > StartY) && (Singleton.Instance.MouseCurrent.X < StartX + texture.Width && Singleton.Instance.MouseCurrent.Y < StartY + texture.Height);
        }
         public bool MouseOnElement(int x1, int x2, int y1, int y2){
            return (Singleton.Instance.MouseCurrent.X > x1 && Singleton.Instance.MouseCurrent.Y > y1) && (Singleton.Instance.MouseCurrent.X < x2 && Singleton.Instance.MouseCurrent.Y < y2);
        }
        public bool IsClick(){
            return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
        }
    }
}
