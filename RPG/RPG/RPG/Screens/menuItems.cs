namespace Rpg.Screens
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MenuItems
    {
        public MenuItems(Texture2D iTexture, Vector2 iPosition, string iText, SpriteFont iFont, bool iSelected)
        {
            this.ItemTexture = iTexture;
            this.ItemPosition = iPosition;
            this.ItemText = iText;
            this.ItemFont = iFont;
            this.Selected = iSelected;
        }

        public Texture2D ItemTexture { get; set; }

        public Vector2 ItemPosition { get; set; }

        public string ItemText { get; set; }

        public SpriteFont ItemFont { get; set; }

        public bool Selected { get; set; }

        public void DrawMenuItems(SpriteBatch spriteBatch, Color menuColor)
        {
            Color color = this.Selected ? menuColor : Color.Black;
            
            spriteBatch.Draw(this.ItemTexture, this.ItemPosition, Color.White);

            Vector2 textSize = this.ItemFont.MeasureString(this.ItemText);
            Vector2 textPosition = this.ItemPosition + new Vector2(
                (float)Math.Floor((this.ItemTexture.Width - textSize.X) / 2),
                (float)Math.Floor((this.ItemTexture.Height - textSize.Y) / 2));
            spriteBatch.DrawString(this.ItemFont, this.ItemText, textPosition, color);
        }
    }
}