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
		private Texture2D detailsWarpOne;
		private bool showWarbOne;
		private bool showWarbOnedraw;
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
			//��һŴ������� ����㹹��(���ѧ������йѺ�ѧ�)
			// Click WarbOne
			if (MouseOnElement(141,341,157,357))
			{
				//141, 157
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
			_spriteBatch.Draw(question, new Rectangle(540, 157, question.Width, question.Height), Color.White);
			_spriteBatch.Draw(question, new Rectangle(939, 157, question.Width, question.Height), Color.White);
			_spriteBatch.Draw(question, new Rectangle(141, 407, question.Width, question.Height), Color.White);
			_spriteBatch.Draw(question, new Rectangle(540, 407, question.Width, question.Height), Color.White);
			_spriteBatch.Draw(question, new Rectangle(939, 407, question.Width, question.Height), Color.White);

			//mouse
			_spriteBatch.DrawString(Arial, "X = " + Singleton.Instance.MouseCurrent.X, new Vector2(0, 0), Color.Black);
			_spriteBatch.DrawString(Arial, "Y = " + Singleton.Instance.MouseCurrent.Y, new Vector2(0, 40), Color.Black);
			_spriteBatch.DrawString(Arial, "Click ?  " + IsClick(), new Vector2(0, 60), Color.Black);




			// Draw About Screen
			if (showWarbOne)
			{
				_spriteBatch.Draw(detailsWarpOne, new Rectangle(50, 50, 1280 - 100, 720 - 100), Color.White);
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
