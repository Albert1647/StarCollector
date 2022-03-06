using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace StarCollector.GameObjects {
    class Gun : _GameObject {
		public Gun(Texture2D texture, Texture2D bubble) : base(texture) {

		}

		public override void Update(GameTime gameTime, Bubble[,] gameObjects) {

		}
		// Draw
		// Draw Shooting Bubble
		public override void Draw(SpriteBatch spriteBatch) {

		}
		// Random Color
		public Color GetRandomColor() {
            return Color.White;
		}
	}
}