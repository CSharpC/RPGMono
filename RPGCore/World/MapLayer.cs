using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.World
{
    public class MapLayer
    {
        public MapLayer()
        {
            tiles.Initialize();
        }
        public Tile[,] tiles; 

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[x, y] = tile;
        }
    }
}
