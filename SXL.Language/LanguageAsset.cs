using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SXL.Language
{
    public class LanguageAsset
    {
        //aparently they have to be public
        public String Name { get; private set; }
        public Texture2D Icon { get; private set; }
        public Dictionary<String, String> Strings { get; private set; }

        internal LanguageAsset(ContentReader input)
        {
            Name = input.ReadString();
            Icon = input.ReadObject<Texture2D>();
            Strings = new Dictionary<string, string>();

            int numKeys = input.ReadInt32();
            for (int i = 0; i < numKeys; i++)
            {
                Strings.Add(input.ReadString(), input.ReadString());
            }
        }

        public LanguageAsset(string name, Dictionary<string, string> strings)
        {
            Name = name;
            Strings = strings;
        }
    }
}
