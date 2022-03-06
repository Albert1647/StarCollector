using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace StarCollector.GameObjects {
	public class Bubble : _GameObject {
		public float Speed;
		public float Angle;
		
		public SoundEffectInstance deadSFX , stickSFX;

		public Bubble(Texture2D texture) : base(texture) {
			
		}

		public override void Update(GameTime gameTime, Bubble[,] gameObjects) {
		
		}

		private void DetectCollision(Bubble[,] gameObjects) {
			
		}
		public override void Draw(SpriteBatch spriteBatch) {

			base.Draw(spriteBatch);
		}

		public int CheckCollision(Bubble other) {
			return 0;
		}
		public void CheckRemoveBubble(Bubble[,] gameObjects, Color ColorTarget, Vector2 me) {
            
		}
	}
}