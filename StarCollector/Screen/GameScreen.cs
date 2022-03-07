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
		private Texture2D GunTexture,StarTexture,Indicator;
		private Gun gun;
        private Random random = new Random();
		private Star[,] star = new Star[9,8];

		private SpriteFont Arial;

        public void Initial() {
			// Instantiate gun on start GameScreen 
            gun = new Gun(GunTexture, Indicator, StarTexture) {
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
			Indicator = Content.Load<Texture2D>("gameScreen/indicator");
            Initial();
        }
        public override void UnloadContent() {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime) {

            
            // update/load gun logic
			gun.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch) {
			_spriteBatch.DrawString(Arial, "Is Shooting = " + Singleton.Instance.IsShooting , new Vector2(0,0), Color.Black);
            // draw gun
			gun.Draw(_spriteBatch);
        }

        public Color GetRandomColor() {
			Color _starColor = Color.Black;
			switch (random.Next(0, 6)) {
				case 0:
					_starColor = Color.White;
					break;
				case 1:
					_starColor = Color.Blue;
					break;
				case 2:
					_starColor = Color.Yellow;
					break;
				case 3:
					_starColor = Color.Red;
					break;
				case 4:
					_starColor = Color.Green;
					break;
				case 5:
					_starColor = Color.Purple;
					break;
			}
			return _starColor;
		}
    }
}
