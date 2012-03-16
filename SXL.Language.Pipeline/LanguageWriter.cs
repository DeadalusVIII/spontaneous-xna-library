using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace SXL.Language.Pipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class LanguageWriter : ContentTypeWriter<LanguageAsset>
    {
        protected override void Write(ContentWriter output, LanguageAsset value)
        {
            value.Write(output);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            // class which will be used to load this data.
            return "SXL.Language.LanguageReader, SXL.Language";
        }
    }
}
