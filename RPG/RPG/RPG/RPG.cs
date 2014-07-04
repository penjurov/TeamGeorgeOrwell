namespace Rpg
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Screens;
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class Rpg : Game
    {
        private static EnumActiveWindow activeWindow;
        
        private readonly GraphicsDeviceManager graphics;
        private readonly SoundEffect mainTheme;
        private readonly SoundEffectInstance mainThemeInstance;
        private readonly Dictionary<EnumActiveWindow,IScreen> screenManager= new Dictionary<EnumActiveWindow,IScreen>();


        private SpriteBatch spriteBatch;        

        private bool loaded = false;

        public Rpg()
        {
            this.graphics = new GraphicsDeviceManager(this);

            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 715;

            this.graphics.IsFullScreen = false;

            this.Content.RootDirectory = "Content";

            this.mainTheme = this.Content.Load<SoundEffect>(@"Textures\Sounds\mainTheme");
            this.mainThemeInstance = this.mainTheme.CreateInstance();
            this.mainThemeInstance.IsLooped = false;
            this.mainThemeInstance.Volume = 0.2f;
        }

        public static EnumActiveWindow PActiveWindow
        {
            get
            {
                return activeWindow;
            }

            set
            {
                activeWindow = value;
            }
        }

        protected override void Initialize()
        {
            PActiveWindow = EnumActiveWindow.MainMenu;
            screenManager.Add(EnumActiveWindow.MainMenu, new MainMenuScreen());
            screenManager.Add(EnumActiveWindow.AboutWindow, new AboutScreen());
            screenManager.Add(EnumActiveWindow.ControlWindow, new ControlScreen());
            screenManager.Add(EnumActiveWindow.GameOver, new GameOver());
            screenManager.Add(EnumActiveWindow.GameWindow, new GameScreen());
            screenManager.Add(EnumActiveWindow.Win, new WinScreen());
            screenManager.Add(EnumActiveWindow.ChooseHeroWindow, new ChooseHeroScreen());

            base.Initialize();
           

            this.IsMouseVisible = false;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);


            foreach (var name in screenManager.Keys)
            {
                if (name != EnumActiveWindow.GameWindow)
                {
                    screenManager[name].LoadObjects(this.Content);
                }
            }
            
                       
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        { 

            if (PActiveWindow == EnumActiveWindow.GameWindow)
            {
                this.mainThemeInstance.Stop();
                if(!this.loaded)
                {
                    screenManager[PActiveWindow].LoadObjects(this.Content);
                    this.loaded=true;
                }
                screenManager[PActiveWindow].UpdateObjects(this.Content);
            }
            else
            {
                screenManager[PActiveWindow].UpdateObjects(this.Content);
                this.mainThemeInstance.Play();
            }          
        }

        protected override void Draw(GameTime gameTime)
        {            
            if (PActiveWindow != EnumActiveWindow.GameWindow || loaded)
            {
                screenManager[PActiveWindow].DrawObjects(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }
        }
    }
}