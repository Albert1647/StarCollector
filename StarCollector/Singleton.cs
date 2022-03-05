using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace StarCollector
{
    class Singleton
    {
        public Vector2 Dimension = new Vector2(1280, 720);
		public MouseState MousePrevious, MouseCurrent;
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
