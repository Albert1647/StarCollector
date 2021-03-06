using StarCollector.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StarCollector.Managers {
	public class ScreenManager {
		// Class responsible for switching screen / hold switching behavior and initialise start screen
		public ContentManager Content { private set; get; }
		public enum GameScreenName {
			MenuScreen,
			GameScreen,
			
			CollectionScreen
		}
		private _GameScreen CurrentGameScreen;
		// Start Screen on Menu
		public ScreenManager() {
			CurrentGameScreen = new MenuScreen();
		}
		// Switch Screen
		public void LoadScreen(GameScreenName _ScreenName) {
			switch (_ScreenName) {
				case GameScreenName.MenuScreen:
					CurrentGameScreen = new MenuScreen();
					break;
				case GameScreenName.GameScreen:
					CurrentGameScreen = new GameScreen();
					break;
				case GameScreenName.CollectionScreen:
					CurrentGameScreen = new CollectionScreen();
					break;
			}
			CurrentGameScreen.LoadContent();
		}
		public void LoadContent(ContentManager Content) {
			this.Content = new ContentManager(Content.ServiceProvider, "Content");
			CurrentGameScreen.LoadContent();
		}
		public void UnloadContent() {
			CurrentGameScreen.UnloadContent();
		}
		// update screen on current screen class
		public void Update(GameTime gameTime) {
			CurrentGameScreen.Update(gameTime);
		}

		// update screen on current screen class
		public void Draw(SpriteBatch spriteBatch) {
			CurrentGameScreen.Draw(spriteBatch);
		}
		// Screen Singleton
		private static ScreenManager instance;
		public static ScreenManager Instance {
			get {
				if (instance == null)
					instance = new ScreenManager();
				return instance;
			}
		}
	}
}
