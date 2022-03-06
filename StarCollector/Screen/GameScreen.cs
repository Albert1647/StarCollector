using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarCollector.GameObjects;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using StarCollector.Managers;
using Microsoft.Xna.Framework.Media;

namespace StarCollector.Screen {
    class GameScreen : _GameScreen {
		private Texture2D GunTexture,StarTexture;
		private Gun gun;

		private SpriteFont Arial;

        public void Initial() {
			// Instantiate gun on GameScren start
            gun = new Gun(GunTexture, StarTexture) {
				pos = new Vector2(Singleton.Instance.Dimension.X / 2 - GunTexture.Width / 2, 700 - GunTexture.Height),
				_gunColor = Color.White
            };
        }

        public override void LoadContent() {
            // Load Resource
            base.LoadContent();
			Arial = Content.Load<SpriteFont>("Arial");
            GunTexture = Content.Load<Texture2D>("gameScreen/gun");
			StarTexture = Content.Load<Texture2D>("gameScreen/star");
            Initial();
        }
        public override void UnloadContent() {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime) {
			gun.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch) {
			_spriteBatch.DrawString(Arial, "Is Shooting = " + Singleton.Instance.IsShooting , new Vector2(0,0), Color.Black);
			gun.Draw(_spriteBatch);
        }
    }
}
