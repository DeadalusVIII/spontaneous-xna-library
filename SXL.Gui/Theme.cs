using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace SXL.Gui
{
    [Serializable]
    public class Theme
    {


        public String FontsPath = "SXL/Gui/Fonts/";
        public String InteractionPath = "SXL/Gui/Interactions/";
        public String MenusPath = "SXL/Gui/Menus/";
        public String GeometryPath = "SXL/Gui/Geometry/";

        public String DefaultControlSpriteFont = "Calibri20";

        public String MouseCursorPath = "SXL/Gui/Interactions/MouseArrow";

        public String RectangleInterior = "RectangleInterior";
        public String RectangleBoundary = "RectangleBoundary";

        public byte Transparency = 150;

        public Color BoundaryColor = Color.White;
        public Color InteriorColor = Color.Black;

        #region Loading and Saving

        public static Theme LoadFromFile(String fileName)
        {
            Theme theme;

            if (File.Exists(fileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(Theme));

                // Deserialization
                TextReader r = new StreamReader(fileName);
                theme = (Theme)s.Deserialize(r);
                r.Close();
            }
            else
            {
                theme = new Theme();
            }

            return theme;
        }

        public static Theme LoadFromString(String text)
        {
            Theme theme;

            if (File.Exists(text))
            {
                XmlSerializer s = new XmlSerializer(typeof(Theme));

                // Deserialization
                TextReader r = new StringReader(text);
                theme = (Theme)s.Deserialize(r);
                r.Close();
            }
            else
            {
                theme = new Theme();
            }

            return theme;
        }

        public static void SaveTheme(String fileName, Theme theme)
        {
            XmlSerializer s = new XmlSerializer(typeof(Theme));
            TextWriter w = new StreamWriter(fileName);
            s.Serialize(w, theme);
            w.Close();
        }

        #endregion
    }
}
