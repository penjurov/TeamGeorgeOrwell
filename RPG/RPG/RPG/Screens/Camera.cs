namespace RPG
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Camera
    {
        private static float zoomAmount = 0.1f;
        private Vector2 position;
        private float rotation;
        private float zoom;
        private int maxZoom;
        private int minZoom;
        private Matrix matrixTransform;

        public Camera()
        {
            this.Position = new Vector2(GameScreen.PRoom.Width / 2, GameScreen.PRoom.Height / 2);
            this.Rotation = 0.0f;
            this.Zoom = 1.0f;

            float wRatio = (float)GameScreen.PRoom.Width / (float)GameScreen.PScreen.Width;
            float hRatio = (float)GameScreen.PRoom.Height / (float)GameScreen.PScreen.Height;
            bool lowestRatio = (wRatio < hRatio) ? true : false;

            float rLength = lowestRatio ? GameScreen.PRoom.Width : GameScreen.PScreen.Width;
            float sLength = lowestRatio ? GameScreen.PRoom.Height : GameScreen.PScreen.Height;

            float tempZoom = 1.0f;
            int c = 0;

            while (sLength * tempZoom < rLength)
            {
                tempZoom *= 1.0f + (zoomAmount * 2);
                c++;
            }

            this.MaxZoom = c;

            if (this.MaxZoom > 9)
            {
                this.MaxZoom = 9;
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
                return this.zoom;
            }

            set
            {
                this.zoom = value;
                if (this.zoom < 1.0f - (zoomAmount * this.MaxZoom))
                {
                    this.zoom = 1.0f - (zoomAmount * this.MaxZoom);
                }

                if (this.zoom > 1.0f + (zoomAmount * this.MinZoom))
                {
                    this.zoom = 1.0f + (zoomAmount * this.MinZoom);
                }
            }
        }

        public int MaxZoom
        {
            get
            {
                return this.maxZoom;
            }

            set
            {
                this.maxZoom = value;
            }
        }

        public int MinZoom
        {
            get
            {
                return this.minZoom;
            }

            set
            {
                this.minZoom = value;
            }
        }

        public Matrix MatrixTransform
        {
            get
            {
                return this.matrixTransform;
            }

            set
            {
                this.matrixTransform = value;
            }
        }

        public static Vector2 GlobalToLocal(Vector2 pos)
        {
            pos -= RPG.PCamera.Position - new Vector2(GameScreen.PScreen.Width / 2, GameScreen.PScreen.Height / 2);
            return pos;
        }

        public static Vector2 LocalToGlobal(Vector2 pos)
        {
            pos += RPG.PCamera.Position - new Vector2(GameScreen.PScreen.Width / 2, GameScreen.PScreen.Height / 2);
            return pos;
        }

        public void Move(Vector2 ammount)
        {
            this.Position += ammount;
        }

        public Matrix Transform(GraphicsDevice graphics)
        {
            float viewPortWidth = graphics.Viewport.Width;
            float viewPortHeight = graphics.Viewport.Height;

            this.MatrixTransform = Matrix.CreateTranslation(new Vector3(-this.Position.X, -this.Position.Y, 0))
                * Matrix.CreateRotationZ(this.Rotation)
                * Matrix.CreateScale(this.Zoom)
                * Matrix.CreateTranslation(new Vector3(viewPortWidth * 0.5f, viewPortHeight * 0.5f, 0));

            return this.MatrixTransform;
        }
    }
}
