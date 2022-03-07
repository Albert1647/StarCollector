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
		
		private bool collision;

		public Star(Texture2D texture) : base(texture) {
			
		}
		public override void Update(GameTime gameTime) {
			// 
			if(IsActive){
				// Calculate moving star
				// ps. pos is star pos
				Velocity.X = (float)Math.Cos(Angle) * Speed;
				Velocity.Y = (float)Math.Sin(Angle) * Speed;
				pos += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
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
		public override void Draw(SpriteBatch _spriteBatch) {
			// Draw a 'moving' star at 'vector' pos
			_spriteBatch.Draw(_texture, pos, _starColor);
			base.Draw(_spriteBatch);
		}
		private void DetectCollision(Star[,] gameObjects) {
			
		}
		

		public int CheckCollision(Star other) {
			return 0;
		}
		public void CheckRemoveBubble(Star[,] gameObjects, Color ColorTarget, Vector2 me) {
            
		}
	}
}