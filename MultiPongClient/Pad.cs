using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongClient
{
    public class Pad : RenderedObject
    {
        public Texture2D Rectangle { get; private set; }

        public Pad(Vector2 screen, Texture2D rectangle, Vector2 initialPosition)
        :base(screen)
        {
            Position = initialPosition;
            Rectangle = rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Rectangle, Position, Color.White);
        }
    }
}