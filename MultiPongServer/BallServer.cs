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

        private void BounceCorner(Pad player)
        {
            Vector2 ballCentre = Position + new Vector2(RADIUS);
            var leftUpperCorner = player.Position;
            var rightUpperCorner = new Vector2(player.Position.X + player.Rectangle.Width, player.Position.Y);
            var leftLowerCorner = new Vector2(player.Position.X, player.Position.Y + player.Rectangle.Height);
            var rightLowerCorner = new Vector2(player.Position.X + player.Rectangle.Width,
                player.Position.Y + player.Rectangle.Height);

            var corners = new[] {leftLowerCorner, leftUpperCorner, rightLowerCorner, rightUpperCorner};

            foreach (var corner in corners)
            {
                if (Vector2.Distance(ballCentre, corner) <= RADIUS)
                {
                    //Go back where you came from with the same speed
                    var scaling = (ballCentre - corner);
                    scaling *= Velocity.Length() / scaling.Length();
                    Velocity = scaling;
                    break;
                }
            }
        }

        private bool Bounce(Pad player1, Pad player2)
        {
            if ((Position.X + 2 * RADIUS >= player2.Position.X
                 && (Position.Y + RADIUS) >= player2.Position.Y
                 && (Position.Y + RADIUS) <= player2.Position.Y + player2.Rectangle.Height) ||
                (Position.X <= player1.Position.X + player1.Rectangle.Width &&
                 (Position.Y + RADIUS) >= player1.Position.Y &&
                 (Position.Y + RADIUS) <= player1.Position.Y + player1.Rectangle.Height))
            {
                Velocity *= new Vector2(-1, 1);
                return true;
            }
            return false;
//
//            if (Position.X + 2 * RADIUS >= Player.Position.X &&
//                Position.X + 2 * RADIUS <= (Player.Position.X + Player.Rectangle.Width) &&
//                (Position.Y + RADIUS) >= Player.Position.Y &&
//                (Position.Y + RADIUS) <= Player.Position.Y + Player.Rectangle.Height)
//            {
//                Velocity *= new Vector2(1, -1);
//                return;
//            }
//            if (Position.X <= (Player.Position.X + Player.Rectangle.Width)
//                && Position.X >= Player.Position.X
//                && (Position.Y + RADIUS) >= Player.Position.Y
//                && (Position.Y + RADIUS) <= Player.Position.Y + Player.Rectangle.Height)
//            {
//                Velocity *= new Vector2(1, -1);
//                return;
//            }
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

            if (!Bounce(gameState.Player1, gameState.Player2))
            {
                BounceCorner(gameState.Player1);
                BounceCorner(gameState.Player2);
            }

            SetOutside();
        }

        private void SetOutside()
        {
            if (Position.X + 2 * RADIUS < 0)
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