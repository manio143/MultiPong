using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongClient
{
    public class Pad
    {
        public Vector2 position;

        public Texture2D rectangle;

        public Rectangle Bounds
        {
            get
            {
                var rect = rectangle.Bounds;
                rect.Offset(position);
                return rect;
            }
        }

        public void move(Vector2 x)
        {
            position += x;
        }

        public Pad(Texture2D rectangle, Vector2 initialPosition)
        {
            position = initialPosition;
            this.rectangle = rectangle;
        }

        public Pad(Vector2 initialPosition)
        {
            position = initialPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rectangle, position, Color.White);
        }
    }
}