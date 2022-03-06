using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace StarCollector.GameObjects {
    class Gun : _GameObject {

		private float aimAngle;
		public Gun(Texture2D texture) : base(texture) {

		}

		public void Update(GameTime gameTime) {
			aimAngle = (float)Math.Atan2((pos.Y + _texture.Height / 2) - Singleton.Instance.MouseCurrent.Y, (pos.X + _texture.Width / 2) - Singleton.Instance.MouseCurrent.X);
			Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
			Singleton.Instance.MouseCurrent = Mouse.GetState();
		}
		public override void Draw(SpriteBatch _spriteBatch) {
			_spriteBatch.Draw(_texture, pos + new Vector2(50, 50), null, Color.White, aimAngle + MathHelper.ToRadians(-90f), new Vector2(50, 50), 1.5f, SpriteEffects.None, 0f);
		}
		// Random Color
		public Color GetRandomColor() {
            return Color.White;
		}
	}
}