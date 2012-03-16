using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SXL.Gui.Interactions;
using SXL.Gui.Windows;


namespace SXL.Gui
{
    public class GuiSystem
    {
        /*public const String FontPath = "XsInterface/Fonts/";
        public const String InteractionPath = "XsInterface/Interactions/";
        public const String MenusPath = "XsInterface/Menus/";
        public const String GeometryPath = "XsInterface/Geometry/";*/

        //keep a reference to the game object, since it contains much data that is useful
        private readonly Game game;

        private readonly Interaction interaction;

        private Theme theme;

        private Rectangle gameWindowSize;

        private GuiWindowManager guiWindowManager;


        //internal static ContentManager ContentManager;
        //internal static GraphicsDeviceManager Graphics;
        //internal static GraphicsDevice GraphicsDevice;

        //Game Panels and Windows
        //private FormManager formManager;
        //private PanelManager panelManager;
        
        
        public GuiSystem(Game gameInstance)
        {
            game = gameInstance;
            
            //By default, it uses the Black color
            //Style = new Style(Color.Black);

            //initializes the default theme
            theme = new Theme();

            //initializes the interaction members
            interaction = new Interaction(this);

            //set the default game draw size as the window size
            gameWindowSize = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            guiWindowManager = new GuiWindowManager(this);

            //formManager = new FormManager();
            //panelManager = new PanelManager();
        }
        
        #region Scissor

        private static readonly RasterizerState ScissorState = new RasterizerState
        {
            ScissorTestEnable = true,
            CullMode = CullMode.None
        };

        public static void StartScissor(GraphicsDevice device, SpriteBatch batch, Rectangle scissorRectangle)
        {
            batch.End();
            device.ScissorRectangle = scissorRectangle;
            batch.Begin(SpriteSortMode.Deferred, null, null, null, ScissorState);
        }

        public static void EndScissor(GraphicsDevice device, SpriteBatch batch)
        {
            batch.End();
            batch.Begin();
        }

        #endregion

        public void AdjustPositionToScreen()
        {
            //panelManager.AjustPositionToScreen();
            //formManager.AjustPositionToScreen();
        }

        /*public void OpenWindow(FormWindow window)
        {
            //formManager.OpenWindow(window);
        }*/

        public void SetLanguageKeys(Dictionary<String,String> languageValues)
        {
            //TODO
        }
        
        #region Update

        public void UpdateInteraction(GameTime gameTime)
        {
            Interaction.Update(gameTime);
        }

        public void UpdateWindows(GameTime gameTime)
        {
            //formManager.Update(gameTime, Interaction);
        }

        public void UpdatePanels(GameTime gameTime)
        {
            //panelManager.Update(gameTime, Interaction);
        }

        #endregion

        #region Draw

        public void DrawPanels(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //panelManager.Draw(gameTime, spriteBatch, Style);
        }

        public void DrawWindows(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //formManager.Draw(gameTime,spriteBatch,Style);
        }

        public void DrawInteraction(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Interaction.Draw(gameTime,spriteBatch);
        }

        #endregion

        #region Properties

        
        public Interaction Interaction
        {
            get { return interaction; }
        }

        //public Style Style { get; set; }

        public Theme Theme
        {
            get { return theme; }
            set { theme = value; }
        }

        
        public Rectangle GameWindowSize
        {
            get { return gameWindowSize; }
            set { gameWindowSize = value;
                guiWindowManager.OnGameWindowSizeChange(gameWindowSize);
            }
        }

        public GuiWindowManager GuiWindowManager
        {
            get { return guiWindowManager; }
            set { guiWindowManager = value; }
        }

        internal GraphicsDevice GraphicsDevice
        {
            get { return game.GraphicsDevice; }
        }

        internal Game Game
        {
            get { return game; }
        }

        internal ContentManager ContentManager
        {
            get { return game.Content; }
        }



        #endregion

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawWindows(gameTime, spriteBatch);
            interaction.Draw(gameTime,spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            interaction.Update(gameTime);
            guiWindowManager.Update(gameTime);
        }
    }
}
