using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Map
    {
        public int width, height;
        public List<MapLayer> layers;
        public Map()
        {

        }
        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            layers = new List<MapLayer>();                   
            Game1.Instance.Drawing += Draw;
        }
         
        ~Map()
        {
            Game1.Instance.Drawing -= Draw;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var batch = Game1.Instance.spriteBatch;
            batch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.FrontToBack);
            for (int i = 0; i < layers.Count(); i++)
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        var t = layers[i].tiles[x, y];
                        batch.Draw(layers[i].tileSet.raw, new Rectangle(x * t.Width, y * t.Height, t.Width, t.Height), layers[i].tileSet.GetTile(t.texture), Color.White);
                    }
            batch.End();
        }
    }
}
