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

    public class Camera
    {
        private Vector2 position;
        private float rotation;
        private float zoom;
        public int maxZoom;
        public int minZoom;
        private Matrix m_transform;
        public static float zoomAmount = 0.1f;

        public Camera()
        {
            position = new Vector2(RPG.room.Width / 2, RPG.room.Height / 2);
            rotation = 0.0f;
            zoom = 1.0f;

            float wRatio = (float)RPG.room.Width / (float)RPG.screen.Width;
            float hRatio = (float)RPG.room.Height / (float)RPG.screen.Height;
            bool lowestRatio = (wRatio < hRatio) ? true : false;

            float rLength = (lowestRatio) ? RPG.room.Width : RPG.screen.Width;
            float sLength = (lowestRatio) ? RPG.room.Height : RPG.screen.Height;

            float tempZoom = 1.0f;
            int c = 0;

            while (sLength * tempZoom < rLength)
            {
                tempZoom *= 1.0f + (zoomAmount * 2);
                c++;
            }

            maxZoom = c;

            if (maxZoom > 9)
            {
                maxZoom = 9;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public float Rotation
        {
            get
            {
                return this.rotation;
            }
            set
            {
                this.rotation = value;
            }
        }

        public float Zoom
        {
            get
            {
                return this.rotation;
            }
            set
            {
                zoom = value;
                if (zoom < 1.0f - (zoomAmount * maxZoom))
                {
                    zoom = 1.0f - (zoomAmount * maxZoom);
                }

                if (zoom > 1.0f + (zoomAmount * minZoom))
	            {
		            zoom = 1.0f + (zoomAmount * minZoom);
	            }
            }
        }

        public void Move(Vector2 Ammount)
        {
            Position += Ammount;
        }

        public Matrix Transform(GraphicsDevice graphics)
        {
            float viewPortWidth = graphics.Viewport.Width;
            float viewPortHeight = graphics.Viewport.Height;

            m_transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateScale(zoom)
                * Matrix.CreateTranslation(new Vector3(viewPortWidth * 0.5f, viewPortHeight * 0.5f, 0));

            return m_transform;
        }

        public void Update()
        {
           
        }

        public static Vector2 globalToLocal(Vector2 pos)
        {
            pos -= (RPG.camera.Position - new Vector2(RPG.screen.Width / 2, RPG.screen.Height / 2));
            return pos;
        }

        public static Vector2 localToGlobal(Vector2 pos)
        {
            pos += (RPG.camera.Position - new Vector2(RPG.screen.Width / 2, RPG.screen.Height / 2));
            return pos;
        }
    }
}
