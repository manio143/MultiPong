using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongServer
{
    public class Pad
    {
        public Vector2 Position { get; private set; }

        public Rectangle Rectangle { get; private set; }

        public void Move(Vector2 x)
        {
            Position += x;
        }

        public Pad(Rectangle rectangle, Vector2 initialPosition)
        {
            Position = initialPosition;
            Rectangle = rectangle;
        }
    }
}