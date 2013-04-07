using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace SXL.Language
{
    /// <summary>
    /// Manages language files 
    /// </summary>
    public class LanguageManager
    {
        private readonly Dictionary<String, LanguageAsset> languageAssets = new Dictionary<string, LanguageAsset>();
        private LanguageAsset activeLanguageAsset;
        
        /// <summary>
        /// Loads all the files (supposed to be Language Assets) in the indicated directory
        /// </summary>
        /// <param name="contentManager">Xna Content Manager</param>
        /// <param name="directory">Directory containing the language files, respective to the content folder</param>
        /// <returns></returns>
        public void LoadAllLanguages(ContentManager contentManager, String directory)
        {
            //to the language file folder
            DirectoryInfo info = new DirectoryInfo(contentManager.RootDirectory + "/" + directory);
            foreach (FileInfo file in info.EnumerateFiles())
            {
                LanguageAsset languageAsset = contentManager.Load<LanguageAsset>(directory + "/" + file.Name.Remove(file.Name.LastIndexOf(".")));
                languageAssets.Add(languageAsset.Name, languageAsset);
            }
        }


        /// <summary>
        /// Loads all the languages from 
        /// </summary>
        /// <param name="contentManager">Xna Content Manager</param>
        /// <param name="fileLocations">File locations, respective to the content folder</param>
        /// <returns></returns>
        public void LoadAllLanguages(ContentManager contentManager, String[] fileLocations)
        {
            foreach (string fileLocation in fileLocations)
            {
                LanguageAsset languageAsset = contentManager.Load<LanguageAsset>(fileLocation);
                languageAssets.Add(languageAsset.Name, languageAsset);
            }
        }

        public void LoadLanguage(ContentManager contentManager, string fileName)
        {
            LanguageAsset languageAsset = contentManager.Load<LanguageAsset>(fileName);
            languageAssets.Add(languageAsset.Name, languageAsset);
        }

        /// <summary>
        /// Sets the language asset to be used
        /// </summary>
        /// <param name="languageName">Language name</param>
        public void SetLanguage(String languageName)
        {
            activeLanguageAsset = languageAssets[languageName];
        }

        public String this[String key]
        {
            get
            {
                if (activeLanguageAsset == null)
                    throw new InvalidOperationException("Language has not been set yet.");

                return activeLanguageAsset.Strings[key];
            }
        }

        public String[] this[String[] keys]
        {
            get
            {
                if (activeLanguageAsset == null)
                    throw new InvalidOperationException("Language has not been set yet.");
                
                String[] values = new string[keys.Count()];
                for (int i = 0; i < keys.Count(); i++)
                {
                    values[i] = activeLanguageAsset.Strings[keys[i]];
                }

                return values;
            }
        }

        public List<LanguageAsset> LanguageAssets
        {
            get { return languageAssets.Values.ToList(); }
        }
    }
}
