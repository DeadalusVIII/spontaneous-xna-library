using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SXL.TextureManipulator.Viewer
{


     /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<Texture2D> texturesToDraw = new List<Texture2D>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

         /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //JoinTest();
            //ColorPerformanceTest();
            //PackMass();
            //HsvTest();
            //SkinTest();


            //ColorPerformanceTest2();
            JoinAndSkewTest();
            //SkewTest();
            //HeartMaking();
             //BigHeartMaking();


             //Texture2D[] texture2Ds = texture2D.DivideX(37);
             /*for(int i = 0; i < texture2Ds.Length; i++)
            {
                texture2Ds[i].SaveToFile(@"D:\Downloads\Test\TestImageTexture2D - " + i + ".png", Texture2DFile.Png);
            }*/

             //pack the 
             //texture2Ds.PackX(texture2Ds[0].Bounds, 2048).SaveToFile(@"D:\Downloads\TestPacked.png", Texture2DFile.Png);



             //texture2DCupcake = Content.Load<Texture2D>("CupcakeItem");
             //texture2DHeart = Content.Load<Texture2D>("JonHeartLargest");
             //TextureBuilder textureBuilder = new TextureBuilder(texture2D);
             //texture2D = new Texture2D(GraphicsDevice,500,500);
             //texture2D = texture2D.Color(Color.Yellow);

             //texture2D = texture2DCupcake.Skew(new Point(120, 230), new Point(200, 270), new Point(120, 280));
             //texture2D = texture2DCupcake.Skew(new Point(2, 2), new Point(20, 2), new Point(2, 20));


             //texture2D = texture2D.Join(texture2DCupcake);
             //for (int i = 0; i < 10; i++ )
             //texture2D = texture2D.DrawRectangle(Color.White, 10, new Rectangle(20, 20, 300, 200), 50);

             /*DateTime timeBegin = DateTime.Now;
            for (int i = 0; i < 10; i++)
                texture2D = texture2D.Transform();
            DateTime timeEnd = DateTime.Now;
            Console.WriteLine((timeEnd - timeBegin).TotalMilliseconds);*/


             //texture2D = texture2D.Translate(new Vector2(100,50));

             //texture2D = texture2D.Rotate(-90);
             //texture2D = texture2D.Resize(new Vector2(0.4f, 0.4f));
             //texture2D = texture2D.Transform();
             //texture2D.SaveAsPng(new FileStream(@"D:\Downloads\testImageTexture2D.png", FileMode.Create, FileAccess.Write), texture2D.Width, texture2D.Height);

             //textureBuilder.Rotate(2,0.5f);
        }

         


         /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.NonPremultiplied);
            foreach (Texture2D texture2D in texturesToDraw)
            {
                spriteBatch.Draw(texture2D,Vector2.Zero,Color.White);
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    
    }
}
