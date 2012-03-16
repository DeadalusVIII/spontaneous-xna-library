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
    public static class ColorExtender
    {
        public static bool AproxEqual(this Color color1, Color color2, byte rangeMin, byte rangeMax)
        {
            bool redInLimits = color1.R >= (color2.R - rangeMin) && color1.R <= (color2.R + rangeMax);
            bool blueInLimits = color1.B >= (color2.B - rangeMin) && color1.B <= (color2.B + rangeMax);
            bool greenInLimits = color1.G >= (color2.G - rangeMin) && color1.G <= (color2.G + rangeMax);
            bool alphaInLimits = color1.A >= (color2.A - rangeMin) && color1.A <= (color2.A + rangeMax);

            return redInLimits && blueInLimits && greenInLimits && alphaInLimits;
        }
    }

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

        private void PackTest()
        {
            Texture2D timerTexture = Content.Load<Texture2D>("Timer");
            Texture2D jonLeftRiseTexture = Content.Load<Texture2D>("DebLeftRise");
            Texture2D jonRightRiseTexture = Content.Load<Texture2D>("DebRightRise");
            Texture2D tomLeftRiseTexture = Content.Load<Texture2D>("MegLeftRise");
            Texture2D tomRightRiseTexture = Content.Load<Texture2D>("MegRightRise");

            timerTexture.PackSpriteSheet(37, 2048).SaveToFile(@"D:\Downloads\Clock.png", Texture2DFile.Png);
            jonLeftRiseTexture.PackSpriteSheet(12, 2048).SaveToFile(@"D:\Downloads\DebLeftRise.png", Texture2DFile.Png);
            jonRightRiseTexture.PackSpriteSheet(12, 2048).SaveToFile(@"D:\Downloads\DebRightRise.png", Texture2DFile.Png);
            tomLeftRiseTexture.PackSpriteSheet(12, 2048).SaveToFile(@"D:\Downloads\MegLeftRise.png", Texture2DFile.Png);
            tomRightRiseTexture.PackSpriteSheet(12, 2048).SaveToFile(@"D:\Downloads\MegRightRise.png", Texture2DFile.Png);

            texturesToDraw.Add(timerTexture);
            texturesToDraw.Add(jonLeftRiseTexture);
            texturesToDraw.Add(jonRightRiseTexture);
            texturesToDraw.Add(tomLeftRiseTexture);
            texturesToDraw.Add(tomRightRiseTexture);
        }

        private void PackMass()
        {
            String[] femaleParts = new string[] { "Body", "Eyes", "Hair", "LeftArm", "LeftSleeve", "Legs", "RightArm", "RightSleeve", "Square", "Shirt", "Shoes", "Silhouette", "Skirt" };
            String[] maleParts = new string[] { "Body", "Eyes", "Hair", "LeftArm", "LeftSleeve", "Legs", "RightArm", "RightSleeve", "Square", "Shirt", "Shoes", "Silhouette", "Shorts" };

            foreach (var part in femaleParts)
            {
                Content.Load<Texture2D>("Packing/PlayerFemale/Rise/Left/PlayerFemaleLeftRise" + part).PackSpriteSheet(12, 2048).SaveToFile(@"C:\Users\Pedro Silva\Desktop\Packed\PlayerFemale\Rise\Left\PlayerFemaleLeftRise" + part + ".png", Texture2DFile.Png);
                Content.Load<Texture2D>("Packing/PlayerFemale/Rise/Right/PlayerFemaleRightRise" + part).PackSpriteSheet(12, 2048).SaveToFile(@"C:\Users\Pedro Silva\Desktop\Packed\PlayerFemale\Rise\Right\PlayerFemaleRightRise" + part + ".png", Texture2DFile.Png);
            }

            foreach (var malePart in maleParts)
            {
                Content.Load<Texture2D>("Packing/PlayerMale/Rise/Left/PlayerMaleLeftRise" + malePart).PackSpriteSheet(12, 2048).SaveToFile(@"C:\Users\Pedro Silva\Desktop\Packed\PlayerMale\Rise\Left\PlayerMaleLeftRise" + malePart + ".png", Texture2DFile.Png);
                Content.Load<Texture2D>("Packing/PlayerMale/Rise/Right/PlayerMaleRightRise" + malePart).PackSpriteSheet(12, 2048).SaveToFile(@"C:\Users\Pedro Silva\Desktop\Packed\PlayerMale\Rise\Right\PlayerMaleRightRise" + malePart + ".png", Texture2DFile.Png);
            }
        }

        private void SkinTest()
        {
            Texture2D skinTestOuter = Content.Load<Texture2D>("SkinTest");
            Texture2D skinTestInner = Content.Load<Texture2D>("SkinTestInner");
            Texture2D skinTestBack = Content.Load<Texture2D>("1");

            Texture2D textureInner = skinTestInner.ExtendRectangleQuad(200, 200);
            Texture2D texture2D = skinTestOuter.ExtendRectangleQuad(200, 200).Color(new Color(0, 0, 0, 150));
            
            texturesToDraw.Add(skinTestBack);
            texturesToDraw.Add(texture2D);
            texturesToDraw.Add(textureInner);
        }

        private void JoinTest()
        {
            String[] variants = new[] { "Silhouette", "Body", "Face", "Eyes", "Hair", "Legs", "Shirt", "Shoes", "Skirt" };
            
            List<Texture2D> joinTestPat = new List<Texture2D>();
            Texture2D finalTexture = null;

            foreach (string variant in variants)
            {
                Texture2D loadedTexture = Content.Load<Texture2D>("Joining/PlayerFemaleDownMove" + variant);

                if (variant == "Hair")
                    loadedTexture = loadedTexture.Color(new Color(227, 30, 36));
                else if (variant == "Face" || variant == "Body" || variant == "Legs")
                    loadedTexture = loadedTexture.Color(new Color(244, 217, 177));
                else if (variant == "Skirt")
                {
                    loadedTexture = loadedTexture.Color(new Color(239, 127, 26));
                    //loadedTexture = loadedTexture.Color(new Color(150, 50, 150));
                }
                else if (variant == "Shirt")
                    loadedTexture = loadedTexture.Color(new Color(153, 51, 51));

                if(finalTexture == null)
                    finalTexture = loadedTexture;
                else
                {
                    finalTexture = finalTexture.Join(loadedTexture);
                }

                //joinTestPat.Add(loadedTexture);
            }

            finalTexture.SaveToFile(@"C:\Users\Pedro Silva\Downloads\PlayerFemaleDownMoveTest", Texture2DFile.Png);

            texturesToDraw.Add(finalTexture);
        }


        private class ColorCounter
        {
            private readonly String name;

            private readonly Color topLeftColor;
            private readonly Color topRightColor;
            private readonly Color bottomLeftColor;

            private readonly List<Point> topLeftLocations;
            private readonly List<Point> topRightLocations;
            private readonly List<Point> bottomLeftLocations;

            public ColorCounter(String name, Color topLeftColor, Color topRightColor, Color bottomLeftColor)
            {
                this.name = name;

                this.topLeftColor = topLeftColor;
                this.topRightColor = topRightColor;
                this.bottomLeftColor = bottomLeftColor;

                topLeftLocations = new List<Point>();
                topRightLocations = new List<Point>();
                bottomLeftLocations = new List<Point>();
            }

            public void Check(Color color, int x, int y)
            {
                if(color.AproxEqual(topLeftColor,10,30))
                    topLeftLocations.Add(new Point(x, y));
                else if (color.AproxEqual(topRightColor,10,30))
                    topRightLocations.Add(new Point(x, y));
                else if (color.AproxEqual(bottomLeftColor,10,30))
                    bottomLeftLocations.Add(new Point(x, y));
            }

            public Point TopLeftMeanLocation
            {
                get
                {
                    Point meanTopLeftLocation = new Point(0, 0);

                    foreach (Point topLeftLocation in topLeftLocations)
                    {
                        meanTopLeftLocation = new Point(meanTopLeftLocation.X + topLeftLocation.X, meanTopLeftLocation.Y + topLeftLocation.Y);
                    }

                    return new Point(meanTopLeftLocation.X / topLeftLocations.Count, meanTopLeftLocation.Y / topLeftLocations.Count);
                }
            }

            public Point TopRightMeanLocation
            {
                get
                {
                    Point meanTopRightLocation = new Point(0, 0);

                    foreach (Point topRightLocation in topRightLocations)
                    {
                        meanTopRightLocation = new Point(meanTopRightLocation.X + topRightLocation.X, meanTopRightLocation.Y + topRightLocation.Y);
                    }

                    return new Point(meanTopRightLocation.X / topRightLocations.Count, meanTopRightLocation.Y / topRightLocations.Count);
                }
            }

            public Point BottomLeftMeanLocation
            {
                get
                {
                    Point meanBottomLeftLocation = new Point(0, 0);

                    foreach (Point bottomLeftLocation in bottomLeftLocations)
                    {
                        meanBottomLeftLocation = new Point(meanBottomLeftLocation.X + bottomLeftLocation.X, meanBottomLeftLocation.Y + bottomLeftLocation.Y);
                    }

                    return new Point(meanBottomLeftLocation.X / bottomLeftLocations.Count, meanBottomLeftLocation.Y / bottomLeftLocations.Count);
                }
            }

            public string Name
            {
                get { return name; }
            }

            public bool Detected
            {
                get { return topLeftLocations.Count > 0 && topRightLocations.Count > 0 && bottomLeftLocations.Count > 0; }
            }

            /*public float Height
            {
                get{return Math.Min()}
            }

            public float Width
            {
                //get { return Math.Max(TopRightMeanLocation.X - TopLeftMeanLocation.X); }
            }*/
        }

        


        private List<ColorCounter> DetectPointedSquares(Texture2D texture2D, int instanceCount,
            Color frontSquareTopLeftColor, Color frontSquareTopRightColor, Color frontSquareBottomLeftColor,
            Color backSquareTopLeftColor, Color backSquareTopRightColor, Color backSquareBottomLeftColor)
        {
            EditableTexture2D editableTexture2D = new EditableTexture2D(texture2D);
            
            int instanceWidth = editableTexture2D.Width / instanceCount;
            List<ColorCounter> colorCounters = new List<ColorCounter>();

            for (int k = 0; k < instanceCount; k++)
            {
                ColorCounter frontSquare = new ColorCounter("Front", frontSquareTopLeftColor, frontSquareTopRightColor, frontSquareBottomLeftColor);
                ColorCounter backSquare = new ColorCounter("Back", backSquareTopLeftColor, backSquareTopRightColor, backSquareBottomLeftColor);
                
                for (int i = k * instanceWidth; i < (k + 1) * instanceWidth; i++)
                {
                    for (int j = 0; j < editableTexture2D.Height; j++)
                    {
                        if (editableTexture2D[i, j] != new Color(0, 0, 0,0))
                            Console.WriteLine("@ " + i + "," + j + ":" + editableTexture2D[i, j]);

                        frontSquare.Check(editableTexture2D[i, j], i, j);
                        backSquare.Check(editableTexture2D[i, j], i, j);
                    }
                }

                if (frontSquare.Detected)
                    colorCounters.Add(frontSquare);

                if (backSquare.Detected)
                    colorCounters.Add(backSquare);
            }

            return colorCounters;
        }


        private class LayerSquare
        {
            public Point Red { get;set; }
            public Point Blue { get;set; }
            public Point Yellow { get;set; }

            public LayerSquare(Point red, Point blue, Point yellow)
            {
                Red = red;
                Blue = blue;
                Yellow = yellow;
            }
        }

        private void LayerDetectionTest()
        {
            Texture2D layerTest = Content.Load<Texture2D>("PlayerMaleLeftCleanLocation");

            EditableTexture2D editableTexture2D = new EditableTexture2D(layerTest);


            int instanceWidth = editableTexture2D.Width/12;
            List<LayerSquare> layerSquares = new List<LayerSquare>();

            for (int k = 0; k < 12; k++ )
            {
                List<Point> redLocations = new List<Point>();
                List<Point> blueLocations = new List<Point>();
                List<Point> yellowLocations = new List<Point>();

                for (int i = k * instanceWidth; i < (k+1) * instanceWidth; i++)
                {
                    for (int j = 0; j < editableTexture2D.Height; j++)
                    {
                        if(editableTexture2D[i,j] == Color.Red)
                            redLocations.Add(new Point(i,j));
                        if(editableTexture2D[i,j] == new Color(0,38,255))
                            blueLocations.Add(new Point(i,j));
                        if (editableTexture2D[i, j] == new Color(255, 216, 0))
                            yellowLocations.Add(new Point(i,j));
                    }
                }

                Point totalRedLocation = new Point(0,0);
                Point totalBlueLocation = new Point(0, 0);
                Point totalYellowLocation = new Point(0, 0);

                for (int i = 0; i < redLocations.Count; i++)
                {
                    if (i == 0)
                        totalRedLocation = redLocations[0];
                    else
                        totalRedLocation = new Point(totalRedLocation.X + redLocations[0].X, totalRedLocation.Y + redLocations[0].Y);
                }

                totalRedLocation = new Point(totalRedLocation.X / redLocations.Count, totalRedLocation.Y / redLocations.Count);

                for (int i = 0; i < blueLocations.Count; i++)
                {
                    if (i == 0)
                        totalBlueLocation = blueLocations[0];
                    else
                        totalBlueLocation = new Point(totalBlueLocation.X + blueLocations[0].X, totalBlueLocation.Y + blueLocations[0].Y);
                }

                totalBlueLocation = new Point(totalBlueLocation.X / blueLocations.Count, totalBlueLocation.Y / blueLocations.Count);


                for (int i = 0; i < yellowLocations.Count; i++)
                {
                    if (i == 0)
                        totalYellowLocation = yellowLocations[0];
                    else
                        totalYellowLocation = new Point(totalYellowLocation.X + yellowLocations[0].X, totalYellowLocation.Y + yellowLocations[0].Y);
                }

                totalYellowLocation = new Point(totalYellowLocation.X / yellowLocations.Count, totalYellowLocation.Y / yellowLocations.Count);


                layerSquares.Add(new LayerSquare(totalRedLocation, totalBlueLocation, totalYellowLocation));
            }

                
        }

        void ColorPerformanceTest()
        {
            Texture2D layerTest = Content.Load<Texture2D>("PlayerMaleLeftCleanLocation");

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10; i++ )
                layerTest = layerTest.Color(Color.Green);

            Debug.WriteLine("Time Spritebatch is: " + (watch.ElapsedMilliseconds / 10));

            watch.Restart();
            /*
            for (int i = 0; i < 10; i++)
                layerTest = layerTest.Color2(Color.Green);

            Debug.WriteLine("Time Normal is: " + (watch.ElapsedMilliseconds / 10));*/
        }

        void ColorPerformanceTest2()
        {
            Texture2D layerTest = Content.Load<Texture2D>("PlayerMaleLeftCleanLocation");

            Stopwatch watch1 = new Stopwatch();
            watch1.Start();

            layerTest.Color2(Color.Green);

            watch1.Stop();

            Console.WriteLine("Color Time: " + watch1.ElapsedMilliseconds);
        }


        private void HsvTest()
        {
            EditableTexture2D editableTexture2D = new EditableTexture2D(graphics.GraphicsDevice,200,200);
            
            for(int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    editableTexture2D[i, (199 - j)] = new HSV(28, i / 200f, j / 200f).ToColor();
                }
            }
            
            editableTexture2D.ToTexture2D().SaveToFile(@"C:\Users\Pedro Silva\Desktop\SkinColorPicker.png",Texture2DFile.Png);

            texturesToDraw.Add(editableTexture2D.ToTexture2D());
        }

        private void SkewTest()
        {
            Texture2D layerTest = Content.Load<Texture2D>("Potion - Red Bull");

            Stopwatch watch1 = new Stopwatch();
            watch1.Start();


            layerTest = layerTest.Skew(new Point(109, 225), new Point(183, 225), new Point(110, 298));

            watch1.Stop();
            
            Console.WriteLine("Skew Time: " + watch1.ElapsedMilliseconds);

            texturesToDraw.Add(layerTest);
        }

        private void JoinAndSkewTest()
        {
            String gender = "PlayerFemale";
            String direction = "Left";
            Texture2D frontLogo = Content.Load<Texture2D>("LogoAdidas");
            Texture2D backLogo = Content.Load<Texture2D>("LogoAdidas");

            //String[] variants = new[] { "Silhouette", "Body", "Face", "Eyes", "Hair", "Legs", "Shirt", "Square", "Shoes", "Shorts" };
            //String[] variants = new[] { "Silhouette", "Body", "Eyes", "Legs", "Shoes", "Shorts", "Shirt", "Square", "Arm", "Sleeve", "Hair", };

            //String[] variants = new[] { "Silhouette", "Body", "Face", "Eyes", "Hair", "Legs", "Shirt", "Square", "Shoes", "Skirt" };
            String[] variants = new[] { "Silhouette", "Body", "Eyes", "Legs", "Shoes", "Skirt", "Shirt", "Square", "Arm", "Sleeve", "Hair", };

            List<Texture2D> joinTestPat = new List<Texture2D>();
            Texture2D finalTexture = null;

            foreach (string variant in variants)
            {
                Texture2D loadedTexture = Content.Load<Texture2D>("Characters/" + gender + "/Static/" + direction + "/" + gender + direction + variant);

                if (variant == "Hair")
                    loadedTexture = loadedTexture.Color(new Color(227, 30, 36));
                else if (variant == "Face" || variant == "Body" || variant == "Legs" || variant == "Arm")
                    loadedTexture = loadedTexture.Color(new Color(244, 217, 177));
                else if (variant == "Square")
                {
                    List<ColorCounter> detectPointedSquares = DetectPointedSquares(loadedTexture, 1, new Color(255, 0, 0), new Color(0, 255, 0),
                                                                     new Color(0, 0, 255),
                                                                     new Color(127, 0, 0), new Color(255, 255, 0), new Color(0, 255, 255));

                    loadedTexture = new Texture2D(loadedTexture.GraphicsDevice, loadedTexture.Width, loadedTexture.Height);

                    Texture2D shirtTex = Content.Load<Texture2D>("Characters/" + gender + "/Static/" + direction + "/" + gender + direction + "Shirt");

                    foreach (ColorCounter detectPointedSquare in detectPointedSquares)
                    {
                        if(detectPointedSquare.Name == "Front")
                        {
                            Texture2D tex = frontLogo;//.Resize(new Point(frontLogo.Width / 3, frontLogo.Height / 3));

                            tex = tex.Skew(detectPointedSquare.TopLeftMeanLocation, detectPointedSquare.TopRightMeanLocation, detectPointedSquare.BottomLeftMeanLocation);
                            tex = tex.IntersectOpaque(shirtTex);

                            loadedTexture = loadedTexture.Join(tex);
                        }
                        else if (detectPointedSquare.Name == "Back")
                        {
                            Texture2D tex = backLogo.Skew(detectPointedSquare.TopLeftMeanLocation,detectPointedSquare.TopRightMeanLocation,detectPointedSquare.BottomLeftMeanLocation);
                            tex = tex.IntersectOpaque(shirtTex);
                            loadedTexture = loadedTexture.Join(tex);
                        }
                    }

                }
                else if (variant == "Skirt" || variant == "Shorts")
                {
                    loadedTexture = loadedTexture.Color(new Color(239, 127, 26));
                    //loadedTexture = loadedTexture.Color(new Color(150, 50, 150));
                }
                else if (variant == "Shirt" || variant == "Sleeve")
                    loadedTexture = loadedTexture.Color(new Color(153, 51, 51));

                if (finalTexture == null)
                    finalTexture = loadedTexture;
                else
                {
                    finalTexture = finalTexture.Join(loadedTexture);
                }

                //joinTestPat.Add(loadedTexture);
            }

            texturesToDraw.Add(finalTexture);
            //joinTestPat.Add(loadedTexture);
        }

        private void HeartMaking()
        {
            Texture2D heartTexture = Content.Load<Texture2D>("HeartMaking/PlayerFemaleMegTagHeart");
            SpriteFont spriteFont = Content.Load<SpriteFont>("HeartMaking/VoodooSpirits");

            //starts a new spritebatch and prepares a new rendertarget where it will be used in
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            RenderTarget2D renderTarget2D = new RenderTarget2D(GraphicsDevice, heartTexture.Width, heartTexture.Height);
            GraphicsDevice.SetRenderTarget(renderTarget2D);

            String s = "Meg";
            Vector2 size = spriteFont.MeasureString(s);
            Console.WriteLine(size);
            size = new Vector2((float)(size.X * Math.Cos(MathHelper.ToRadians(-30))) / 2f, (float)(size.X * Math.Sin(MathHelper.ToRadians(-30))) / 2f);

            //sets the background transparent
            GraphicsDevice.Clear(Color.Transparent);
            
            //draws the texture, with rotation, to the rendertarget 
            spriteBatch.Begin();
            spriteBatch.Draw(heartTexture,Vector2.Zero,Color.White);
            spriteBatch.DrawString(spriteFont, s, new Vector2(15, 18) - size, Color.Black, MathHelper.ToRadians(-30), Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.End();

            //resets the graphics device to render normally
            GraphicsDevice.SetRenderTarget(null);

            texturesToDraw.Add(renderTarget2D);
        }

        private void BigHeartMaking()
        {
            Texture2D tex1 = Content.Load<Texture2D>("BigHeart/HeartLargestBelow");
            Texture2D tex2 = Content.Load<Texture2D>("BigHeart/HeartLargestBetween");
            Texture2D tex3 = Content.Load<Texture2D>("BigHeart/HeartLargestAbove");

            Color color = Color.Gray;//new Color(0, 147, 221);

            Texture2D tex = tex1.Color(color);
            tex = tex.Join(tex2);
            tex = tex.Join(tex3.Color(color));

            texturesToDraw.Add(tex);

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
