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
                            Warp, WinStar;
        private SoundEffect Click, GameOver, GameWin, GotStar, GunShoot, Pop, HoverMenu;
        private SoundEffectInstance GunShootInstance, PopInstance;
        private Song ThemeSong;
		private Gun gun;
        private float Timer = 0f;
		public Star[,] star = new Star[23,8];
		private SpriteFont Arial,scoreFont;
        private bool gameOver, gameWin, HoverMainMenu, HoverRetry, HoverContinue, HoverOK, HoverMainMenuFinal, HoverMainMenuLose;
        private int leftWallX = 326;
        private float rotate = 0;
        private bool dialog, playGameOver, playGameWin, playGotStar;
        // private int rightWallX = 600;
        private bool gameComplete,Dialog;
        private bool MouseOnMainButton,MouseOnRetryButton,MouseOnContinueButton,MouseOnOkButton;
        private Vector2 FontWidth;

        public void Initial() {
            // Instantiate gun on start GameScreen 
            gun = new Gun(GunTexture, Indicator, StarTexture) {
                pos = new Vector2(Singleton.Instance.Dimension.X / 2 - GunTexture.Width / 2, 700 - GunTexture.Height),
                _gunColor = Color.White,
                _GunShoot = GunShootInstance,
                _Pop = PopInstance
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
			BG = Content.Load<Texture2D>("gameScreen/ingame_bg4");
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
            PopInstance = Pop.CreateInstance();
            HoverMenu = Content.Load<SoundEffect>("Sound/menu_select");
            ThemeSong = Content.Load<Song>("Sound/theme");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume=0.2f;
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

            switch(Singleton.Instance.currentLevel){
                case 1:
                    WinStar = Content.Load<Texture2D>("collectionScreen/warp_One");
                break;
                case 2:
                    WinStar = Content.Load<Texture2D>("collectionScreen/warp_Two");
                break;
                case 3:
                    WinStar = Content.Load<Texture2D>("collectionScreen/warp_Three");
                break;
                case 4:
                    WinStar = Content.Load<Texture2D>("collectionScreen/warp_Four");
                break;
                case 5:
                    WinStar = Content.Load<Texture2D>("collectionScreen/warp_Five");
                break;
                case 6:
                    WinStar = Content.Load<Texture2D>("collectionScreen/warp_Six");
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

                if(rotate < 360){
                    rotate += 0.05f;
				if(rotate >= 360){
					rotate = 0;
				}
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
                    if(!playGotStar){
                        GotStar.Play();
                        playGotStar = true;
                    }
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
                    if(!playGameWin){
                        MediaPlayer.Pause();
                        GameWin.Play(0.3f,0.0f,0.0f);
                        playGameWin = true;
                    }
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
                            Singleton.Instance.Score = 0;
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                        }
                    } else {
                        MouseOnMainButton = false;
                        HoverMainMenuFinal = false;
                    }
                 } else if (gameWin) {
                     if(!playGameWin){
                        MediaPlayer.Pause();
                        GameWin.Play(0.3f,0.0f,0.0f);
                        playGameWin = true;
                     }
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
                                Singleton.Instance.Score = 0;
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
                                Singleton.Instance.Score = 0;
                            }
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                        }
                    } else {
                        MouseOnMainButton = false;
                        HoverMainMenu = false;
                    }
                      
                 } else {
                    // Retry Button
                    if(!playGameOver){
                        MediaPlayer.Pause();
                        GameOver.Play();
                        playGameOver = true;
                    }
                    if (MouseOnElement(600,676,342,363)) {
                        MouseOnRetryButton = true;
                        if (HoverRetry == false)
                        {
                            HoverMenu.Play();
                            HoverRetry = true;
                        }
                        if (IsClick()){
                            Click.Play();
                            Singleton.Instance.Score = 0;
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                        }
                    } else {
                        MouseOnRetryButton = false;
                        HoverRetry = false;
                    }
                    // MainMenu Button
                    
                    if (MouseOnElement(563,765,438,453)) {
                        MouseOnMainButton = true;
                        if (HoverMainMenuLose == false)
                        {
                            HoverMenu.Play();
                            HoverMainMenuLose = true;
                        }
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
            FontWidth = scoreFont.MeasureString(Singleton.Instance.Score.ToString());
            _spriteBatch.Draw(Score_Board, new Vector2(42, 54),Color.White);
            _spriteBatch.DrawString(scoreFont,Singleton.Instance.Score.ToString(), new Vector2(150-FontWidth.X/2, 80), Color.White);

            // draw combo
            if((Singleton.Instance.Combo - 1) >= 2){
                FontWidth = scoreFont.MeasureString("Combo");
                _spriteBatch.DrawString(scoreFont,"Combo", new Vector2(150-FontWidth.X/2, 600), Color.White);
                FontWidth = scoreFont.MeasureString("x" + (Singleton.Instance.Combo - 1).ToString());
                _spriteBatch.DrawString(scoreFont,"x" + (Singleton.Instance.Combo - 1).ToString(), new Vector2(150-FontWidth.X/2, 640), Color.White);
            }
            
            _spriteBatch.DrawString(scoreFont,"Level " + (Singleton.Instance.currentLevel).ToString(), new Vector2(75, 0), Color.White);
            // draw Discover_Frame
            _spriteBatch.Draw(Discover_Frame, new Vector2(1000, 60),Color.White);
            _spriteBatch.Draw(Ship, new Vector2(1095, 370),Color.White);
            _spriteBatch.Draw(Warp, new Vector2(1055 + Warp.Width / 2, 85 + Warp.Height / 2),null, Color.White, rotate, new Vector2(Warp.Width/2 , Warp.Height/2), 1f, SpriteEffects.None, 0f);
            
            if(gameWin && !dialog){
                _spriteBatch.Draw(Star_Collect, new Vector2(325, 100),Color.White);
                _spriteBatch.Draw(WinStar, new Vector2(1000 + (Discover_Frame.Width / 2) , 85), null,Color.White, 0f,new Vector2(WinStar.Width/2, 0), 1f, SpriteEffects.None, 0f);
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
                _spriteBatch.Draw(WinStar, new Vector2(1000 + (Discover_Frame.Width / 2) , 85), null,Color.White, 0f,new Vector2(WinStar.Width/2, 0), 1f, SpriteEffects.None, 0f);
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
                _spriteBatch.Draw(WinStar, new Vector2(1000 + (Discover_Frame.Width / 2) , 85), null,Color.White, 0f,new Vector2(WinStar.Width/2, 0), 1f, SpriteEffects.None, 0f);
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
                    return 11f;
                case 2:
                    return 11f;
                case 3:
                    return 11f;
                case 4:
                    return 11f;
                case 5:
                    return 12;
                case 6:
                    return 12;
                default:
                    return 11f;
            }
        }

        public int getRowOfCurrentLevel(){
            switch(Singleton.Instance.currentLevel){
                case 1:
                    return 8;
                case 2:
                    return 9;
                case 3:
                    return 10;
                case 4:
                    return 11;
                case 5:
                    return 13;
                case 6:
                    return 15;
                default:
                    return 8;
            }
        }

        public int getShowRowOfCurrentLevel(){
            switch(Singleton.Instance.currentLevel){
                case 1:
                    return 3;
                case 2:
                    return 4;
                case 3:
                    return 4;
                case 4: 
                    return 4;
                case 5:
                    return 5;
                case 6:
                    return 5;
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
