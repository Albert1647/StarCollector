using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarCollector.Managers;
using StarCollector.GameObjects;
using System;
using Microsoft.Xna.Framework.Audio;

namespace StarCollector.Screen {
	class GameScreen : _GameScreen {
		public void Initial() {

		}
		public override void LoadContent() {
			base.LoadContent();

			Initial();
		}
		public override void UnloadContent() {
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime) {
            
			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch spriteBatch) {

		}
	}
}
