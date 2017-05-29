using System;
using Microsoft.Xna.Framework;
using MultiPongCommon;

namespace MultiPongServer
{
    public class GameState
    {
        public BallServer CurrentBall;
        public BallServer StartingBall;
        public Pad Player1, Player2;

        public byte Players { get; set; } = 0;

        public byte Player1Score { get; private set; }
        public byte Player2Score { get; private set; }        

        public GameState(Vector2 BallPosition, Vector2 Player1Position, Vector2 Player2Position, Vector2 InitVelocity,
            int ScreenHeight)
        {
            Player1 = new Pad(Constants.PLAYER1_INITIAL_RECTANGLE, Player1Position);
            Player2 = new Pad(Constants.PLAYER2_INITIAL_RECTANGLE, Player2Position);
            StartingBall = CurrentBall = new BallServer(BallPosition, InitVelocity, ScreenHeight);
        }

        public void Update(TimeSpan gameTime)
        {
            CurrentBall.Update(gameTime, this);
            var outside = CurrentBall.Outside;
            if (outside != Outside.None)
            {
                if (outside == Outside.Left)
                    Player2Score++;
                else if (outside == Outside.Right)
                    Player1Score++;

                CurrentBall = StartingBall;
            }
        }

        public bool CanAddNewPlayer()
        {
            return Players < 2;
        }
    }
}