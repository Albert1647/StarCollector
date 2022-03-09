using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace StarCollector.GameObjects {
	public class Star : _GameObject {
		public float Speed;
		public float Angle;
		public bool IsActive;
		public Vector2 Velocity;
		public Color _starColor;
		//define star delimiter
		private int starDelimeter = 74;

        private int leftWallX = 326;
		
		public Star(Texture2D texture) : base(texture) {
		}
		public override void Update(GameTime gameTime, Star[,] starArray) {
			if(IsActive){
				// Calculate moving star
				// ps. pos is star pos
				Velocity.X = (float)Math.Cos(Angle) * Speed;
				Velocity.Y = (float)Math.Sin(Angle) * Speed;
				pos += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
				// send flying star to process collision
				DetectCollision(starArray);

				// If ball reach top ceiling
				if (pos.Y <= Singleton.Instance.ceilingY) {
					IsActive = false;
					Singleton.Instance.IsShooting = false;
					Singleton.Instance.Score -= 10;
					Singleton.Instance.ceilingY += (_texture.Width * 2);
				}
				// If ball collision left
				if (pos.X <= 326) {
					// flip angle horizontal
					Angle = -Angle;
					Angle += MathHelper.ToRadians(180);
				}
				// If right side of ball reach collision right
				if (pos.X + _texture.Width >= 955) {
					// flip angle horizontal
					Angle = -Angle;
					Angle += MathHelper.ToRadians(180);
				}
			}
		}

		private void DetectCollision(Star[,] starArray){
			for(int i = 0 ; i < starArray.GetLength(0) ; i++){
                for(int j = 0 ; j < starArray.GetLength(1) ; j++){
					// If Star Collide
					// if(!StarIsEmpty(starArray[i,j]) && StarIsCollide(pos, starArray[i,j].pos )){
					if(!StarIsEmpty(starArray[i,j]) && StarIsCollide(GetMiddleOfStar(pos), GetMiddleOfStar(starArray[i,j].pos) )){
						// if hit bottom right of star
						if (GetMiddleOfStar(pos).X >= GetMiddleOfStar(starArray[i, j].pos).X) {
							if (i % 2 == 0) {
								// last row of even row	
								if (j == starArray.GetLength(1)) {	
									if(starArray[i + 1, j - 1] == null){
										starArray[i + 1, j - 1] = this;
										starArray[i + 1, j - 1].pos = new Vector2(leftWallX + ((j - 1) * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i + 1) * (_texture.Height-10)));
										CheckRemoveBubble(starArray, _starColor, new Vector2(j - 1, i + 1));
									} else {
										starArray[i, j - 1] = this;
										starArray[i, j - 1].pos = new Vector2(leftWallX + ((j - 1) * _texture.Width) + ((i) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i) * (_texture.Height-10)));
										CheckRemoveBubble(starArray, _starColor, new Vector2(j + 1, i));
									}
								} else {
									if(starArray[i + 1, j] == null){
										starArray[i + 1, j] = this;	
										starArray[i + 1, j].pos = new Vector2(leftWallX + (j * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i + 1) * (_texture.Height-10)));
										CheckRemoveBubble(starArray, _starColor, new Vector2(j, i + 1));
									} else {
										starArray[i, j + 1] = this;	
										starArray[i, j + 1].pos = new Vector2(leftWallX + ((j + 1)  * _texture.Width) + ((i) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i) * (_texture.Height-10)));
										CheckRemoveBubble(starArray, _starColor, new Vector2((j + 1), i));
									}
								}
							} else {
								// normal row
								if(starArray[i + 1, j + 1] == null){
									starArray[i + 1, j + 1] = this;
									starArray[i + 1, j + 1].pos = new Vector2(leftWallX + ((j + 1) * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i + 1) * (_texture.Height-10)));
									CheckRemoveBubble(starArray, _starColor, new Vector2(j + 1, i + 1));
								} else {
									starArray[i, j + 1] = this;
									starArray[i, j + 1].pos = new Vector2(leftWallX + ((j + 1) * _texture.Width) + ((i) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i) * (_texture.Height-10)));
									CheckRemoveBubble(starArray, _starColor, new Vector2(j, i));
								}
							}
						} else {
						// if hit bottom left of star 
							if (i % 2 == 0) {
								// bug prevention
								if( j > 0 ){
									if(starArray[i + 1, j - 1] == null){
										starArray[i + 1, j - 1] = this;
										starArray[i + 1, j - 1].pos = new Vector2(leftWallX + ((j - 1) * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i + 1) * (_texture.Height-10)));
										CheckRemoveBubble(starArray, _starColor, new Vector2(j - 1, i + 1));
									} else {
										starArray[i, j - 1] = this;
										starArray[i, j - 1].pos = new Vector2(leftWallX + ((j - 1) * _texture.Width) + ((i) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i) * (_texture.Height-10)));
										CheckRemoveBubble(starArray, _starColor, new Vector2(j - 1, i));
									}
								}
								else {
									if(starArray[i + 1, j] == null){
										starArray[i + 1, j] = this;
										starArray[i + 1, j].pos = new Vector2(leftWallX + ((j) * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i + 1) * (_texture.Height-10)));
										CheckRemoveBubble(starArray, _starColor, new Vector2(j, i + 1));
									} else {
										starArray[i, j] = this;
										starArray[i, j].pos = new Vector2(leftWallX + ((j) * _texture.Width) + ((i) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i) * (_texture.Height-10)));
										CheckRemoveBubble(starArray, _starColor, new Vector2(j, i + 1));
									}
								}
							} else {
								if(starArray[i + 1, j] == null){
									starArray[(i + 1), j] = this;
									starArray[(i + 1), j].pos = new Vector2(leftWallX + (j * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i + 1) * (_texture.Height-10)));
									CheckRemoveBubble(starArray, _starColor, new Vector2(j, i + 1));
								} else {
									starArray[(i), j] = this;
									starArray[(i), j].pos = new Vector2(leftWallX + (j * _texture.Width) + ((i) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (i) * (_texture.Height-10)));
									CheckRemoveBubble(starArray, _starColor, new Vector2(j, i));
								}
							}
						}

						IsActive = false;
						// 
						if(Singleton.Instance.RemovableStar.Count >= 3){
							Singleton.Instance.Score += Singleton.Instance.RemovableStar.Count * 10;
							starArray = CheckLeftOver(starArray);
							checkStarColor(starArray);
						}
						else if (Singleton.Instance.RemovableStar.Count > 0) {
							// Redraw 
							foreach (Vector2 v in Singleton.Instance.RemovableStar) {
								starArray[(int)v.Y, (int)v.X] = new Star(_texture) {
								pos = new Vector2(leftWallX + (v.X * _texture.Width) + ((v.Y) % 2 == 0 ? 0 : _texture.Width / 2), (Singleton.Instance.ceilingY + (v.Y) * (_texture.Height-10))),
								_starColor = _starColor,
								IsActive = false
							};
							}
						}
						// Clear Removable star
						Singleton.Instance.RemovableStar.Clear();
						Singleton.Instance.IsShooting = false;
						return;
					}
                }
            }
		}
		
		public Vector2 GetMiddleOfStar(Vector2 star){
			return new Vector2(star.X + 50  , star.Y  + 50);
		}
		// Check hanging star
		public Star[,] CheckLeftOver(Star[,] starArray){
			for(int i = 1 ; i < starArray.GetLength(0) ; i++){
                for(int j = 0 ; j < starArray.GetLength(1) ; j++){
					if(!StarIsEmpty(starArray[i,j])){
						if( (i % 2) == 0){
							if(j == 0 ){
								if(starArray[i-1,j] == null){
									starArray[i,j] = null;
								}
							} else {
								if(starArray[i-1,j] == null && starArray[i-1,j - 1] == null){
									starArray[i,j] = null;
								}
							}
						} else {
							if(j == starArray.GetLength(1) - 1){
								if(starArray[i-1,j] == null){
									starArray[i,j] = null;
								}
							} else {
								if(starArray[i-1,j] == null && starArray[i-1,j + 1] == null){
									starArray[i,j] = null;
								}
							}
						}
					}
                }
            }

			return starArray;
		}

		public void CheckRemoveBubble(Star[,] starArray, Color targetColor, Vector2 star) {
			if ((star.X >= 0 && star.Y >= 0) && (star.X <= starArray.GetLength(1) && star.Y <= starArray.GetLength(0)) && starArray[(int)star.Y, (int)star.X] != null && starArray[(int)star.Y, (int)star.X]._starColor == targetColor) {
				Singleton.Instance.RemovableStar.Add(star);
				starArray[(int)star.Y, (int)star.X] = null;
			} else {
				return;
			}
			CheckRemoveBubble(starArray, targetColor, new Vector2(star.X + 1, star.Y)); // Right
			CheckRemoveBubble(starArray, targetColor, new Vector2(star.X - 1, star.Y)); // Left
			if (star.Y % 2 == 0) {
				CheckRemoveBubble(starArray, targetColor, new Vector2(star.X, star.Y - 1)); // Top Right
				CheckRemoveBubble(starArray, targetColor, new Vector2(star.X - 1, star.Y - 1)); // Top Left
				CheckRemoveBubble(starArray, targetColor, new Vector2(star.X, star.Y + 1)); // Bot Right
				CheckRemoveBubble(starArray, targetColor, new Vector2(star.X - 1, star.Y + 1)); // Bot Left
			} else {
				CheckRemoveBubble(starArray, targetColor, new Vector2(star.X + 1, star.Y - 1)); // Top Right
				CheckRemoveBubble(starArray, targetColor, new Vector2(star.X, star.Y - 1)); // Top Left
				CheckRemoveBubble(starArray, targetColor, new Vector2(star.X + 1, star.Y + 1)); // Bot Right
				CheckRemoveBubble(starArray, targetColor, new Vector2(star.X, star.Y + 1)); // Bot 		}
			}
		}
		public bool StarIsCollide(Vector2 currentPos , Vector2 starPos){
			return (int)Math.Sqrt(Math.Pow(currentPos.X - starPos.X, 2) + Math.Pow(currentPos.Y - starPos.Y, 2)) <= starDelimeter;
		}

		public bool StarIsEmpty(Star starArray){
			return starArray == null;
		}

		public override void Draw(SpriteBatch _spriteBatch) {
			// Draw a 'moving' star at 'vector' pos
			_spriteBatch.Draw(_texture, pos, _starColor);
			base.Draw(_spriteBatch);
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
	}
}