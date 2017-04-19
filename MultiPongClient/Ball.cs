using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiPongClient
{
    public class Ball
    {
        Vector2 position;
        Vector2 velocity;
        const float acceleration = 30f;

        public const int RADIUS = 50;

        int height;

        public Texture2D circle;

        public Rectangle Bounds
        {
            get
            {
                var rect = circle.Bounds;
                rect.Offset(position);
                return rect;
            }
        }

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
            var direction = velocity;
            direction.Normalize();
            velocity += direction * acceleration * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (position.Y < 0 || position.Y + RADIUS > height)
                Bounce(new Vector2(1, -1));
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