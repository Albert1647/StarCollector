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
		private Texture2D squareBG, leave;
		private Texture2D question, warpOne, warpTwo, warpThree, warpFour, warpFive , warpSix;
		private Texture2D detailsWarpOne, detailsWarpTwo, detailsWarpThree, detailsWarpFour, detailsWarpFive, detailsWarpSix;
		private bool showWarbOne, showWarbTwo , showWarbThree , showWarbFour, showWarbFive, showWarbSix ;
		private bool showWarbOnedraw, showWarbTwodraw, showWarbThreedraw, showWarbFourdraw, showWarbFivedraw, showWarbSixdraw ;
		public void Initial()
		{

		}
		public override void LoadContent()
		{
			base.LoadContent();
			//location file
			Arial = Content.Load<SpriteFont>("Arial");
			squareBG = Content.Load<Texture2D>("CollectionScreen/bg_collection");
			leave = Content.Load<Texture2D>("CollectionScreen/leave");
			question = Content.Load<Texture2D>("CollectionScreen/question");
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

			// Leave to menu
			if (MouseOnElement(1140,1204,70,135))
			{
				if(IsClick()){
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                }
			}
			
			//if(passStarOne)
			// Click WarbOne
			if (MouseOnElement(141,341,157,357))
			{
				if (IsClick())
				{
					showWarbOne = true;
				}	
			}
			// Leave to showWarbOne
			if (MouseOnElement(1140,1204,70,135) && showWarbOne)
			{	
				showWarbOnedraw=true;
				if(IsClick())
				{
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
					showWarbOnedraw = false;
				}
			}

			//if(passStarTwo)
			// Click WarbTwo
			if (MouseOnElement(540, 740, 157, 357))
			{
				if (IsClick())
				{
					showWarbTwo = true;
				}
			}
			// Leave to showWarbTwo
			if (MouseOnElement(1140, 1204, 70, 135) && showWarbTwo)
			{
				showWarbTwodraw = true;
				if (IsClick())
				{
					ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
					showWarbTwodraw = false;
				}
			}

			//if(passStarThree)
			// Click WarbThree
			if (MouseOnElement(939, 1139, 157, 357))
			{
				if (IsClick())
				{
					showWarbThree = true;
				}
			}
			// Leave to showWarbThree
			if (MouseOnElement(1140, 1204, 70, 135) && showWarbThree)
			{
				showWarbThreedraw = true;
				if (IsClick())
				{
					ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
					showWarbThreedraw = false;
				}
			}

			//if(passStarFour)
			// Click WarbFour
			if (MouseOnElement(141, 341, 407, 607))
			{
				if (IsClick())
				{
					showWarbFour = true;
				}
			}
			// Leave to showWarbThree
			if (MouseOnElement(1140, 1204, 70, 135) && showWarbFour)
			{
				showWarbFourdraw = true;
				if (IsClick())
				{
					ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
					showWarbFourdraw = false;
				}
			}

			//if(passStarFive)
			// Click WarbFive
			if (MouseOnElement(540, 740, 407, 607))
			{
				if (IsClick())
				{
					showWarbFive = true;
				}
			}
			// Leave to showWarbFive
			if (MouseOnElement(1140, 1204, 70, 135) && showWarbFive)
			{
				showWarbFivedraw = true;
				if (IsClick())
				{
					ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
					showWarbFivedraw = false;
				}
			}
			//if(passStarSix)
			if (MouseOnElement(939, 1139, 407, 607))
			{
				if (IsClick())
				{
					showWarbSix = true;
				}
			}
			// Leave to showWarbFive
			if (MouseOnElement(1140, 1204, 70, 135) && showWarbSix)
			{
				showWarbSixdraw = true;
				if (IsClick())
				{
					ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.CollectionScreen);
					showWarbSixdraw = false;
				}
			}


			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch _spriteBatch)
		{
			//Draw squareBG
			_spriteBatch.Draw(squareBG, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
			//Draw Leave Button
			_spriteBatch.Draw(leave, new Rectangle(1140, 70, leave.Width, leave.Height), Color.White);
			//Draw Star
			_spriteBatch.Draw(warpOne, new Rectangle(141, 157, warpOne.Width, warpOne.Height), Color.White);
			_spriteBatch.Draw(warpTwo, new Rectangle(540, 157, warpTwo.Width, warpTwo.Height), Color.White);
			_spriteBatch.Draw(warpThree, new Rectangle(939, 157, warpThree.Width, warpThree.Height), Color.White);
			_spriteBatch.Draw(warpFour, new Rectangle(141, 407, warpFour.Width, warpFour.Height), Color.White);
			_spriteBatch.Draw(warpFive, new Rectangle(540, 407, warpFive.Width, warpFive.Height), Color.White);
			_spriteBatch.Draw(warpSix, new Rectangle(939, 407, warpSix.Width, warpSix.Height), Color.White);
			//question
			//_spriteBatch.Draw(question, new Rectangle(141, 157, question.Width, question.Height), Color.White);
			//_spriteBatch.Draw(question, new Rectangle(540, 157, question.Width, question.Height), Color.White);
			//_spriteBatch.Draw(question, new Rectangle(939, 157, question.Width, question.Height), Color.White);
			//_spriteBatch.Draw(question, new Rectangle(141, 407, question.Width, question.Height), Color.White);
			//_spriteBatch.Draw(question, new Rectangle(540, 407, question.Width, question.Height), Color.White);
			//_spriteBatch.Draw(question, new Rectangle(939, 407, question.Width, question.Height), Color.White);

			//mouse
			_spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X, new Vector2(0, 0), Color.Black);
			_spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
			_spriteBatch.DrawString(Arial, "Click ?  " + IsClick(), new Vector2(0, 60), Color.Black);


			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// Draw showWarbOne Screen
			if (showWarbOne)
			{
				//Draw details Star 1
				_spriteBatch.Draw(detailsWarpOne, new Rectangle(57, 57, 1280 - 114, 720 - 114), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1140, 70, leave.Width, leave.Height), Color.White);
			}
			// Draw showWarbOne Screen
			if (showWarbTwo)
			{
				//Draw details Star 1
				_spriteBatch.Draw(detailsWarpTwo, new Rectangle(57, 57, 1280 - 114, 720 - 114), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1140, 70, leave.Width, leave.Height), Color.White);
			}
			// Draw showWarbOne Screen
			if (showWarbThree)
			{
				//Draw details Star 1
				_spriteBatch.Draw(detailsWarpThree, new Rectangle(57, 57, 1280 - 114, 720 - 114), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1140, 70, leave.Width, leave.Height), Color.White);
			}
			if (showWarbFour)
			{
				//Draw details Star 1
				_spriteBatch.Draw(detailsWarpFour, new Rectangle(57, 57, 1280 - 114, 720 - 114), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1140, 70, leave.Width, leave.Height), Color.White);
			}
			if (showWarbFive)
			{
				//Draw details Star 1
				_spriteBatch.Draw(detailsWarpFive, new Rectangle(57, 57, 1280 - 114, 720 - 114), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1140, 70, leave.Width, leave.Height), Color.White);
			}
			if (showWarbSix)
			{
				//Draw details Star 1
				_spriteBatch.Draw(detailsWarpSix, new Rectangle(57, 57, 1280 - 114, 720 - 114), Color.White);
				//Draw Leave Button
				_spriteBatch.Draw(leave, new Rectangle(1140, 70, leave.Width, leave.Height), Color.White);
			}
			_spriteBatch.DrawString(Arial, "Mouse on Start ?  " + showWarbOnedraw, new Vector2(0, 80), Color.Black);
			
			
		}
		// helper function to shorten singleton calling
		public bool IsClick()
		{
			return Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released;
		}
		public bool MouseOnElement(int x1, int x2, int y1, int y2){
            return (Singleton.Instance.MouseCurrent.X > x1 && Singleton.Instance.MouseCurrent.Y > y1) && (Singleton.Instance.MouseCurrent.X < x2 && Singleton.Instance.MouseCurrent.Y < y2);
        }
	}
	
}
