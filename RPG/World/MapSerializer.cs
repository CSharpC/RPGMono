using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;

namespace RPG.World
{
    /// <summary>
    /// <para>Saves and loads map data.</para>
    /// </summary>
    public class MapSerializer
    {
        
        #region Static Functions
        public static string Serialize(Map map)
        {
            FileStream fs = File.Create(@"C:\\Users\\bombo\\Documents\\generated_map.xml");
            XmlWriter writer = XmlWriter.Create(fs);
            writer.WriteElementString("map", "");
            foreach (var layer in map.layers)
            {
                
            }
            writer.Close();
            fs.Close();
        }

        public static Map Deserialize(string mapData)
        {
            var lines = mapData.Replace("\t", "").Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].StartsWith("Map"))
                throw new ArgumentException("Not a valid map string!");
            var dim = lines[0].Split(' ');
            int width = int.Parse(dim[1]), height = int.Parse(dim[2]);

            var map = new Map(width, height);
            map.layers.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                if(lines[i].StartsWith("Layer"))
                {
                    var layerInfo = lines[i].Split(' ');
                    var layer = new MapLayer(layerInfo[1], int.Parse(layerInfo[2]), int.Parse(layerInfo[3]), width, height);
                    int curLine = i + 1, y = 0;
                    while(!lines[curLine].StartsWith("]"))
                    {
                        var blocks = lines[curLine].Split('|');
                        for(int x = 0; x < blocks.Length; x++)
                        {
                            var tex = blocks[x].Split(':');
                            layer.tiles[x, y].texture = new Microsoft.Xna.Framework.Point(int.Parse(tex[0]), int.Parse(tex[1]));
                        }
                        y++;
                        curLine++;
                    }
                    map.layers.Add(layer);
                }
            }
            //TODO: Change global tile size if needed.
            return map;
        }
        #endregion
    }
}
