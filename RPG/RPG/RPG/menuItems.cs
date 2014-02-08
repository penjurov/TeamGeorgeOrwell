namespace RPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Storage;
    using Microsoft.Xna.Framework.GamerServices;

    public class menuItems
    {
        private Texture2D itemTexture;
        private Vector2 itemPosition;
        private string itemText;
        private SpriteFont itemFont;
        private bool selected;

        public menuItems(Texture2D iTexture, Vector2 iPosition, string iText, SpriteFont iFont, bool iSelected)
        {
            this.ItemTexture = iTexture;
            this.ItemPosition = iPosition;
            this.ItemText = iText;
            this.ItemFont = iFont;
            this.Selected = iSelected;
        }

        public Texture2D ItemTexture
        {
            get
            {
                return this.itemTexture;
            }
            set
            {
                this.itemTexture = value;
            }
        }

        public Vector2 ItemPosition
        {
            get
            {
                return this.itemPosition;
            }
            set
            {
                this.itemPosition = value;
            }
        }

        public string ItemText
        {
            get
            {
                return this.itemText;
            }
            set
            {
                this.itemText = value;
            }
        }

        public SpriteFont ItemFont
        {
            get
            {
                return this.itemFont;
            }
            set
            {
                this.itemFont = value;
            }
        }

        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Color color = Selected ? new Color(248, 218, 127) : Color.Black;
            spriteBatch.Draw(ItemTexture, ItemPosition, Color.White);
          
            Vector2 textSize = ItemFont.MeasureString(ItemText);
            Vector2 textPosition = ItemPosition + new Vector2(
                        (float)Math.Floor((ItemTexture.Width - textSize.X) / 2),
                        (float)Math.Floor((ItemTexture.Height - textSize.Y) / 2));
            spriteBatch.DrawString(ItemFont, ItemText, textPosition, color);
        }
    }
}
