using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongClient
{
    public class Pad
    {
        Vector2 position;

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

        public Pad(Texture2D rectangle, Vector2 initialPosition)
        {
            position = initialPosition;
            this.rectangle = rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rectangle, position, Color.White);
        }
    }
}