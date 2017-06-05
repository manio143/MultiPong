using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiPongCommon;

namespace MultiPongClient
{
    public class Ball : RenderedObject
    {
        Vector2 velocity;
        private Vector2 screen;

        public const int RADIUS = MultiPongCommon.Constants.BALL_RADIUS;

        Texture2D circle;

        public Ball(Vector2 screen, Texture2D circle, Vector2 initialPosition, Vector2 initVelocity)
        :base(screen)
        {
            Position = initialPosition;
            velocity = initVelocity;
            this.circle = circle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(circle, Position, Color.White);
        }
    }
}