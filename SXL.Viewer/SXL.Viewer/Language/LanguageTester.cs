using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SXL.Language;

namespace SXL.Viewer.Language
{
    public class LanguageTester : GameComponent
    {
        LanguageManager _languageManager = new LanguageManager();


        public LanguageTester(Game game) : base(game)
        {
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected void LoadContent()
        {
            _languageManager.LoadAllLanguages(Game.Content, "Languages");
        }
    }
}
