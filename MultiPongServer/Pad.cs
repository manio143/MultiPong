using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongServer
{
    public class Pad
    {
        public Vector2 position;

        public Rectangle rectangle;

        public void move(Vector2 x)
        {
            position += x;
        }

        public Pad(Rectangle rectangle, Vector2 initialPosition)
        {
            position = initialPosition;
            this.rectangle = rectangle;
        }
    }
}