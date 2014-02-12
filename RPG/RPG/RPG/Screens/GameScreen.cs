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
        private static List<Bullet> enemyBullets = new List<Bullet>();

        private readonly Cursor cursor = new Cursor(new Vector2(0, 0));

        private Texture2D gameWindowTexture;
        private Vector2 gameWindowTexturePos;
        private Heroes soldier;
        private MeleUnits meleUnit;
        private RangedUnits rangedUnit;
        private KeyboardState keyboard;
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

        public static List<Bullet> EnemyBullets
        {
            get
            {
                return enemyBullets;
            }

            set
            {
                enemyBullets = value;
            }
        }

        public Heroes Soldier
        {
            get
            {
                return this.soldier;
            }
            set
            {
                this.soldier = value;
            }
        }

        public static Vector2 CharacterPosition { get; set; }


        public void Load(ContentManager content, Viewport viewport, GraphicsDeviceManager graphics)
        {
            this.gameWindowTexture = content.Load<Texture2D>(@"Textures\GameScreens\Level1");

            CharacterPosition = new Vector2(viewport.Width / 2, viewport.Height / 2);

            this.soldier = new Heroes(CharacterPosition, 2);

            this.meleUnit = new MeleUnits(new Vector2(500, 500), 1.3f);
            this.rangedUnit = new RangedUnits(new Vector2(480, 480), 0);

            this.soldier.LoadContent(content, "thor_top_view");
            this.cursor.LoadContent(content, "crosshair");
            this.meleUnit.LoadContent(content, "male");
            this.rangedUnit.LoadContent(content, "male");
            this.soldier.Ammo = 200;

            Texture2D bulletTexture = content.Load<Texture2D>(@"Textures\Objects\bullet");

            for (int i = 0; i < this.soldier.Ammo; i++)
            {
                Bullet o = new Bullet(new Vector2(0, 0), bulletTexture);
                PBullets.Add(o);
            }

            for (int i = 0; i < 100; i++)
            {
                Bullet o = new Bullet(new Vector2(0, 0), bulletTexture);
                EnemyBullets.Add(o);
            }
        }

        public void Draw(GraphicsDevice graphicDevice, Viewport viewport, SpriteBatch spriteBatch, ContentManager content)
        {
            this.gameWindowTexturePos = new Vector2(0, 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);
            
            spriteBatch.Draw(this.gameWindowTexture, this.gameWindowTexturePos, Color.White);

            this.soldier.Draw(spriteBatch, viewport, soldier.Rotation);
            this.meleUnit.Draw(spriteBatch, viewport, meleUnit.Rotation);
            this.rangedUnit.Draw(spriteBatch, viewport, rangedUnit.Rotation);

            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            Vector2 ammoPosition = new Vector2(10, 10);

            spriteBatch.DrawString(font, string.Format("Ammo :  {0}", this.soldier.Ammo), ammoPosition, Color.White);

            foreach (var bullet in bullets)
            {
                if (bullet.Alive)
                {
                    bullet.Draw(spriteBatch, viewport, bullet.Rotation);
                }
            }

            foreach (var bullet in enemyBullets)
            {
                if (bullet.Alive)
                {
                    bullet.Draw(spriteBatch, viewport, bullet.Rotation);
                }
            }
            spriteBatch.End();

            spriteBatch.Begin();
            this.cursor.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Update(Viewport viewport)
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            this.soldier.Update(viewport);

            this.cursor.Update();
            this.rangedUnit.FiringTimer++;
            this.soldier.FiringTimer++;

            if (Math.Abs(this.soldier.Position.X - this.meleUnit.Position.X) < 475 &&
               Math.Abs(this.soldier.Position.Y - this.meleUnit.Position.Y) < 340)
            {
                this.meleUnit.Update(viewport);
            }

            if (Math.Abs(this.soldier.Position.X - this.rangedUnit.Position.X) < 475 &&
               Math.Abs(this.soldier.Position.Y - this.rangedUnit.Position.Y) < 340)
            {
                this.rangedUnit.Update(viewport);
                this.rangedUnit.CheckShooting();
            }

            foreach (var bullet in bullets)
            {
                if (bullet.Alive)
                    bullet.Update();
            }

            foreach (var bullet in enemyBullets)
            {
                if (bullet.Alive)
                    bullet.Update();
            }

                if (this.keyboard.IsKeyDown(Keys.Tab))
                {
                    MainMenuScreen.PMainMenuItems[0].ItemText = "Resume game";
                    Rpg.ActiveWindowSet(EnumActiveWindow.MainMenu);
                }

                if (this.mouse.LeftButton == ButtonState.Pressed)
                {
                    this.soldier.CheckShooting();
                }

                previousMouse = mouse;
        }

    }
}