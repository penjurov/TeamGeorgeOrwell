namespace Rpg.Screens
{
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
           
        private static Rectangle room;
        private readonly Cursor cursor = new Cursor(new Vector2(0, 0));
    
        private Texture2D gameWindowTexture;
        private Vector2 gameWindowTexturePos;
        private List<Units> units = new List<Units>();
        private Heroes soldier;
        private KeyboardState keyboard;
        private MouseState mouse;

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

        public static Vector2 CharacterPosition { get; set; }


        public void Load(ContentManager content, Viewport viewport, GraphicsDeviceManager graphics)
        {
            this.gameWindowTexture = content.Load<Texture2D>(@"Textures\GameScreens\Level1");

            room = new Rectangle(0, 0, gameWindowTexture.Width, gameWindowTexture.Height);
            
            CharacterPosition = new Vector2(room.Width / 2, room.Height / 2);
            
            switch (ChooseHeroScreen.HeroName)
            {
                case "ODIN":
                    {
                        this.soldier = new Heroes(CharacterPosition, 2, 1200, 110, 90);
                        this.soldier.LoadContent(content, "Odin");
                        break;
                    }
                case "THOR":
                    {
                        this.soldier = new Heroes(CharacterPosition, 2, 1500, 130, 100);
                        this.soldier.LoadContent(content, "Thor");
                        break;
                    }
                case "EIR":
                    {
                        this.soldier = new Heroes(CharacterPosition, 2, 1000, 90, 80);
                        this.soldier.LoadContent(content, "Odin");
                        break;
                    }
                default:
                    break;
            }


            units.Add(this.soldier);
            AddMeleUnit(content, 900, 700, "male");
            AddRangeUnit(content, 200, 670, "male");
          
            this.cursor.LoadContent(content, "crosshair");

            Texture2D bulletTexture = content.Load<Texture2D>(@"Textures\Objects\bullet");

            for (int i = 0; i < 100; i++)
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
            graphicDevice.Clear(Color.Black);
            spriteBatch.Draw(this.gameWindowTexture, this.gameWindowTexturePos, Color.White);

            foreach (var unit in units)
            {
                unit.Draw(spriteBatch, viewport, unit.Rotation);
            }


            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            Vector2 statsPosition = new Vector2(10, 10);
            spriteBatch.DrawString(font, string.Format("HP :  {0}", this.soldier.Health), statsPosition, Color.Red);

            statsPosition = new Vector2(10, 40);
            spriteBatch.DrawString(font, string.Format("Att :  {0}", this.soldier.Attack), statsPosition, Color.Red);

            statsPosition = new Vector2(10, 70);
            spriteBatch.DrawString(font, string.Format("Def :  {0}", this.soldier.Defence), statsPosition, Color.Red);

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
            this.cursor.DrawCursor(spriteBatch);
            spriteBatch.End();
        }

        public void Update()
        {
            this.keyboard = Keyboard.GetState();

            this.cursor.UpdateCursor();
            this.mouse = Mouse.GetState();

            foreach (var unit in units)
            {
                unit.Update();
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
        }

        private void AddMeleUnit(ContentManager content, int x, int y, string textureName)
        {
            MeleUnits meleUnit = new MeleUnits(new Vector2(x, y), 0.7f);
            meleUnit.LoadContent(content, textureName);
            units.Add(meleUnit);
        }

        private void AddRangeUnit(ContentManager content, int x, int y,  string textureName)
        {
            RangedUnits rangedUnit = new RangedUnits(new Vector2(x, y), 0);
            rangedUnit.LoadContent(content, textureName);
            units.Add(rangedUnit);
        }
    }
}