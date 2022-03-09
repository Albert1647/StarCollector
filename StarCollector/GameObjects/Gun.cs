using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace StarCollector.GameObjects {
    class Gun : _GameObject {
		private Random random = new Random();
		private float aimAngle;
		private Color _starColor;
		private Texture2D starTexture;
		private Texture2D Indicator;
		private Star star; // star on gun
		public Color _gunColor;
		public Gun(Texture2D texture, Texture2D indicator, Texture2D star) : base(texture) {
			// save texture
			starTexture = star;
			Indicator = indicator;
			// random color
			_starColor = Singleton.Instance.GetColor();
			// set gun color
			_gunColor = Color.White;
		}

		public override void Update(GameTime gameTime, Star[,] starArray) {
			Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
			Singleton.Instance.MouseCurrent = Mouse.GetState();
			// shootable at mouse Y
			if (IsShootable()) {
				// calculate gun aim angle
				aimAngle = (float)Math.Atan2((pos.Y + _texture.Height / 2) - Singleton.Instance.MouseCurrent.Y, (pos.X + _texture.Width / 2) - Singleton.Instance.MouseCurrent.X);
				// shooting
				if (!Singleton.Instance.IsShooting && IsClick()) {
					star = new Star(starTexture) {
						// shoot star start on pos below
						pos = new Vector2(Singleton.Instance.Dimension.X / 2 - starTexture.Width / 2, 700 - starTexture.Height),
						Angle = aimAngle + MathHelper.Pi,
						_starColor = _starColor,
						Speed = 2000,
						IsActive = true
					};
					// update starColor in board
					checkStarColor(starArray);
					// get existing starColor in board
					_starColor = getStarColor(starArray);
					// set state to shooting
					Singleton.Instance.IsShooting = true;
				}
			}
			// if shooting / go update star
			if (Singleton.Instance.IsShooting){
				// if shooting update logic in star
				star.Update(gameTime, starArray);
			}
		}
		public override void Draw(SpriteBatch _spriteBatch) {
			// Draw Indicator
			_spriteBatch.Draw(Indicator, pos + new Vector2(50 + Indicator.Width, 50), null, Color.White, aimAngle + MathHelper.ToRadians(90f), new Vector2(0,0), 1.5f, SpriteEffects.None, 0f);
			// Draw Gun with Turning Angle
			_spriteBatch.Draw(_texture, pos + new Vector2(50, 50), null, Color.White, aimAngle + MathHelper.ToRadians(-90f), new Vector2(50, 50), 1.5f, SpriteEffects.None, 0f);
			if (!Singleton.Instance.IsShooting){
				// Draw Bubble On Gun
				_spriteBatch.Draw(starTexture, new Vector2(Singleton.Instance.Dimension.X / 2 - starTexture.Width / 2, 700 - starTexture.Height), _starColor);
			}
			else{
				// Draw Shooting Star
				star.Draw(_spriteBatch);
			}
		}
		// Random Color
		 public Color getStarColor(Star[,] starArray) {
            
			return Singleton.Instance.starColor[random.Next(0, Singleton.Instance.starColor.Count)];
		}
		// Update Existing StarColor in board
		 public void checkStarColor(Star[,] starArray) {
			Singleton.Instance.starColor.Clear();
            for(int i = 0 ; i < starArray.GetLength(0) ; i++){
                for(int j = 0 ; j < starArray.GetLength(1) ; j++){
                    if (starArray[i, j] != null){
                        if(!Singleton.Instance.starColor.Contains(starArray[i,j]._starColor)){
                            Singleton.Instance.starColor.Add(starArray[i,j]._starColor);
                        }
                    }
                }
            }
		}

		public bool IsClick(){
            return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
        }

		public bool IsShootable(){
			return (Singleton.Instance.MouseCurrent.Y < 625 
			&& Singleton.Instance.MouseCurrent.X > 0 
			&& Singleton.Instance.MouseCurrent.Y > 0 
			&& Singleton.Instance.MouseCurrent.X < Singleton.Instance.Dimension.X
			// && Singleton.Instance.MouseCurrent.Y < Singleton.Instance.Dimension.Y
			);
		}
	}
}