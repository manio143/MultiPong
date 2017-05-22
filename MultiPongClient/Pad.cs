using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongClient
{
    public class Pad
    {
        public Vector2 Position { get; private set; }

        public Texture2D Rectangle { get; private set; }

        public void Move(Vector2 x)
        {
            Position = x;
        }

        public Pad(Texture2D rectangle, Vector2 initialPosition)
        {
            Position = initialPosition;
            Rectangle = rectangle;
        }

        public Pad(Vector2 initialPosition)
        {
            Position = initialPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Rectangle, Position, Color.White);
        }
    }
}