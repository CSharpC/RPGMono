using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class TileSet
    {
        public Texture2D raw;
        public int columns, rows, tileWidth, tileHeight;

        ContentManager content;
        public TileSet(string file, int cols, int rows)
        {
            content = Game1.Instance.Content;
            raw = content.Load<Texture2D>(file);
            columns = cols;
            this.rows = rows;
            tileWidth = raw.Width / cols;
            tileHeight = raw.Height / rows;
        }

        public Rectangle GetTile(int x, int y)
        {
            if (x < columns && y < rows)
                return new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
            else return Rectangle.Empty;
        }

        public Rectangle GetTile(Point p)
        {
            if (p.X < columns && p.Y < rows)
                return new Rectangle(p.X * tileWidth, p.Y * tileHeight, tileWidth, tileHeight);
            else return Rectangle.Empty;
        }
    }
}
