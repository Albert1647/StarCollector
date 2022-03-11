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
		private Texture2D leave ,HoverLeave;
		private Texture2D question, squareBG, Menu_bg;
		private Texture2D warpOne, warpTwo, warpThree, warpFour, warpFive, warpSix;
		private Texture2D detailsWarpOne, detailsWarpTwo, detailsWarpThree, detailsWarpFour, detailsWarpFive, detailsWarpSix;
		private SoundEffect Click, HoverMenu;
		private bool showWarbOne, showWarbTwo, showWarbThree, showWarbFour, showWarbFive, showWarbSix;
		private bool clickOtherStar = true;
		private bool showHoverLeave;
		private bool HoverCloseButton, HoverCloseButtonInStar1, HoverCloseButtonInStar2, HoverCloseButtonInStar3, HoverCloseButtonInStar4, HoverCloseButtonInStar5, HoverCloseButtonInStar6,
					 HoverStar1, HoverStar2, HoverStar3, HoverStar4, HoverStar5, HoverStar6;
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
			HoverLeave = Content.Load<Texture2D>("CollectionScreen/HoverLeave");
			Click = Content.Load<SoundEffect>("Sound/click");
			HoverMenu = Content.Load<SoundEffect>("Sound/menu_select");

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
				if(HoverCloseButton == false) {
					HoverMenu.Play();
					HoverCloseButton = true;
                }
				showHoverLeave = true;
				if (IsClick())
				{
					Click.Play();
					ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
				}
			}else
			{
				showHoverLeave = false;
				HoverCloseButton = false;
			}

			//unlock star 1 when win
			if (Singleton.Instance.clearStar > 0)
			{
				
				// Click WarbOne
				if (MouseOnElement(141, 341, 157, 357) && clickOtherStar)
				{
					if (HoverStar1 == false)
					{
						HoverMenu.Play();
						HoverStar1 = true;
					}
					if (IsClick())
					{
						Click.Play();
						showWarbOne = true;
					}
				} else 
				{
					HoverStar1 = false;
                }
				// Leave to showWarbOne
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbOne && !clickOtherStar)
				{
					showHoverLeave = true;
					if (HoverCloseButtonInStar1 == false)
					{
						HoverMenu.Play();
						HoverCloseButtonInStar1 = true;
					}
					if (IsClick())
					{
						Click.Play();
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
					}
				}
				else if (showWarbOne)
				{
					showHoverLeave = false;
					HoverCloseButtonInStar1 = false;
				}
			}

			//unlock star 2 when win
			if (Singleton.Instance.clearStar > 1)
			{
				// Click WarbTwo
				if (MouseOnElement(540, 740, 157, 357) && clickOtherStar)
				{
					if (HoverStar2 == false)
					{
						HoverMenu.Play();
						HoverStar2 = true;
					}
					if (IsClick())
					{
						Click.Play();
						showWarbTwo = true;
					}
				} else
                {
					HoverStar2 = false;
                }
				// Leave to showWarbTwo
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbTwo && !clickOtherStar)
				{
					showHoverLeave = true;
					if (HoverCloseButtonInStar2 == false)
					{
						HoverMenu.Play();
						HoverCloseButtonInStar2 = true;
					}
					if (IsClick())
					{
						Click.Play();
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
					}
				}else if(showWarbTwo)
				{
					showHoverLeave = false;
					HoverCloseButtonInStar2 = false;
				}
			}

			//unlock star 3 when win
			if (Singleton.Instance.clearStar > 2)
			{
				// Click WarbThree
				if (MouseOnElement(939, 1139, 157, 357) && clickOtherStar)
				{
					if (HoverStar3 == false)
					{
						HoverMenu.Play();
						HoverStar3 = true;
					}
					if (IsClick())
					{
						Click.Play();
						showWarbThree = true;
					}
				} else
                {
					HoverStar3 = false;
                }
				// Leave to showWarbThree
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbThree && !clickOtherStar)
				{
					showHoverLeave = true;
					if (HoverCloseButtonInStar3 == false)
					{
						HoverMenu.Play();
						HoverCloseButtonInStar3 = true;
					}
					if (IsClick())
					{
						Click.Play();
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
					}
				}
				else if (showWarbThree)
				{
					showHoverLeave = false;
					HoverCloseButtonInStar3 = false;
				}
			}

			//unlock star 4 when win
			if (Singleton.Instance.clearStar > 3)
			{
				// Click WarbFour
				if (MouseOnElement(141, 341, 407, 607) && clickOtherStar)
				{
					if (HoverStar4 == false)
					{
						HoverMenu.Play();
						HoverStar4 = true;
					}
					if (IsClick())
					{
						Click.Play();
						showWarbFour = true;
					}
				} else
                {
					HoverStar4 = false;
                }
				// Leave to showWarbThree
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbFour && !clickOtherStar)
				{
					showHoverLeave = true;
					if (HoverCloseButtonInStar4 == false)
					{
						HoverMenu.Play();
						HoverCloseButtonInStar4 = true;
					}
					if (IsClick())
					{
						Click.Play();
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
					}
				}
				else if (showWarbFour)
				{
					showHoverLeave = false;
					HoverCloseButtonInStar4 = false;
				}
			}

			//unlock star 5 when win
			if (Singleton.Instance.clearStar > 4)
			{
				// Click WarbFive
				if (MouseOnElement(540, 740, 407, 607) && clickOtherStar)
				{
					if (HoverStar5 == false)
					{
						HoverMenu.Play();
						HoverStar5 = true;
					}
					if (IsClick())
					{
						Click.Play();
						showWarbFive = true;
					}
				} else
                {
					HoverStar5 = false;
                }
				// Leave to showWarbFive
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbFive && !clickOtherStar)
				{
					showHoverLeave = true;
					if (HoverCloseButtonInStar5 == false)
					{
						HoverMenu.Play();
						HoverCloseButtonInStar5 = true;
					}
					if (IsClick())
					{
						Click.Play();
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
					}
				}
				else if (showWarbFive)
				{
					showHoverLeave = false;
					HoverCloseButtonInStar5 = false;
				}
			}


			//unlock star 6 when win
			if (Singleton.Instance.clearStar > 5 )
			{
				if (MouseOnElement(939, 1139, 407, 607) && clickOtherStar)
				{
					if (HoverStar6 == false)
					{
						HoverMenu.Play();
						HoverStar6 = true;
					}
					if (IsClick())
					{
						showWarbSix = true;
					}
				} else
                {
					HoverStar6 = false;
                }
				// Leave to showWarbFive
				if (MouseOnElement(1150, 1196, 80, 118) && showWarbSix && !clickOtherStar)
				{
					showHoverLeave = true;
					if (HoverCloseButtonInStar6 == false)
					{
						HoverMenu.Play();
						HoverCloseButtonInStar6 = true;
					}
					if (IsClick())
					{
						Click.Play();
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
						clickOtherStar = true;
					}
				}
				else if (showWarbSix)
				{
					showHoverLeave = false;
					HoverCloseButtonInStar6 = false;
				}
			}

			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch _spriteBatch)
		{
			_spriteBatch.Draw(Menu_bg, new Vector2(0, 0), Color.White);
			//Draw question BG 
			_spriteBatch.Draw(squareBG, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
			//Draw Hover Leave Button
			if (showHoverLeave)
			{
				_spriteBatch.Draw(HoverLeave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
			}
			else
			{
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
			}

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
				clickOtherStar = false;
				//Draw Hover Leave Button
				if (showHoverLeave)
				{
					_spriteBatch.Draw(HoverLeave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
				else {
					//Draw Leave Button
					_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
			}
			// details Star 2
			if (showWarbTwo)
			{
				//Draw details Star 2
				_spriteBatch.Draw(detailsWarpTwo, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				clickOtherStar = false;
				//Draw Hover Leave Button
				if (showHoverLeave)
				{
					_spriteBatch.Draw(HoverLeave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
				else
				{
					//Draw Leave Button
					_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
			}
			// details Star 3
			if (showWarbThree)
			{
				//Draw details Star 3
				_spriteBatch.Draw(detailsWarpThree, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				clickOtherStar = false;
				//Draw Hover Leave Button
				if (showHoverLeave)
				{
					_spriteBatch.Draw(HoverLeave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
				else
				{
					//Draw Leave Button
					_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
			}
			// details Star 4
			if (showWarbFour)
			{
				//Draw details Star 4
				_spriteBatch.Draw(detailsWarpFour, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				clickOtherStar = false;
				//Draw Hover Leave Button
				if (showHoverLeave)
				{
					_spriteBatch.Draw(HoverLeave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
				else
				{
					//Draw Leave Button
					_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
			}
			//details Star 5
			if (showWarbFive)
			{
				//Draw details Star 5
				_spriteBatch.Draw(detailsWarpFive, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				clickOtherStar = false;
				//Draw Hover Leave Button
				if (showHoverLeave)
				{
					_spriteBatch.Draw(HoverLeave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
				else
				{
					//Draw Leave Button
					_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
			}
			//details Star 6
			if (showWarbSix)
			{
				//Draw details Star 6
				_spriteBatch.Draw(detailsWarpSix, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
				clickOtherStar = false;
				//Draw Hover Leave Button
				if (showHoverLeave)
				{
					_spriteBatch.Draw(HoverLeave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
				else
				{
					//Draw Leave Button
					_spriteBatch.Draw(leave, new Rectangle(1150, 80, leave.Width, leave.Height), Color.White);
				}
			}


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
