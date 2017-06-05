using Microsoft.Xna.Framework;

namespace MultiPongServer
{
    public class Pad
    {
        public Vector2 Position { get; private set; }

        public Rectangle Rectangle { get; private set; }

        public void Move(Vector2 newVec)
        {
            Position = new Vector2(Position.X, newVec.Y);
        }

        public Pad(Rectangle rectangle, Vector2 initialPosition)
        {
            Position = initialPosition;
            Rectangle = rectangle;
        }
    }
}