using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongClient
{
    public class Ball
    {
        Vector2 position;
        Vector2 velocity;

        public const int RADIUS = 50;

        int height;

        Texture2D circle;

        public Ball(Texture2D circle, Vector2 initialPosition, Vector2 initVelocity, int screenHeight)
        {
            position = initialPosition;
            velocity = initVelocity;
            this.circle = circle;
            height = screenHeight;
        }

        public void Update(GameTime gameTime)
        {
            position += (float) gameTime.ElapsedGameTime.TotalSeconds * velocity;
        }

        public void Move(Vector2 position)
        {
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(circle, position, Color.White);
        }

        internal void Bounce(Vector2 bounceFactor)
        {
            velocity *= bounceFactor;
        }
    }
}