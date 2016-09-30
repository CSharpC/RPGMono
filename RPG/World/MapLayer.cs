using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class MapLayer
    {
        public Tile[,] tiles;
        public TileSet tileSet;

        public MapLayer(string tileset, int tsColumns, int tsRows, int width, int height)
        {
            tiles = new Tile[width, height];
            InitializeTiles();
            tileSet = new TileSet(tileset, tsColumns, tsRows);
        }

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[x, y] = tile;
        }

        private void InitializeTiles()
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j] = new Tile();
        }
    }
}
