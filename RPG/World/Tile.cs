using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Tile
    {
        public Tile()
        {
            Width = defaultWidth;
            Height = defaultHeight;
            texture = Point.Zero;
        }
        public Tile(int texX, int texY)
        {
            texture = new Point(texX, texY);
        }

        #region Basic Fields
        public int Width;
        public int Height;
        public Point texture = new Point();
        public string Image { get; set; }
        #endregion
        #region Static Stuff
        static int defaultWidth = 32, defaultHeight = 32;
        public static int DefaultWidth {
            get { return defaultWidth; }
            set { defaultWidth = value; }
        }
        public static int DefaultHeight
        {
            get { return defaultHeight; }
            set { defaultHeight = value; }
        }        
        #endregion

    }
}
