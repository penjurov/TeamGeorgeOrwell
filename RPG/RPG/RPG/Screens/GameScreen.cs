namespace Rpg.Screens
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Objects;

    internal class GameScreen
    {
        private static List<Bullet> bullets = new List<Bullet>();

        private static Rectangle screen;
        private static Rectangle room;
        private readonly Vector2 range = new Vector2(0, 0);
        private readonly Cursor cursor = new Cursor(new Vector2(0, 0));

        private Texture2D gameWindowTexture;
        private Vector2 gameWindowTexturePos;
        private Heroes soldier;
        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;
        private MouseState previousMouse;

        public static List<Bullet> PBullets
        {
            get
            {
                return bullets;
            }

            set
            {
                bullets = value;
            }
        }

        public static Rectangle PRoom
        {
            get
            {
                return room;
            }

            private set
            {
                room = value;
            }
        }

        public static Rectangle PScreen
        {
            get
            {
                return screen;
            }

            private set
            {
                screen = value;
            }
        }

        public void Load(ContentManager content, Viewport viewport, Camera camera, GraphicsDeviceManager graphics)
        {
            room = new Rectangle(0, 0, graphics.PreferredBackBufferWidth * 10, graphics.PreferredBackBufferHeight * 10);
            screen = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            this.gameWindowTexture = content.Load<Texture2D>(@"Textures\GameScreens\GameScreen");
            Vector2 characterPosition = new Vector2((this.gameWindowTexture.Width - viewport.Width) / 2, 0);

            this.soldier = new Heroes(characterPosition, 3);

            this.soldier.LoadContent(content, "male");
            this.cursor.LoadContent(content, "crosshair");
            this.soldier.Ammo = 32;

            Texture2D bulletTexture = content.Load<Texture2D>(@"Textures\Objects\bullet");

            for (int i = 0; i < this.soldier.Ammo; i++)
            {
                Bullet o = new Bullet(new Vector2(0, 0), bulletTexture);
                o.Alive = false;
                PBullets.Add(o);
            }

            camera.Position = this.soldier.Position;
        }

        public void Draw(GraphicsDevice graphicDevice, Viewport viewport, SpriteBatch spriteBatch, ContentManager content, Camera camera)
        {
            this.gameWindowTexturePos = new Vector2((viewport.Width - this.gameWindowTexture.Width) / 2, 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform(graphicDevice));
            graphicDevice.Clear(Color.Black);
            spriteBatch.Draw(this.gameWindowTexture, this.gameWindowTexturePos, Color.White);
            this.soldier.Draw(spriteBatch, viewport);

            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            Vector2 ammoPosition = new Vector2(10, 10);
            spriteBatch.DrawString(font, string.Format("Ammo :  {0}", this.soldier.Ammo), ammoPosition, Color.White);

            foreach (var bullet in bullets)
            {
                if (bullet.Alive)
                {
                    bullet.Draw(spriteBatch, viewport);
                }
            }

            spriteBatch.End();

            spriteBatch.Begin();
            this.cursor.Draw(spriteBatch, viewport);
            spriteBatch.End();
        }

        public void Update(Camera camera)
        {
            this.mouse = Mouse.GetState();
            this.previousMouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();
            this.soldier.Update();
            this.cursor.UpdateCursor();

            foreach (var bullet in bullets)
            {
                bullet.Update();
            }

            if (this.keyboard.IsKeyDown(Keys.Tab) && this.previousKeyboard.IsKeyUp(Keys.Tab))
            {
                MainMenuScreen.PMainMenuItems[0].ItemText = "Resume game";
                Rpg.ActiveWindowSet(EnumActiveWindow.MainMenu);
            }

            this.soldier.FiringTimer++;
            if (this.mouse.LeftButton == ButtonState.Pressed)
            {
                if (this.soldier.Ammo > 0)
                {
                    this.soldier.CheckShooting();
                }
            }

            this.previousKeyboard = this.keyboard;
            this.previousMouse = this.mouse;

            camera.Position = this.soldier.Position;
        }
    }
}