using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;


namespace StarCollector.GameObjects {
	public class Star : _GameObject {
		public float Speed;
		public float Angle;
		public bool IsActive;
		public Vector2 Velocity;
		public Color _starColor;
		private int ceilingY = 30;
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
				if (pos.Y <= 30) {
					IsActive = false;
					Singleton.Instance.IsShooting = false;
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
								if (j == 7) {
									starArray[i + 1, j - 1] = this;
									starArray[i + 1, j - 1].pos = new Vector2(leftWallX + ((j - 1) * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (ceilingY + (i + 1) * (_texture.Height-10)));
								} else {
									starArray[i + 1, j] = this;	
									starArray[i + 1, j].pos = new Vector2(leftWallX + (j * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (ceilingY + (i + 1) * (_texture.Height-10)));
								}
							} else {
								// normal row
								starArray[i + 1, j + 1] = this;
								starArray[i + 1, j + 1].pos = new Vector2(leftWallX + ((j + 1) * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (ceilingY + (i + 1) * (_texture.Height-10)));
							}
						} else {
						// if hit bottom left of star 
							if (i % 2 == 0) {
								starArray[i + 1, j - 1] = this;
								starArray[i + 1, j - 1].pos = new Vector2(leftWallX + ((j - 1) * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (ceilingY + (i + 1) * (_texture.Height-10)));
							} else {
								starArray[(i + 1), j] = this;
								starArray[(i + 1), j].pos = new Vector2(leftWallX + (j * _texture.Width) + ((i + 1) % 2 == 0 ? 0 : _texture.Width / 2), (ceilingY + (i + 1) * (_texture.Height-10)));
							}
						}
						IsActive = false;
						Singleton.Instance.IsShooting = false;
						return;
					}
                }
            }
		}
		
		public Vector2 GetMiddleOfStar(Vector2 star){
			return new Vector2(star.X + 50 , star.Y + 50);
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

		public int CheckCollision(Star other) {
			return 0;
		}

		public bool IsRemoveable(Star[,] StarArray, Color TargetColor, Vector2 current){
			
		}

		
		public void CheckRemoveBubble(Star[,] gameObjects, Color ColorTarget, Vector2 me) {
            
		}
	}
}