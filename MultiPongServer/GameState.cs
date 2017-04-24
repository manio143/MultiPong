using System;
using Microsoft.Xna.Framework;

namespace MultiPongServer
{
    public class GameState
    {
        public Vector2 BallPosition;
        public Vector2 Player1Position;
        public Vector2 Player2Position;

        public void Update(TimeSpan gameTime)
        {
            //TODO: physics + collisions
        }
    }
}