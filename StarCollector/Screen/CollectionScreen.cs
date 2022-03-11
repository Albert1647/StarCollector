using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarCollector.Managers;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace StarCollector.Screen
{
	class CollectionScreen : _GameScreen
	{
		private SpriteFont Arial;
		private Texture2D leave;
		private Texture2D question, squareBG, Menu_bg;
		private Texture2D warpOne, warpTwo, warpThree, warpFour, warpFive, warpSix;
		private Texture2D detailsWarpOne, detailsWarpTwo, detailsWarpThree, detailsWarpFour, detailsWarpFive, detailsWarpSix;
		private bool showWarbOne, showWarbTwo, showWarbThree, showWarbFour, showWarbFive, showWarbSix;
		private bool showWarbOnedraw, showWarbTwodraw, showWarbThreedraw, showWarbFourdraw, showWarbFivedraw, showWarbSixdraw;
		private bool clickOtherStar = true;
		public void Initial()
		{

		}
		public override void LoadContent()
		{
			base.LoadContent();
			//location file
			Arial = Content.Load<SpriteFont>("Arial");
			leave = Content.Load<Texture2D>("CollectionScreen/leave");
			warpOne = Content.Load<Texture2D>("CollectionScreen/warp_One");
			warpTwo = Content.Load<Texture2D>("CollectionScreen/warp_Two");
			warpThree = Content.Load<Texture2D>("CollectionScreen/warp_Three");
			warpFour = Content.Load<Texture2D>("CollectionScreen/warp_Four");
			warpFive = Content.Load<Texture2D>("CollectionScreen/warp_Five");
			warpSix = Content.Load<Texture2D>("CollectionScreen/warp_Six");
			detailsWarpOne = Content.Load<Texture2D>("CollectionScreen/details_warp_One");
			detailsWarpTwo = Content.Load<Texture2D>("CollectionScreen/details_warp_Two");
			detailsWarpThree = Content.Load<Texture2D>("CollectionScreen/details_warp_Three");
			detailsWarpFour = Content.Load<Texture2D>("CollectionScreen/details_warp_Four");
			detailsWarpFive = Content.Load<Texture2D>("CollectionScreen/details_warp_Five");
			detailsWarpSix = Content.Load<Texture2D>("CollectionScreen/details_warp_Six");
			question = Content.Load<Texture2D>("CollectionScreen/question");
			squareBG = Content.Load<Texture2D>("CollectionScreen/squareBG");
			Menu_bg = Content.Load<Texture2D>("MenuScreen/menu_bg");

			Initial();
		}
		public override void UnloadContent()
		{
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime)
		{
			// Save Current Mouse Position
			Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
			Singleton.Instance.MouseCurrent = Mouse.GetState();

			// Leave to menu 1150, 80
			if (MouseOnElement(1150, 1196, 80, 118) && clickOtherStar)
			{
				if (IsClick())
				{
					ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
				}
			}

			//unlock star 1 when win
			if (Singleton.Instance.clearStar > 0)
			{
				// Click WarbOne
				if (MouseOnElement(141, 341, 157, 357) && clickOtherStar)
				{
					if (IsClick())
					{
						showWarbOne = true;
					}
				}
				// Leave to showWarbOne
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbOne && !clickOtherStar)
				{
					showWarbOnedraw = true;
					if (IsClick())
					{
						
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
						showWarbOnedraw = false;
						
					}
				}
			}

			//unlock star 2 when win
			if (Singleton.Instance.clearStar > 1)
			{
				// Click WarbTwo
				if (MouseOnElement(540, 740, 157, 357) && clickOtherStar)
				{
					if (IsClick())
					{
						showWarbTwo = true;
					}
				}
				// Leave to showWarbTwo
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbTwo)
				{
					showWarbTwodraw = true;
					if (IsClick())
					{
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
						showWarbTwodraw = false;
					}
				}
			}

			//unlock star 3 when win
			if (Singleton.Instance.clearStar > 2)
			{
				// Click WarbThree
				if (MouseOnElement(939, 1139, 157, 357) && clickOtherStar)
				{
					if (IsClick())
					{
						showWarbThree = true;
					}
				}
				// Leave to showWarbThree
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbThree)
				{
					showWarbThreedraw = true;
					if (IsClick())
					{
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
						showWarbThreedraw = false;
					}
				}
			}

			//unlock star 4 when win
			if (Singleton.Instance.clearStar > 3)
			{
				// Click WarbFour
				if (MouseOnElement(141, 341, 407, 607) && clickOtherStar)
				{
					if (IsClick())
					{
						showWarbFour = true;
					}
				}
				// Leave to showWarbThree
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbFour)
				{
					showWarbFourdraw = true;
					if (IsClick())
					{
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
						showWarbFourdraw = false;
					}
				}
			}

			//unlock star 5 when win
			if (Singleton.Instance.clearStar > 4)
			{
				// Click WarbFive
				if (MouseOnElement(540, 740, 407, 607) && clickOtherStar)
				{
					if (IsClick())
					{
						showWarbFive = true;
					}
				}
				// Leave to showWarbFive
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbFive)
				{
					showWarbFivedraw = true;
					if (IsClick())
					{
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
						showWarbFivedraw = false;
					}
				}
			}


			//unlock star 6 when win
			if (Singleton.Instance.clearStar > 5 )
			{
				if (MouseOnElement(939, 1139, 407, 607) && clickOtherStar)
				{
					if (IsClick())
					{
						showWarbSix = true;
					}
				}
				// Leave to showWarbFive
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbSix)
				{
					showWarbSixdraw = true;
					if (IsClick())
					{
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
						showWarbSixdraw = false;
					}
				}
			}

			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch _spriteBatch)
		{
			_spriteBatch.Draw(Menu_bg, new Vector2(0, 0), Color.White);
			//Draw question BG 
			_spriteBatch.Draw(squareBG, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
			//Draw Leave Button
			_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);

			//question
			_spriteBatch.Draw(question, new Rectangle(195, 100, 196, 220), Color.White);
			_spriteBatch.Draw(question, new Rectangle(542, 100, 196, 220), Color.White);
			_spriteBatch.Draw(question, new Rectangle(889, 100, 196, 220), Color.White);
			_spriteBatch.Draw(question, new Rectangle(195, 412, 196, 220), Color.White);
			_spriteBatch.Draw(question, new Rectangle(542, 412, 196, 220), Color.White);
			_spriteBatch.Draw(question, new Rectangle(889, 412, 196, 220), Color.White);
			//Draw Star
			//draw star 1 when win
			if (Singleton.Instance.clearStar > 0)
			{
				_spriteBatch.Draw(warpOne, new Rectangle(195, 100, 196, 220), Color.White);
			}
			//draw star 2 when win
			if (Singleton.Instance.clearStar > 1)
			{
				_spriteBatch.Draw(warpTwo, new Rectangle(542, 100, warpTwo.Width, warpTwo.Height), Color.White);
			}
			//draw star 3 when win
			if (Singleton.Instance.clearStar > 2)
			{
				_spriteBatch.Draw(warpThree, new Rectangle(889, 100, warpThree.Width, warpThree.Height), Color.White);
			}
			//draw star 4 when win
			if (Singleton.Instance.clearStar > 3)
			{
				_spriteBatch.Draw(warpFour, new Rectangle(195, 412, warpFour.Width, warpFour.Height), Color.White);
			}
			//draw star 5 when win
			if (Singleton.Instance.clearStar > 4)
			{
				_spriteBatch.Draw(warpFive, new Rectangle(542, 412, warpFive.Width, warpFive.Height), Color.White);
			}
			//draw star 6 when win
			if (Singleton.Instance.clearStar > 5)
			{
				_spriteBatch.Draw(warpSix, new Rectangle(889, 412, warpSix.Width, warpSix.Height), Color.White);
			}

			//mouse
			_spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X, new Vector2(0, 0), Color.Black);
			_spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
			_spriteBatch.DrawString(Arial, "Click ?  " + IsClick(), new Vector2(0, 60), Color.Black);


			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// details Star 1
			if (showWarbOne)
			{
				//Draw details Star 1
				_spriteBatch.Draw(detailsWarpOne, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				clickOtherStar = false;
			}
			// details Star 2
			if (showWarbTwo)
			{
				//Draw details Star 2
				_spriteBatch.Draw(detailsWarpTwo, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				clickOtherStar = false;
			}
			// details Star 3
			if (showWarbThree)
			{
				//Draw details Star 3
				_spriteBatch.Draw(detailsWarpThree, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				clickOtherStar = false;
			}
			// details Star 4
			if (showWarbFour)
			{
				//Draw details Star 4
				_spriteBatch.Draw(detailsWarpFour, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				clickOtherStar = false;
			}
			//details Star 5
			if (showWarbFive)
			{
				//Draw details Star 5
				_spriteBatch.Draw(detailsWarpFive, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				clickOtherStar = false;
			}
			//details Star 6
			if (showWarbSix)
			{
				//Draw details Star 6
				_spriteBatch.Draw(detailsWarpSix, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				clickOtherStar = false;
			}
			_spriteBatch.DrawString(Arial, "Mouse on Start ?  " + showWarbOnedraw, new Vector2(0, 80), Color.Black);


		}
		// helper function to shorten singleton calling
		public bool IsClick()
		{
			return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
		}
		public bool MouseOnElement(int x1, int x2, int y1, int y2)
		{
			return (Singleton.Instance.MouseCurrent.X > x1 && Singleton.Instance.MouseCurrent.Y > y1) && (Singleton.Instance.MouseCurrent.X < x2 && Singleton.Instance.MouseCurrent.Y < y2);
		}
	}

}
