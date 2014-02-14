namespace Rpg.Screens
{
    using System;
    using System.Collections.Generic;
    using Interfaces;    
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;    
    using Objects;

    internal class GameScreen
    {
        private readonly Cursor cursor = new Cursor(new Vector2(0, 0));

        private IList<Bullet> bullets = new List<Bullet>();
        private IList<Bullet> enemyBullets = new List<Bullet>();
        private bool loaded = false;          
        private Rectangle room;
          
        private Texture2D gameWindowTexture;
        private Vector2 gameWindowTexturePos;
        private List<Units> units = new List<Units>();
        private Heroes hero;
        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;
        private MouseState previousMouse;

        private SoundEffect walk;
        private SoundEffectInstance walkInstance;
        private SoundEffect walk2;
        private SoundEffectInstance walkInstance2;
        private SoundEffect gunShot;
        private SoundEffectInstance gunShotInstance;

        public IList<Bullet> PBullets
        {
            get
            {
                return this.bullets;
            }

            set
            {
                this.bullets = value;
            }
        }

        public IList<Bullet> EnemyBullets
        {
            get
            {
                return this.enemyBullets;
            }

            set
            {
                this.enemyBullets = value;
            }
        }

        public Rectangle PRoom
        {
            get
            {
                return this.room;
            }

            private set
            {
                this.room = value;
            }
        }

        public Vector2 CharacterPosition { get; set; }

        public void Load(ContentManager content)
        {
            this.LoadMusic(content);
            this.LoadLevel(content);
            this.LoadHero(content);
            this.LoadUnits(content);
            this.LoadCursor(content);
            this.LoadBullets(content);      
        }
          
        public void Draw(SpriteBatch spriteBatch, ContentManager content)
        {           
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);
                this.DrawBackGround(spriteBatch);
                this.DrawUnits(spriteBatch);
                this.DrawLabels(spriteBatch, content);
                this.DrawBullets(spriteBatch);
                this.DrawCursor(spriteBatch);
            spriteBatch.End();
        }

        public void Update()
        {
            this.keyboard = Keyboard.GetState();
            this.mouse = Mouse.GetState();

            this.UpdateCursor();
            this.UpdateUnits();
            this.UpdateBullets();
            this.UpdateHero();

            // Check for Tab
            if (this.keyboard.IsKeyDown(Keys.Tab))
            {
                MainMenuScreen.PMainMenuItems[0].ItemText = "Resume game";
                Rpg.ActiveWindowSet(EnumActiveWindow.MainMenu);
            }

            this.previousKeyboard = this.keyboard;
            this.previousMouse = this.mouse; 
        }

        private void LoadBullets(ContentManager content)
        {
            Texture2D bulletTexture = content.Load<Texture2D>(@"Textures\Objects\bullet");

            for (int i = 0; i < 100; i++)
            {
                Bullet o = new Bullet(new Vector2(0, 0), bulletTexture);
                this.PBullets.Add(o);
            }

            for (int i = 0; i < 100; i++)
            {
                Bullet o = new Bullet(new Vector2(0, 0), bulletTexture);
                this.EnemyBullets.Add(o);
            }
        }

        private void LoadCursor(ContentManager content)
        {
            this.cursor.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", "crosshair"));
        }

        private void LoadUnits(ContentManager content)
        {
            this.AddMeleUnit(content, 900, 700, "male");
            this.AddRangeUnit(content, 200, 670, "male");
        }

        private void LoadHero(ContentManager content)
        {
            this.CharacterPosition = new Vector2(this.room.Width / 2, this.room.Height / 2);

            switch (ChooseHeroScreen.HeroName)
            {
                case "ODIN":
                    {
                        this.hero = new Heroes(this.CharacterPosition, 2, 1200, 110, 90);
                        break;
                    }

                case "THOR":
                    {
                        this.hero = new Heroes(this.CharacterPosition, 2, 1500, 130, 100);
                        break;
                    }

                case "EIR":
                    {
                        this.hero = new Heroes(this.CharacterPosition, 2, 1000, 90, 80);
                        break;
                    }

                default:
                    break;
            }

            this.hero.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", ChooseHeroScreen.HeroName));
            this.units.Add(this.hero);
        }

        private void LoadLevel(ContentManager content)
        {
            this.gameWindowTexture = content.Load<Texture2D>(@"Textures\GameScreens\Level1");

            this.room = new Rectangle(0, 0, this.gameWindowTexture.Width, this.gameWindowTexture.Height);
        }

        private void LoadMusic(ContentManager content)
        {
            this.walk = content.Load<SoundEffect>(@"Textures\Sounds\pl_dirt1");
            this.walk2 = content.Load<SoundEffect>(@"Textures\Sounds\pl_dirt2");
            this.gunShot = content.Load<SoundEffect>(@"Textures\Sounds\gunShot");
            this.walkInstance = this.walk.CreateInstance();
            this.walkInstance.IsLooped = false;
            this.walkInstance.Volume = 0.1f;
            this.walkInstance2 = this.walk2.CreateInstance();
            this.walkInstance2.IsLooped = false;
            this.walkInstance2.Volume = 0.1f;
            this.gunShotInstance = this.gunShot.CreateInstance();
            this.gunShotInstance.IsLooped = false;
            this.gunShotInstance.Volume = 0.1f;
        }

        private void DrawCursor(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.cursor.SpriteIndex, this.cursor.Position, Color.White);
        }

        private void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (var bullet in this.bullets)
            {
                if (bullet.Alive)
                {
                    this.ObjectDraw(spriteBatch, bullet.SpriteIndex, bullet.Position, bullet.Rotation);
                }
            }

            foreach (var bullet in this.enemyBullets)
            {
                if (bullet.Alive)
                {
                    this.ObjectDraw(spriteBatch, bullet.SpriteIndex, bullet.Position, bullet.Rotation);
                }
            }
        }

        private void DrawLabels(SpriteBatch spriteBatch, ContentManager content)
        {
            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            Vector2 statsPosition = new Vector2(10, 10);
            spriteBatch.DrawString(font, string.Format("HP :  {0}", this.hero.Health), statsPosition, Color.Red);

            statsPosition = new Vector2(10, 40);
            spriteBatch.DrawString(font, string.Format("Att :  {0}", this.hero.Attack), statsPosition, Color.Red);

            statsPosition = new Vector2(10, 70);
            spriteBatch.DrawString(font, string.Format("Def :  {0}", this.hero.Defence), statsPosition, Color.Red);
        }

        private void DrawUnits(SpriteBatch spriteBatch)
        {
            foreach (var unit in this.units)
            {
                this.ObjectDraw(spriteBatch, unit.SpriteIndex, unit.Position, unit.Rotation);
            }
        }

        private void DrawBackGround(SpriteBatch spriteBatch)
        {
            this.gameWindowTexturePos = new Vector2(0, 0);
            spriteBatch.Draw(this.gameWindowTexture, this.gameWindowTexturePos, Color.White);
        }

        private void UpdateHero()
        {
            Vector2 oldPos = this.hero.Position;

            if (this.keyboard.IsKeyDown(Keys.W))
            {
                if (oldPos.Y > this.PRoom.Y + 20)
                {
                    this.hero.Position = new Vector2(oldPos.X, oldPos.Y - this.hero.Speed);
                    this.CharacterPosition = this.hero.Position;

                    this.walkInstance.Play();
                    this.walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.A))
            {
                if (oldPos.X > this.PRoom.X + 20)
                {
                    this.hero.Position = new Vector2(oldPos.X - this.hero.Speed, oldPos.Y);
                    this.CharacterPosition = this.hero.Position;
                    this.walkInstance.Play();
                    this.walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.S))
            {
                if (oldPos.Y < this.PRoom.Height - 90)
                {
                    this.hero.Position = new Vector2(oldPos.X, oldPos.Y + this.hero.Speed);
                    this.CharacterPosition = this.hero.Position;
                    this.walkInstance.Play();
                    this.walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.D))
            {
                if (oldPos.X < this.PRoom.Width - 50)
                {
                    this.hero.Position = new Vector2(oldPos.X + this.hero.Speed, oldPos.Y);
                    this.CharacterPosition = this.hero.Position;
                    this.walkInstance.Play();
                    this.walkInstance2.Play();
                }
            }

            oldPos = this.hero.Position;

            this.hero.Rotation = this.PointDirecions(this.hero.Position.X, this.hero.Position.Y, this.mouse.X, this.mouse.Y);
        }

        private void UpdateBullets()
        {
            foreach (var bullet in this.bullets)
            {
                if (bullet.Alive)
                {
                    bullet.Update();
                }                   
            }

            foreach (var bullet in this.enemyBullets)
            {
                if (bullet.Alive)
                {
                    bullet.Update();
                }                 
            }
        }

        private void UpdateUnits()
        {
            foreach (var unit in this.units)
            {
                if (unit is IShootable)
                {
                    if (unit is ILevelable)
                    {
                        unit.FiringTimer++;
                        if (this.mouse.LeftButton == ButtonState.Released && this.previousMouse.LeftButton == ButtonState.Pressed)
                        {
                            this.hero.CheckShooting(this.PBullets);
                            if (this.loaded)
                            {
                                this.gunShot.Play();
                            }

                            this.loaded = true;
                        }
                    }
                    else
                    {
                        unit.FiringTimer++;
                        if (Math.Abs(this.hero.Position.X - unit.Position.X) < 200 &&
                            Math.Abs(this.hero.Position.Y - unit.Position.Y) < 200)
                        {
                            unit.Rotation = this.PointDirecions(unit.Position.X, unit.Position.Y, this.CharacterPosition.X, this.CharacterPosition.Y);
                            unit.Update();
                            unit.CheckShooting(this.EnemyBullets);
                        }
                    }
                }
                else
                {
                    if (Math.Abs(this.hero.Position.X - unit.Position.X) < 200 &&
                        Math.Abs(this.hero.Position.Y - unit.Position.Y) < 200)
                    {
                        unit.Rotation = this.PointDirecions(unit.Position.X, unit.Position.Y, this.CharacterPosition.X, this.CharacterPosition.Y);
                        unit.Update();
                    }
                }              
            }
        }

        private void UpdateCursor()
        {
            this.cursor.Position = new Vector2(this.mouse.X - 11, this.mouse.Y - 11);
        }

        private void AddMeleUnit(ContentManager content, int x, int y, string textureName)
        {
            MeleUnits meleUnit = new MeleUnits(new Vector2(x, y), 0.7f);
            meleUnit.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", textureName));
            this.units.Add(meleUnit);
        }

        private void AddRangeUnit(ContentManager content, int x, int y, string textureName)
        {
            RangedUnits rangedUnit = new RangedUnits(new Vector2(x, y), 0);
            rangedUnit.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", textureName));
            this.units.Add(rangedUnit);
        }

        private void ObjectDraw(SpriteBatch spriteBatch, Texture2D sprite, Vector2 position, float rotation)
        {
            Vector2 center = new Vector2(sprite.Width / 2, sprite.Height / 2);
            float scale = 0.7f;

            spriteBatch.Draw(sprite, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
        }

        private float PointDirecions(float x, float y, float x2, float y2)
        {
            float divX = x - x2;
            float divY = y - y2;
            float adj = divX;
            float opp = divY;
            float tan = opp / adj;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0)
            {
                res += 360;
            }

            return res;
        }
    }
}