using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace RPG.World
{
    /// <summary>
    /// <para>Saves and loads map data.</para>
    /// </summary>
    public class MapSerializer
    {
        
        #region Static Functions
        public static void Serialize(Map map)
        {
            FileStream file = File.Create("serialized_map.xml");
            XmlWriter writer = XmlWriter.Create(file);
            writer.WriteStartElement("map");
            writer.WriteAttributeString("width", Convert.ToString(map.width));
            writer.WriteAttributeString("height", Convert.ToString(map.height));
            foreach (var layer in map.layers)
            {
                writer.WriteStartElement("layer");               
                writer.WriteAttributeString("tileset", layer.tsName);
                writer.WriteAttributeString("ts_rows", Convert.ToString(layer.tileSet.rows));
                writer.WriteAttributeString("ts_cols", Convert.ToString(layer.tileSet.columns));
                for (int i = 0;i<map.width;i++)
                {
                    writer.WriteStartElement("row");
                    for (int j = 0;j<map.height;j++)
                    {
                        writer.WriteStartElement("tile");
                        int textureIndex = layer.tiles[i, j].texture.Y * layer.tileSet.rows + layer.tiles[i, j].texture.X;
                        writer.WriteAttributeString("texture", Convert.ToString(textureIndex));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Close();
            file.Close();
        }

        public static Map Deserialize(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            XmlNode root = doc.DocumentElement;
            int width = Convert.ToInt32(root.Attributes["width"].InnerText);
            int height = Convert.ToInt32(root.Attributes["height"].InnerText);
            var map = new Map(width, height);
            foreach (XmlElement layer in doc.GetElementsByTagName("layer"))
            {
                int ts_rows = Convert.ToInt32(layer.GetAttribute("ts_rows"));
                int ts_cols = Convert.ToInt32(layer.GetAttribute("ts_cols"));
                string tileset = layer.GetAttribute("tileset");
                map.layers.Add(new MapLayer(tileset, ts_cols, ts_rows, width, height));
                for (int i = 0; i < layer.ChildNodes.Count; i++)
                {
                    XmlNode row = layer.ChildNodes[i];
                    for (int j = 0; j < row.ChildNodes.Count; j++)
                    {
                        int texture_index = Convert.ToInt32(row.ChildNodes[j].Attributes["texture"].InnerText);
                        int texX = texture_index % ts_cols;
                        int texY = texture_index / ts_rows;                        
                        map.layers[0].tiles[i, j].texture = new Point(texX, texY);
                    }
                }
            }
            //TODO: Change global tile size if needed.
            return map;
        }
        #endregion
    }
}
