using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace SXL.Language.Pipeline
{
    public class LanguageAsset
    {
        private readonly String name;
        private readonly TextureContent icon;
        private readonly Dictionary<String, String> strings = new Dictionary<string, string>(); 

        public LanguageAsset(XmlElement element, ContentProcessorContext context)
        {
            //load the name
            name = element.Attributes["name"].InnerText;

            icon = LoadTexture(context, element.Attributes["icon"].InnerText);

            XmlNodeList modesStrings = element["Strings"].GetElementsByTagName("String");
            foreach (XmlElement node in modesStrings)
                strings.Add(node.Attributes["key"].InnerText, node.Attributes["value"].InnerText.Replace("\\n","\n"));
        }

        public void Write(ContentWriter output)
        {
            output.Write(name);
            output.WriteObject(icon);
            output.Write(strings.Values.Count());
            foreach (KeyValuePair<string, string> keyValuePair in strings)
            {
                output.Write(keyValuePair.Key);
                output.Write(keyValuePair.Value);
            }

        }

        public TextureContent LoadTexture(ContentProcessorContext context, String path)
        {
            return context.BuildAndLoadAsset<TextureContent, TextureContent>(new ExternalReference<TextureContent>(path + ".png"), "TextureProcessor");
        }
    }
}
