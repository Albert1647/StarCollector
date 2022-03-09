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
		private Texture2D GunTexture,StarTexture,Indicator,BG,StarDiscover;
		private Gun gun;
        private Random random = new Random();
		public Star[,] star = new Star[11,8];

		private SpriteFont Arial,scoreFont;
        private int startLengthRow = 3;

        private bool gameOver,gameWin;

        private int leftWallX = 326;
        // private int rightWallX = 600;
        private bool gameComplete;

        public void Initial() {
			// Instantiate gun on start GameScreen 
            gun = new Gun(GunTexture, Indicator, StarTexture) {
				pos = new Vector2(Singleton.Instance.Dimension.X / 2 - GunTexture.Width / 2, 700 - GunTexture.Height),
				_gunColor = Color.White
            };

             switch(Singleton.Instance.currentLevel){
                case 1:
                    for(int i = 0 ; i < 3; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = GetRandomColor()
                        };
                    }
                }
                break;
                case 2:
                    for(int i = 0 ; i < 3; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = GetRandomColor()
                        };
                    }
                }
                break;
                case 3:
                    for(int i = 0 ; i < 4; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = GetRandomColor()
                        };
                    }
                }
                break;
                case 4:
                    for(int i = 0 ; i < 5; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = GetRandomColor()
                        };
                    }
                }
                break;
                case 5:
                    for(int i = 0 ; i < 6; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = GetRandomColor()
                        };
                    }
                }
                break;
                case 6:
                    for(int i = 0 ; i < 7; i++){
                    for(int j = 0 ; j < star.GetLength(1) ; j++){
                        star[i,j] = new Star(StarTexture){
                            IsActive = false,
                            pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                            _starColor = GetRandomColor()
                        };
                    }
                }
                break;
            }

            // Original
            for(int i = 0 ; i < startLengthRow ; i++){
                for(int j = 0 ; j < star.GetLength(1) ; j++){
                    star[i,j] = new Star(StarTexture){
                        IsActive = false,
                        pos = new Vector2(leftWallX + (j * StarTexture.Width + (i % 2 == 0 ? 0 : StarTexture.Width / 2)), (Singleton.Instance.ceilingY + (i * (StarTexture.Height-10)))),
                        _starColor = GetRandomColor()
                    };
                }
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
            Singleton.Instance.oldCeilingY = Singleton.Instance.ceilingY;

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
                        ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.GameScreen);
                    } else {
                        gameComplete = true;
                    }                        
                }
            // update/load gun logic
			gun.Update(gameTime, star);
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

        }

        public Color GetRandomColor() {
			Color _starColor = Color.Black;
			switch (random.Next(0, 6)) {
				case 0:
					_starColor = Color.White;
					break;
				case 1:
					_starColor = Color.Blue;
					break;
				case 2:
					_starColor = Color.Yellow;
					break;
				case 3:
					_starColor = Color.Red;
					break;
				case 4:
					_starColor = Color.Green;
					break;
				case 5:
					_starColor = Color.Purple;
					break;
			}
			return _starColor;
		}
    }
}
