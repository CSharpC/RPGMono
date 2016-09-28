using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.World
{
    public class Tile
    {
        public Tile()
        {
            Width = defaultWidth;
            Height = defaultHeight;
        }
        public Tile(string image)
        {
            Image = image;
        }

        #region Basic Fields
        public int Width { get; set; }
        public int Height { get; set; }
        public string Image { get; set; }
        #endregion
        #region Static Stuff
        static int defaultWidth = 32, defaultHeight = 32;
        public static void ChangeDefaultDimensions(int width, int height)
        {
            defaultWidth = width;
            defaultHeight = height;
        }
        #endregion

    }
}
