using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiPongServer;

namespace MultiPongServer
{
    public class BallServer
    {
        public Vector2 position;
        public Vector2 velocity;
        const float acceleration = 30f;

        public const int RADIUS = 50;

        int height;

        private void Bounce(Pad Player)
        {
            Vector2 ballCentre = position + new Vector2(RADIUS);
            Vector2 A, B, C, D, scaling;
            A = Player.position;
            B = new Vector2(Player.position.X + Player.rectangle.Width, Player.position.Y);
            C = new Vector2(Player.position.X, Player.position.Y + Player.rectangle.Height);
            D = new Vector2(Player.position.X + Player.rectangle.Width,
                            Player.position.Y + Player.rectangle.Height);

            if (Vector2.Distance(ballCentre, A) <= RADIUS)
            {
                scaling = (ballCentre - A);
                scaling.Normalize();
                scaling *= velocity.Length();
                velocity = scaling;
                return;
            }
            if (Vector2.Distance(ballCentre, B) <= RADIUS)
            {
                scaling = (ballCentre - B);
                scaling.Normalize();
                scaling *= velocity.Length();
                velocity = scaling;
                return;
            }
            if (Vector2.Distance(ballCentre, C) <= RADIUS)
            {
                scaling = (ballCentre - C);
                scaling.Normalize();
                scaling *= velocity.Length();
                velocity = scaling;
                return;
            }
            if (Vector2.Distance(ballCentre, D) <= RADIUS)
            {
                scaling = (ballCentre - D);
                scaling.Normalize();
                scaling *= velocity.Length();
                velocity = scaling;
                return;
            }

            if (position.X + 2 * RADIUS >= Player.position.X
                && (position.Y + RADIUS) <= Player.position.Y
                && (position.Y + RADIUS) >= Player.position.Y + Player.rectangle.Height)
            {
                velocity *= new Vector2(-1, 1);
                return;
            }
            if (position.X <= (Player.position.X + Player.rectangle.Width) &&
                (position.Y + RADIUS) <= Player.position.Y &&
                (position.Y + RADIUS) >= Player.position.Y + Player.rectangle.Height)
            {
                velocity *= new Vector2(-1, 1);
                return;
            }

            if (position.X + 2 * RADIUS >= Player.position.X &&
                position.X + 2 * RADIUS <= (Player.position.X + Player.rectangle.Width) &&
                (position.Y + RADIUS) <= Player.position.Y &&
                (position.Y + RADIUS) >= Player.position.Y + Player.rectangle.Height)
            {
                velocity *= new Vector2(-1, 1);
                return;
            }
            if (position.X <= (Player.position.X + Player.rectangle.Width)
                && position.X >= Player.position.X
                && (position.Y + RADIUS) <= Player.position.Y
                && (position.Y + RADIUS) >= Player.position.Y + Player.rectangle.Height)
            {
                velocity *= new Vector2(-1, 1);
                return;
            }
        }

        public BallServer(Vector2 initialPosition, Vector2 initVelocity, int screenHeight)
        {
            position = initialPosition;
            velocity = initVelocity;
            height = screenHeight;
        }

        public void Update(TimeSpan gameTime, GameState gameState)
        {
            position += (float) gameTime.TotalSeconds * velocity;
            var direction = velocity;
            direction.Normalize();
            velocity += direction * acceleration * (float) gameTime.TotalSeconds;
            Bounce(gameState.Player1);
            Bounce(gameState.Player2);
        }
    }
}