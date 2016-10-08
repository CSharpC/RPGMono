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
            FileStream file = File.Create("serialized_map.xml"); //Create a new file.
            XmlWriter writer = XmlWriter.Create(file); //Create a new XmlWriter.
            /* Create the root element for our xml, <map>
             * It will look like this:
             * <map width="24" height="24">
             */
            writer.WriteStartElement("map");
            writer.WriteAttributeString("width", Convert.ToString(map.width));
            writer.WriteAttributeString("height", Convert.ToString(map.height));
            foreach (var layer in map.layers)
            {
                /* For every layer, create a <layer> element like this:
                 * <layer ts_cols="2" ts_rows="2" tileset="">
                 */
                writer.WriteStartElement("layer");
                writer.WriteAttributeString("tileset", layer.tsName);
                writer.WriteAttributeString("ts_rows", Convert.ToString(layer.tileSet.rows));
                writer.WriteAttributeString("ts_cols", Convert.ToString(layer.tileSet.columns));
                for (int i = 0; i < map.width; i++)
                {
                    //As before. This node has no attributes.
                    writer.WriteStartElement("row");
                    for (int j = 0; j < map.height; j++)
                    {
                        /* Create a tile element, it looks like this:
                         * <tile texture="1"/>
                         */
                        writer.WriteStartElement("tile");
                        /* This one is ugly.
                         * Tiles are ordered like this
                         * 0   1
                         * 2   3
                         */
                        int textureIndex = layer.tiles[i, j].texture.Y * layer.tileSet.rows + layer.tiles[i, j].texture.X; //Maaaagic math. 
                        writer.WriteAttributeString("texture", Convert.ToString(textureIndex));
                        writer.WriteEndElement(); //Close the tile tag.
                    }
                    writer.WriteEndElement(); //Close the row tag.
                }
                writer.WriteEndElement(); //Close the layer tag.
            }
            writer.WriteEndElement();  //Close the map tag.
            writer.Close(); //Close the XmlWriter.
            file.Close(); //Close the FileStram.
        }

        public static Map Deserialize(string filepath)
        {
            XmlDocument doc = new XmlDocument(); //Creates a new XmlDocument.
            doc.Load(filepath);
            XmlNode root = doc.DocumentElement; //This is our <map> tag.
            int width = Convert.ToInt32(root.Attributes["width"].InnerText); //Get the map width.
            int height = Convert.ToInt32(root.Attributes["height"].InnerText); //You know.
            var map = new Map(width, height); //Initialize the new Map. Yay!
            foreach (XmlElement layer in doc.GetElementsByTagName("layer")) //Foreach <layer> tag
            {
                //Get TileSet rows, columns.
                int ts_rows = Convert.ToInt32(layer.GetAttribute("ts_rows"));
                int ts_cols = Convert.ToInt32(layer.GetAttribute("ts_cols"));
                //Get the tileset name.
                string tileset = layer.GetAttribute("tileset");
                //Add a new Layer to the map.
                map.layers.Add(new MapLayer(tileset, ts_cols, ts_rows, width, height));
                for (int i = 0; i < layer.ChildNodes.Count; i++) //Assuming ChildNodes are only rows.
                {
                    XmlNode row = layer.ChildNodes[i];
                    for (int j = 0; j < row.ChildNodes.Count; j++) //Assuming ChildNodes are only tiles.
                    {
                        int texture_index = Convert.ToInt32(row.ChildNodes[j].Attributes["texture"].InnerText);
                        //Err....
                        int texX = texture_index % ts_cols;
                        int texY = texture_index / ts_rows;
                        map.layers[0].tiles[i, j].texture = new Point(texX, texY);
                    }
                }
            }            
            return map; //And this is our map.
        }
        #endregion
    }
}
