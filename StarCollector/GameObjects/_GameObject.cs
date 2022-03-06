using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarCollector.GameObjects {
    	public class _GameObject {
		protected Texture2D _texture;
		public Vector2 pos;
		public Color color;

		public _GameObject(Texture2D texture) {
			_texture = texture;
			pos = Vector2.Zero;
		}

		public virtual void Update(GameTime gameTime) {
		}
		public virtual void Draw(SpriteBatch spriteBatch) {
		}
    }

}