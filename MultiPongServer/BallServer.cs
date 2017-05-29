using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using MultiPongCommon;

namespace MultiPongServer
{
    public enum Outside
    {
        None = 0,
        Left,
        Right
    }

    public struct BallServer
    {
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Outside Outside { get; set; }
        const float acceleration = MultiPongCommon.Constants.BALL_ACCELERATION;

        public const int RADIUS = MultiPongCommon.Constants.BALL_RADIUS;

        int height;

        private void Bounce(Pad Player)
        {
            Vector2 ballCentre = Position + new Vector2(RADIUS);
            Vector2 A, B, C, D, scaling;
            A = Player.Position;
            B = new Vector2(Player.Position.X + Player.Rectangle.Width, Player.Position.Y);
            C = new Vector2(Player.Position.X, Player.Position.Y + Player.Rectangle.Height);
            D = new Vector2(Player.Position.X + Player.Rectangle.Width,
                Player.Position.Y + Player.Rectangle.Height);

            if (Vector2.Distance(ballCentre, A) <= RADIUS)
            {
                scaling = (ballCentre - A);
                scaling.Normalize();
                scaling *= Velocity.Length();
                Velocity = scaling;
                return;
            }
            if (Vector2.Distance(ballCentre, B) <= RADIUS)
            {
                scaling = (ballCentre - B);
                scaling.Normalize();
                scaling *= Velocity.Length();
                Velocity = scaling;
                return;
            }
            if (Vector2.Distance(ballCentre, C) <= RADIUS)
            {
                scaling = (ballCentre - C);
                scaling.Normalize();
                scaling *= Velocity.Length();
                Velocity = scaling;
                return;
            }
            if (Vector2.Distance(ballCentre, D) <= RADIUS)
            {
                scaling = (ballCentre - D);
                scaling.Normalize();
                scaling *= Velocity.Length();
                Velocity = scaling;
                return;
            }

            if (Position.X + 2 * RADIUS >= Player.Position.X
                && (Position.Y + RADIUS) <= Player.Position.Y
                && (Position.Y + RADIUS) >= Player.Position.Y + Player.Rectangle.Height)
            {
                Velocity *= new Vector2(-1, 1);
                return;
            }
            if (Position.X <= (Player.Position.X + Player.Rectangle.Width) &&
                (Position.Y + RADIUS) <= Player.Position.Y &&
                (Position.Y + RADIUS) >= Player.Position.Y + Player.Rectangle.Height)
            {
                Velocity *= new Vector2(-1, 1);
                return;
            }

            if (Position.X + 2 * RADIUS >= Player.Position.X &&
                Position.X + 2 * RADIUS <= (Player.Position.X + Player.Rectangle.Width) &&
                (Position.Y + RADIUS) <= Player.Position.Y &&
                (Position.Y + RADIUS) >= Player.Position.Y + Player.Rectangle.Height)
            {
                Velocity *= new Vector2(-1, 1);
                return;
            }
            if (Position.X <= (Player.Position.X + Player.Rectangle.Width)
                && Position.X >= Player.Position.X
                && (Position.Y + RADIUS) <= Player.Position.Y
                && (Position.Y + RADIUS) >= Player.Position.Y + Player.Rectangle.Height)
            {
                Velocity *= new Vector2(-1, 1);
                return;
            }
        }

        public BallServer(Vector2 initialPosition, Vector2 initVelocity, int screenHeight)
        {
            Position = initialPosition;
            Velocity = initVelocity;
            height = screenHeight;
            Outside = Outside.None;
        }

        public void Update(TimeSpan gameTime, GameState gameState)
        {
            Position += (float) gameTime.TotalSeconds * Velocity;
            var direction = Velocity;
            direction.Normalize();
            Velocity += direction * acceleration * (float) gameTime.TotalSeconds;

            if (Position.Y < 0 || Position.Y + 2 * RADIUS > height)
                BounceBoundaries(gameTime, gameState);

            Bounce(gameState.Player1);
            Bounce(gameState.Player2);
        
            SetOutside();
        }

        private void SetOutside()
        {
            if (Position.X + 2*RADIUS < 0)
                Outside = Outside.Left;
            else if (Position.X > Constants.SCREEN_WIDTH)
                Outside = Outside.Right;
            else
                Outside = Outside.None;
        }

        private void BounceBoundaries(TimeSpan gameTime, GameState gameState)
        {
            Velocity = new Vector2(Velocity.X, -Velocity.Y);
            Update(gameTime, gameState); // move back to the field
        }
    }
}