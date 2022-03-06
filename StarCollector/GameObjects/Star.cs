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
		
		public SoundEffectInstance deadSFX , stickSFX;

		public Star(Texture2D texture) : base(texture) {
			IsActive = true;
		}
		public override void Update(GameTime gameTime) {
			// 
			if(IsActive){
				Velocity.X = (float)Math.Cos(Angle) * Speed;
				Velocity.Y = (float)Math.Sin(Angle) * Speed;
				pos += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
				
				// If ball reach top outside
				if (pos.Y <= 40) {
					IsActive = false;
					Singleton.Instance.IsShooting = false;
				}
				// If ball collision left
				if (pos.X <= 325) {
					Angle = -Angle;
					Angle += MathHelper.ToRadians(180);
				}
				// If right side of ball reach collision right
				if (pos.X + _texture.Width >= 960) {
					Angle = -Angle;
					Angle += MathHelper.ToRadians(180);
				}

			}
			
		}
		public override void Draw(SpriteBatch _spriteBatch) {
			// Draw a 'moving' star at x pos
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