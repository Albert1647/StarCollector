using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace StarCollector.GameObjects {
		// Class hold basic attribute of gameObject
    	public class _GameObject {
		protected Texture2D _texture;
		public Vector2 pos;
		
		public _GameObject(Texture2D texture) {
			_texture = texture; // initialise gameObject texture
			pos = Vector2.Zero; // default location
		}

		public virtual void Update(GameTime gameTime, Star[,] gameObjects) {
		}
		public virtual void Draw(SpriteBatch spriteBatch) {
		}
    }

}