namespace RPG
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MenuItems
    {
        private Texture2D itemTexture;
        private Vector2 itemPosition;
        private string itemText;
        private SpriteFont itemFont;
        private bool selected;

        public MenuItems(Texture2D iTexture, Vector2 iPosition, string iText, SpriteFont iFont, bool iSelected)
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
            Color color = this.Selected ? new Color(248, 218, 127) : Color.Black;
            spriteBatch.Draw(this.ItemTexture, this.ItemPosition, Color.White);

            Vector2 textSize = this.ItemFont.MeasureString(this.ItemText);
            Vector2 textPosition = this.ItemPosition + new Vector2(
                        (float)Math.Floor((this.ItemTexture.Width - textSize.X) / 2),
                        (float)Math.Floor((this.ItemTexture.Height - textSize.Y) / 2));
            spriteBatch.DrawString(this.ItemFont, this.ItemText, textPosition, color);
        }
    }
}
