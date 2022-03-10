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
		public int currentLevel = 1;

		public int STARHITBOX = 74;
		public int rowGapClosing = 10;
		public int oldCeilingY = 30;
		public int ceilingY = 30;
		public List<Color> starColor = new List<Color>();
		public int clearStar = 0;

		public Color GetColor(){
			List<Color> color = new List<Color>();
			color.Add(new Color(255 ,85, 85));
			color.Add(new Color(64, 64, 184));
			color.Add(new Color(72, 200, 72));
			color.Add(new Color(255, 255, 25));
			switch(Singleton.Instance.currentLevel){
				case 1 : case 2 : 
					break;
				case 3 : 
					color.Add(new Color(149, 85, 213));
					break;
				case 4 : case 5 : 
					color.Add(new Color(149, 85, 213));
					color.Add(new Color(255, 255, 255));
					break;
				case 6 :
					color.Add(new Color(149, 85, 213));
					color.Add(new Color(255, 255, 255));
					color.Add(new Color(72, 136, 200));
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
