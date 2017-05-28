using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongClient
{
    public class Ball
    {
        Vector2 position;
        Vector2 velocity;

        public const int RADIUS = MultiPongCommon.Constants.BALL_RADIUS;

        Texture2D circle;

        public Ball(Texture2D circle, Vector2 initialPosition, Vector2 initVelocity)
        {
            position = initialPosition;
            velocity = initVelocity;
            this.circle = circle;
        }

        public void Move(Vector2 position)
        {
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(circle, position, Color.White);
        }
    }
}