using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace StarCollector
{
    class Singleton
    {
		// Game Resolution
        public Vector2 Dimension = new Vector2(1280, 720);
		public List<Vector2> RemovableStar = new List<Vector2>();
		public MouseState MousePrevious, MouseCurrent;
		private Random random = new Random();
		public bool IsShooting;
		public int Score = 0;
		public int currentLevel = 2;
		public List<Color> starColor = new List<Color>();
		public int oldCeilingY = 30;
		public int ceilingY = 30;

		public Color GetColor(){
			List<Color> color = new List<Color>();
			color.Add(new Color(255 ,85, 85));
			color.Add(Color.Blue);
			color.Add(Color.Green);
			color.Add(Color.Yellow);
			switch(Singleton.Instance.currentLevel){
				case 1 : case 2 : 
					break;
				case 3 : 
					color.Add(Color.Purple);
					break;
				case 4 : case 5 : 
					color.Add(Color.Purple);
					color.Add(Color.White);
					break;
				case 6 :
					color.Add(Color.Purple);
					color.Add(Color.White);
					color.Add(Color.SkyBlue);
					break;
				default :
					break;
			}
			return color[random.Next(0, color.Count)];
		}
		// Export Instance
        private static Singleton instance;
		public static Singleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Singleton();
				}
				return instance;
			}
		}
	}
}
