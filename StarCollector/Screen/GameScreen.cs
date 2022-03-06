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
		private Texture2D gunTexture;
		private Gun gun;

        public void Initial() {

            gun = new Gun(gunTexture) {
                pos = new Vector2(Singleton.Instance.Dimension.X / 2 - gunTexture.Width / 2, 700 - gunTexture.Height),
            };
        }

        public override void LoadContent() {
            // Load Resource
            base.LoadContent();
            gunTexture = Content.Load<Texture2D>("PlayScreen/bow_sheet");
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
