using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SXL.Language.Pipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "Language Processor - SXL")]
    public class LanguageProcessor : ContentProcessor<XmlElement, LanguageAsset>
    {
        public override LanguageAsset Process(XmlElement input, ContentProcessorContext context)
        {
            return new LanguageAsset(input,context);
        }
    }
}