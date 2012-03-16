using Microsoft.Xna.Framework.Content;
namespace SXL.Language
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// </summary>
    internal class LanguageReader : ContentTypeReader<LanguageAsset>
    {
        protected override LanguageAsset Read(ContentReader input, LanguageAsset existingInstance)
        {
            return new LanguageAsset(input);
        }
    }
}
