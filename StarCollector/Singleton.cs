using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace StarCollector
{
    class Singleton
    {
		// Game Resolution
        public Vector2 Dimension = new Vector2(1280, 720);
		public List<Vector2> RemovableStar = new List<Vector2>();
		public MouseState MousePrevious, MouseCurrent;
		public bool IsShooting;
        private static Singleton instance;
		// Export Instance
		public int Score = 0;

		public int ceilingY = 30;
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
