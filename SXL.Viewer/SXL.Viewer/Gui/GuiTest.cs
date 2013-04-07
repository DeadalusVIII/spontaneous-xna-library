using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SXL.Gui;
using SXL.TextureManipulator;

namespace SXL.Viewer.Gui
{
    class GuiTest : DrawableGameComponent
    {
        private GuiSystem guiSystem;
        private List<Texture2D> texturesToDraw = new List<Texture2D>(); 

        public GuiTest(Game game) 
            : base(game)
        {
            guiSystem = new GuiSystem(game);
        }

        public override void Initialize()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            TextureCanvas2D canvas2D = new TextureCanvas2D(Game.GraphicsDevice, 500, 500);
            canvas2D.DrawRoundedRectangle(new Rectangle(20,20,400,100),Color.Yellow, Color.White,5,30,30);
            texturesToDraw.Add(canvas2D.CloseAndProcessToTexture2D());

            TextureCanvas2D canvas2D2 = new TextureCanvas2D(Game.GraphicsDevice, 500, 500);
            canvas2D2.DrawRoundedRectangle(new Rectangle(5, 5, 200, 200), Color.Blue, Color.White, 5, 30, 30);
            texturesToDraw.Add(canvas2D2.CloseAndProcessToTexture2D());

            System.Diagnostics.Debug.WriteLine(watch.ElapsedMilliseconds);
            watch.Stop();
            //tex2);

            //Texture2D newTexture = new Texture2D(Game.GraphicsDevice,500,500);
            //newTexture = newTexture.DrawArc(Color.White, 2, new Rectangle(20, 20, 20, 20), 90, -90);
            //newTexture = newTexture.Do();//.DrawRoundedRectangle(Color.White, 4, new Rectangle(20, 20, 300, 300));
            
            //texturesToDraw.Add(newTexture);
            //SkinTest();
        }

        private void SkinTest()
        {
            Texture2D skinTestOuter = Game.Content.Load<Texture2D>("SkinTest");
            Texture2D skinTestInner = Game.Content.Load<Texture2D>("SkinTestInner");
            Texture2D skinTestBack = Game.Content.Load<Texture2D>("1");

            Texture2D textureInner = skinTestInner.ExtendRectangleQuad(200, 200);
            Texture2D texture2D = skinTestOuter.ExtendRectangleQuad(200, 200).Color(new Color(0, 0, 0, 150));

            texturesToDraw.Add(skinTestBack);
            texturesToDraw.Add(texture2D);
            texturesToDraw.Add(textureInner);
        }

        public override void Update(GameTime gameTime)
        {
            guiSystem.Update(gameTime);
            guiSystem.UpdateInteraction(gameTime);
            guiSystem.UpdateWindows(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //guiSystem.Draw(gameTime, spriteBatch);
            //guiSystem.DrawInteraction(gameTime, spriteBatch);

            foreach (var texture2D in texturesToDraw)
            {
                spriteBatch.Draw(texture2D, new Vector2(), Color.White);
            }
        }
    }
}
