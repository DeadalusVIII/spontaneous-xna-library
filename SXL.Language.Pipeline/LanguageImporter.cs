using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SXL.Language.Pipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".lang", DisplayName = "Language Importer - SXL", DefaultProcessor = "LanguageProcessor")]
    public class LanguageImporter : ContentImporter<XmlElement>
    {
        public override XmlElement Import(string filename, ContentImporterContext context)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(System.IO.File.ReadAllText(filename));

            //return the root of the xml file
            return doc.DocumentElement;
        }
    }
}
