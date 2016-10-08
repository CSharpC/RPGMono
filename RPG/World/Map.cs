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
            layers.Add(new MapLayer("tileset", 2, 2, width, height));            
            Game1.Instance.Drawing += Draw;
        }

        public void generateRandomMap()
        {
            FileStream fs = File.Create(@"C:\\Users\\bombo\\Documents\\generated_map.xml");
            XmlWriter writer = XmlWriter.Create(fs);
            writer.WriteStartElement("map");
            writer.WriteStartElement("layer");
            writer.WriteAttributeString("tileset", "tileset");
            writer.WriteAttributeString("width", width.ToString());
            writer.WriteAttributeString("height", width.ToString());
            writer.WriteAttributeString("ts_rows", "2");
            writer.WriteAttributeString("ts_cols", "2");            
            Random rand = new Random();            
            for (int i = 0;i<height;i++)
            {
                writer.WriteStartElement("row");
                for (int j = 0;j<width;j++)
                {
                    writer.WriteStartElement("tile");
                    writer.WriteAttributeString("texture", Convert.ToString(j<width/2?2:3));
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Close();
            fs.Close();
        }
        public static Map LoadMap()
        {
            XmlDocument doc = new XmlDocument();
            FileStream fs = File.Open(@"C:\\Users\\bombo\\Documents\\generated_map.xml", FileMode.Open);
            doc.Load(fs);
            XmlNode layer = doc.DocumentElement.SelectSingleNode("layer");
            Map result = new Map();
            result.width = Convert.ToInt32(layer.Attributes["width"].InnerText);
            result.height = Convert.ToInt32(layer.Attributes["height"].InnerText);
            int ts_cols = Convert.ToInt32(layer.Attributes["ts_cols"].InnerText);
            int ts_rows = Convert.ToInt32(layer.Attributes["ts_rows"].InnerText);
            result.layers = new List<MapLayer>();
            result.layers.Add(new MapLayer(layer.Attributes["tileset"].InnerText, ts_cols, ts_rows, result.width, result.height));
            for (int i = 0;i<layer.ChildNodes.Count;i++)
            {
                XmlNode row = layer.ChildNodes[i];
                for (int j = 0;j<row.ChildNodes.Count;j++)
                {                    
                    int texture_index = Convert.ToInt32(row.ChildNodes[j].Attributes["texture"].InnerText);
                    int texX = texture_index / ts_rows;
                    int texY = texture_index % ts_cols;
                    result.layers[0].tiles[i, j].texture = new Point(texX, texY);
                }
            }
            fs.Close();
            return result;
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
