using System;
using Microsoft.Xna.Framework;
using MultiPongClient;

namespace MultiPongServer
{
    public class GameState
    {
        BallServer DummyBall;
        public Pad Player1, Player2;

        public GameState(Vector2 BallPosition, Vector2 Player1Position, Vector2 Player2Position, Vector2 InitVelocity,
            int ScreenHeight)
        {
            Player1 = new Pad(Player1Position);
            Player2 = new Pad(Player2Position);
            DummyBall = new BallServer(BallPosition, InitVelocity, ScreenHeight);
        }

        public void Update(TimeSpan gameTime)
        {
            DummyBall.Update(gameTime, this);
        }

        public void UpdatePlayers(Vector2 player1, Vector2 player2)
        {
            this.Player1.move(player1);
            this.Player2.move(player2);
        }
    }
}