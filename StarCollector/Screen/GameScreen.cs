using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarCollector.GameObjects;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using StarCollector.Managers;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace StarCollector.Screen {
    class GameScreen : _GameScreen {
		private Texture2D GunTexture,StarTexture,Indicator,BG, Ceiling,
                            WinWindow,LoseWindow,Mainmenu_button,Continue_button,Continue_button_hover,
                            Mainmenu_button_hover,Retry_button,Retry_button_hover,
                            Ok_button,Ok_button_hover,Discover_Frame,Ship,Score_Board,Star_Collect,
                            Warp;
        private SoundEffect Click, GameOver, GameWin, GotStar, GunShoot, Pop, HoverMenu;
        private SoundEffectInstance GunShootInstance, PopInstance;
        private Song ThemeSong;
		private Gun gun;
        private float Timer = 0f;
		public Star[,] star = new Star[23,8];
		private SpriteFont Arial,scoreFont;
        private bool gameOver, gameWin, HoverMainMenu, HoverRetry, HoverContinue, HoverOK, HoverMainMenuFinal, HoverMainMenuLose;
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
                _gunColor = Color.White,
                _GunShoot = GunShootInstance
            };

            Singleton.Instance.ceilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
            Singleton.Instance.oldCeilingY = GetStartCeilingY(getRowOfCurrentLevel(), getShowRowOfCurrentLevel());
            for(int i = 0 ; i < getRowOfCurrentLevel(); i++){
            for(int j = 0 ; j < star.GetLength(1) ; j++){
                star[i,j] = new Star(StarTexture){
                    IsActive = false,
                    pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-Singleton.Instance.rowGapClosing)))),
                    _starColor = Singleton.Instance.GetColor(),
                    _Pop = PopInstance
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
            Click = Content.Load<SoundEffect>("Sound/click");
            GameOver = Content.Load<SoundEffect>("Sound/game_over");
            GameWin = Content.Load<SoundEffect>("Sound/game_win");
            GotStar = Content.Load<SoundEffect>("Sound/got_star");
            GunShoot = Content.Load<SoundEffect>("Sound/gun_shoot(new)");
            GunShootInstance = GunShoot.CreateInstance();
            Pop = Content.Load<SoundEffect>("Sound/star_pop");
            HoverMenu = Content.Load<SoundEffect>("Sound/menu_select");
            ThemeSong = Content.Load<Song>("Sound/theme");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(ThemeSong);

            switch (Singleton.Instance.currentLevel){
                case 1:
                    Warp = Content.Load<Texture2D>("gameScreen/star_red");
                break;
                case 2:
                    Warp = Content.Load<Texture2D>("gameScreen/star_purple");
                break;
                case 3:
                    Warp = Content.Load<Texture2D>("gameScreen/star_skyblue");
                break;
                case 4:
                    Warp = Content.Load<Texture2D>("gameScreen/star_yellow");
                break;
                case 5:
                    Warp = Content.Load<Texture2D>("gameScreen/star_white");
                break;
                case 6:
                    Warp = Content.Load<Texture2D>("gameScreen/star_blue");
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
                    GotStar.Play();
                    if(MouseOnElement(632,670,429,452)) {
                        MouseOnOkButton = true;
                        if(HoverOK == false) {
                            HoverMenu.Play();
                            HoverOK = true;
                        }
                        if(IsClick()){
                            Click.Play();
                            dialog = true;
                        }
                    } else {
                            MouseOnOkButton = false;
                            HoverOK = false;
                        }

                } else 
                if(gameWin && gameComplete){
                    GameWin.Play();
                    if(MouseOnElement(574,729,402,422)) {
                        MouseOnMainButton = true;
                        if (HoverMainMenuFinal == false)
                        {
                            HoverMenu.Play();
                            HoverMainMenuFinal = true;
                        }
                        if (IsClick()){
                            Click.Play();
                            Singleton.Instance.currentLevel = 1;
                            Singleton.Instance.HighestScore = Singleton.Instance.Score;
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                        }
                    } else {
                        MouseOnMainButton = false;
                        HoverMainMenuFinal = false;
                    }
                 } else if (gameWin) {
                    GameWin.Play();
                     // Continue Button
                    if(MouseOnElement(576,700,344,368)) {
                        MouseOnContinueButton = true;
                        if (HoverContinue == false)
                        {
                            HoverMenu.Play();
                            HoverContinue = true;
                        }
                        if (IsClick()){
                            Click.Play();
                            if (Singleton.Instance.currentLevel < 6 ) {
                                Singleton.Instance.currentLevel += 1;
                                Singleton.Instance.ceilingY = 30;
                                Singleton.Instance.oldCeilingY = 30;
                                if(Singleton.Instance.clearStar < 6) {
                                    Singleton.Instance.clearStar += 1;
                                }
                                Singleton.Instance.HighestScore = Singleton.Instance.Score;
                                ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                            } else {
                                gameComplete = true;
                            }                        
                        }
                    } else {
                        MouseOnContinueButton = false;
                        HoverContinue = false;
                    }
                    // MainMenu Button
                     if(MouseOnElement(561,715,436,452)) {
                        MouseOnMainButton = true;
                        if (HoverMainMenu == false)
                        {
                            HoverMenu.Play();
                            HoverMainMenu = true;
                        }
                        if (IsClick()){
                            Click.Play();
                            if(Singleton.Instance.currentLevel < 6)
                            {
                                Singleton.Instance.currentLevel += 1;
                                if(Singleton.Instance.clearStar < 6) {
                                    Singleton.Instance.clearStar += 1;
                                }
                                Singleton.Instance.HighestScore = Singleton.Instance.Score;
                            }
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                        }
                    } else {
                        MouseOnMainButton = false;
                        HoverMainMenu = false;
                    }
                      
                 } else {
                    // Retry Button
                    GameOver.Play();
                    if (MouseOnElement(600,676,342,363)) {
                        MouseOnRetryButton = true;
                        if (HoverRetry == false)
                        {
                            HoverMenu.Play();
                            HoverRetry = true;
                        }
                        if (IsClick()){
                            Click.Play();
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                        }
                    } else {
                        MouseOnRetryButton = false;
                        HoverRetry = false;
                    }
                    // MainMenu Button
                    if (HoverMainMenuLose == false)
                    {
                        HoverMenu.Play();
                        HoverMainMenuLose = true;
                    }
                    if (MouseOnElement(563,765,438,453)) {
                        MouseOnMainButton = true;
                        if(IsClick()){
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                        }
                    } else {
                        MouseOnMainButton = false;
                        HoverMainMenuLose = false;
                    }
                    // Retry Button
                    
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
            _spriteBatch.DrawString(scoreFont,Singleton.Instance.HighestScore.ToString(), new Vector2(150-FontWidth.X/2, 120), Color.Black);
            _spriteBatch.DrawString(scoreFont,(Singleton.Instance.Combo - 1).ToString(), new Vector2(150-FontWidth.X/2, 160), Color.Black);
            
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
                if (MouseOnMainButton)
                {
                    _spriteBatch.Draw(Mainmenu_button_hover, new Vector2(452, 380),Color.White);
                }
                else {
                    _spriteBatch.Draw(Mainmenu_button, new Vector2(452, 380),Color.White);
                }
            } else if (gameWin) {
                // draw WinWindow
                _spriteBatch.Draw(WinWindow, new Vector2(325, 100),Color.White);
                if (MouseOnContinueButton)
                {
                    _spriteBatch.Draw(Continue_button_hover, CenterElementWithHeight(Continue_button_hover,327) , Color.White);
                }
                else {
                    _spriteBatch.Draw(Continue_button, CenterElementWithHeight(Continue_button,327) , Color.White);
                }
                if (MouseOnMainButton)
                {
                    _spriteBatch.Draw(Mainmenu_button_hover, CenterElementWithHeight(Mainmenu_button_hover,412) , Color.White);
                }
                else {
                    _spriteBatch.Draw(Mainmenu_button, CenterElementWithHeight(Mainmenu_button,412) , Color.White);
                }
                
            }  
            //gameOver == true
            if (gameOver){
                // draw LoseWindow
                _spriteBatch.Draw(LoseWindow, new Vector2(325, 100),Color.White);
    
                if (MouseOnRetryButton)
                {
                    _spriteBatch.Draw(Retry_button_hover,CenterElementWithHeight(Retry_button_hover,327) , Color.White);
                }
                else {
                    _spriteBatch.Draw(Retry_button,CenterElementWithHeight(Retry_button,327) , Color.White);
                }
                if (MouseOnMainButton)
                {
                    _spriteBatch.Draw(Mainmenu_button_hover,CenterElementWithHeight(Mainmenu_button_hover,412) , Color.White);
                }
                else {
                    _spriteBatch.Draw(Mainmenu_button,CenterElementWithHeight(Mainmenu_button,412) , Color.White);

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
                    return 2;
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
                    return 2;
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
        public Vector2 CenterElementWithHeight(Texture2D element,int height){
            return new Vector2(Singleton.Instance.Dimension.X / 2 - (element.Width / 2) ,height );
        }
    }
}
