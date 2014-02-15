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
        
        private IList<Obstacles> obstacles = new List<Obstacles>();
        private IList<Bullet> bullets = new List<Bullet>();
        private IList<Bullet> enemyBullets = new List<Bullet>();
        private bool loaded = false;          
        private Rectangle room;
        private Random rand = new Random();
          
        private Texture2D gameWindowTexture;
        private Vector2 gameWindowTexturePos;
        private IList<Units> units = new List<Units>();
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

        public void Load(ContentManager content)
        {
            this.LoadMusic(content);
            this.LoadLevel(content);
            this.LoadHero(content);
            this.LoadUnits(content);
            this.LoadCursor(content);
            this.LoadBullets(content);
            this.LoadObstacles(content);
        }
          
        public void Draw(SpriteBatch spriteBatch, ContentManager content)
        {           
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);
                this.DrawBackGround(spriteBatch);
                this.DrawUnits(spriteBatch);
                this.DrawLabels(spriteBatch, content);
                this.DrawBullets(spriteBatch);
                this.DrawObstacles(spriteBatch);
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

            for (int i = 0; i < 10; i++)
            {
                Bullet o = new Bullet(new Vector2(0, 0), bulletTexture);
                o.Area = new Rectangle(0, 0, bulletTexture.Width, bulletTexture.Height);
                this.bullets.Add(o);
            }

            for (int i = 0; i < 10; i++)
            {
                Bullet o = new Bullet(new Vector2(0, 0), bulletTexture);
                o.Area = new Rectangle(0, 0, bulletTexture.Width, bulletTexture.Height);
                this.enemyBullets.Add(o);
            }
        }

        private void LoadObstacles(ContentManager content)
        {
            Texture2D invisTexture = content.Load<Texture2D>(@"Textures\Objects\invisible");

            for (int i = 150; i < 275; i+=25)
			{
			    Obstacles invisble = new Obstacles(new Vector2(i, 360), invisTexture, false);
                invisble.Area = new Rectangle((int)invisble.Position.X, (int)invisble.Position.Y, invisTexture.Width, invisTexture.Height);
                this.obstacles.Add(invisble);
			}

            for (int i = 150; i < 275; i += 25)
            {
                Obstacles invisble = new Obstacles(new Vector2(i, 475), invisTexture, false);
                invisble.Area = new Rectangle((int)invisble.Position.X, (int)invisble.Position.Y, invisTexture.Width, invisTexture.Height);
                this.obstacles.Add(invisble);
            }

            Texture2D pilarTexture = content.Load<Texture2D>(@"Textures\Objects\pillar");
            Obstacles pillar = new Obstacles(new Vector2(580, 580), pilarTexture, true);
            pillar.Area = new Rectangle((int)pillar.Position.X, (int)pillar.Position.Y, pilarTexture.Width, pilarTexture.Height);
            this.obstacles.Add(pillar);

            pillar = new Obstacles(new Vector2(420, 660), pilarTexture, true);
            pillar.Area = new Rectangle((int)pillar.Position.X, (int)pillar.Position.Y, pilarTexture.Width, pilarTexture.Height);
            this.obstacles.Add(pillar);
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
            switch (ChooseHeroScreen.HeroName)
            {
                case "ODIN":
                    {
                        this.hero = new Heroes(new Vector2(this.room.Width / 2, this.room.Height / 2), 2, 900, 110, 70);
                        break;
                    }

                case "THOR":
                    {
                        this.hero = new Heroes(new Vector2(this.room.Width / 2, this.room.Height / 2), 2, 1000, 130, 90);
                        break;
                    }

                case "EIR":
                    {
                        this.hero = new Heroes(new Vector2(this.room.Width / 2, this.room.Height / 2), 2, 750, 90, 60);
                        break;
                    }

                default:
                    break;
            }

            this.hero.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", ChooseHeroScreen.HeroName));
            this.hero.Area = new Rectangle(0, 0, this.hero.SpriteIndex.Width, this.hero.SpriteIndex.Height);
            this.hero.Position = new Vector2(50, 400);
            this.hero.Alive = true;
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

        private void DrawObstacles(SpriteBatch spriteBatch)
        {
            foreach (var obstacles in this.obstacles)
            {
                this.ObjectDraw(spriteBatch, obstacles.SpriteIndex, obstacles.Position, obstacles.Rotation);
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
            
            statsPosition = new Vector2(10, 100);
            spriteBatch.DrawString(font, string.Format("Experience :  {0}", this.hero.CurrentExp), statsPosition, Color.Red);    
        }

        private void DrawUnits(SpriteBatch spriteBatch)
        {
            foreach (var unit in this.units)
            {
                if (unit.Alive)
                {
                    this.ObjectDraw(spriteBatch, unit.SpriteIndex, unit.Position, unit.Rotation);   
                }
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
            int x = (int)this.hero.Position.X;
            int y = (int)this.hero.Position.Y; 
            this.hero.Area = new Rectangle(x, y, hero.Area.Width, hero.Area.Height);


            if (this.keyboard.IsKeyDown(Keys.W) && !collision(new Vector2(0, -hero.Speed), hero))
            {
                if (oldPos.Y > this.room.Y + 20)
                {
                    this.hero.Position = new Vector2(oldPos.X, oldPos.Y - this.hero.Speed);
                    this.walkInstance.Play();
                    this.walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.A) && !collision(new Vector2(-hero.Speed, 0), hero))
            {
                if (oldPos.X > this.room.X + 20)
                {
                    this.hero.Position = new Vector2(oldPos.X - this.hero.Speed, oldPos.Y);
                    this.walkInstance.Play();
                    this.walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.S) && !collision(new Vector2(0, hero.Speed), hero))
            {
                if (oldPos.Y < this.room.Height - 90)
                {
                    this.hero.Position = new Vector2(oldPos.X, oldPos.Y + this.hero.Speed);
                    this.walkInstance.Play();
                    this.walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.D) && !collision(new Vector2(hero.Speed, 0), hero))
            {
                if (oldPos.X < this.room.Width - 50)
                {
                    this.hero.Position = new Vector2(oldPos.X + this.hero.Speed, oldPos.Y);
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
                if (bullet.Alive && Math.Abs(this.hero.Position.X - bullet.Position.X) < 200 &&
                        Math.Abs(this.hero.Position.Y - bullet.Position.Y) < 200) 
                {
                    bullet.Area = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.SpriteIndex.Width, bullet.SpriteIndex.Height);
                    bullet.Position += this.PushTo(bullet.Speed, bullet.Rotation, bullet);
                }    
                else
                {
                    bullet.Alive = false;
                }
            }

            foreach (var bullet in this.enemyBullets)
            {
                if (bullet.Alive)
                {
                    bullet.Area = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.SpriteIndex.Width, bullet.SpriteIndex.Height);
                    bullet.Position += this.PushTo(bullet.Speed, bullet.Rotation, bullet);
                }                 
            }
        }

        private void UpdateUnits()
        {
            foreach (var unit in this.units)
            {
                if (unit.Alive)
                {
                    int x = (int)unit.Position.X;
                    int y = (int)unit.Position.Y;
                    unit.Area = new Rectangle(x, y, unit.Area.Width, unit.Area.Height);

                    if (collision(new Vector2(0, 0), unit))
                    {
                        unit.Health = unit.Health - ((hero.Attack / unit.Defence) * 20) + rand.Next((int)hero.Attack/10);
                        if (unit.Health < 0)
                        {
                            unit.Alive = false;
                            hero.CurrentExp = hero.CurrentExp + unit.ExpGiven;
                        }
                    }

                    if (unit is ILevelable)
                    {
                        unit.FiringTimer++;
                        if (this.mouse.LeftButton == ButtonState.Released && this.previousMouse.LeftButton == ButtonState.Pressed)
                        {
                            this.hero.CheckShooting(this.bullets);
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
                            unit.Rotation = this.PointDirecions(unit.Position.X, unit.Position.Y, this.hero.Position.X, this.hero.Position.Y);
                            unit.Position += this.PushTo(unit.Speed, unit.Rotation, unit);
                            unit.CheckShooting(this.enemyBullets);
                        }
                    }
                }              
            }
        }

        private void UpdateCursor()
        {
            this.cursor.Position = new Vector2(this.mouse.X - this.cursor.SpriteIndex.Width / 2, this.mouse.Y - this.cursor.SpriteIndex.Height / 2);
        }

        private void AddMeleUnit(ContentManager content, int x, int y, string textureName)
        {
            MeleUnits meleUnit = new MeleUnits(new Vector2(x, y), 1.3f);
            meleUnit.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", textureName));
            meleUnit.Area = new Rectangle(0, 0, meleUnit.SpriteIndex.Width, meleUnit.SpriteIndex.Height);
            meleUnit.Health = 260;
            meleUnit.Defence = 40;
            meleUnit.Attack = 70;
            meleUnit.ExpGiven = 230;
            meleUnit.Alive = true;
            this.units.Add(meleUnit);
        }

        private void AddRangeUnit(ContentManager content, int x, int y, string textureName)
        {
            RangedUnits rangedUnit = new RangedUnits(new Vector2(x, y), 0);
            rangedUnit.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", textureName));
            rangedUnit.Area = new Rectangle(0, 0, rangedUnit.SpriteIndex.Width, rangedUnit.SpriteIndex.Height);
            rangedUnit.Health = 210;
            rangedUnit.Defence = 30;
            rangedUnit.Attack = 80;
            rangedUnit.ExpGiven = 180;
            rangedUnit.Alive = true;
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

        private bool collision(Vector2 pos, Obj obj)
        {
            Rectangle newArea = new Rectangle(obj.Area.X, obj.Area.Y, obj.Area.Width, obj.Area.Height);
            if (pos.X < 1)
            {
                newArea.X += (int)(pos.X * 3);
            }
            else
            {
                newArea.X += (int)pos.X;        
            }

            if (pos.Y < 1)
            {
                newArea.Y += (int)(pos.Y * 3); ;
            }
            else
            {
                newArea.Y += (int)pos.Y;
            }

            foreach (var o in obstacles)
            {
                if (obj.GetType() == typeof(Bullet))           
                {             
                    if (o.Visible)
                    {
                        if ((newArea.X + pos.X + newArea.Width / 2) > o.Area.X && newArea.X < (o.Area.X + o.Area.Width)
                            && (newArea.Y + pos.Y + newArea.Height / 2) > o.Area.Y && newArea.Y < (o.Area.Y + o.Area.Height))
                        {
                            obj.Alive = false;
                            return true;
                        }
                    }
                }
                else
                {
                    if ((newArea.X + pos.X + newArea.Width / 2) > o.Area.X && newArea.X < (o.Area.X + o.Area.Width)
                        && (newArea.Y + pos.Y + newArea.Height / 2) > o.Area.Y && newArea.Y < (o.Area.Y + o.Area.Height))
                    {
                        return true;
                    }
                }
            }


            if (obj.GetType() == typeof(MeleUnits) || obj.GetType() == typeof(RangedUnits))
            {
                foreach (var o in bullets)
                {
                    if(o.Alive)
                    {
                        if ((newArea.X + pos.X + newArea.Width / 2) > o.Area.X && newArea.X < (o.Area.X + o.Area.Width)
                                && (newArea.Y + pos.Y + newArea.Height / 2) > o.Area.Y && newArea.Y < (o.Area.Y + o.Area.Height))
                        {
                            o.Alive = false;
                            return true;                            
                        } 
                    }       
                }
            }
           
            return false;
        }

        private Vector2 PushTo(float pix, float dir, Obj unit)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));

            if (!collision(new Vector2(newX, newY), unit))
	        {                
                return new Vector2(pix * newX, pix * newY);
	        }

            return new Vector2(0, 0);                    
        }
    }
}