﻿using Microsoft.Xna.Framework;

namespace MultiPongCommon
{
    public static class Constants
    {
        public const int BALL_RADIUS = 50;
        public const int PAD_WIDTH = 20;
        public const int PAD_LENGTH = 180;

        public const int PLAYER_RECTANGLE_WIDTH = 20;
        public const int PLAYER_RECTANGLE_HEIGHT = 180;

        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;

        public static readonly Vector2 BALL_INITIAL_POSITION = new Vector2(200, 150);
        public static readonly Vector2 PLAYER1_INITIAL_POSITION = new Vector2(10, 100);
        public static readonly Vector2 PLAYER2_INITIAL_POSITION = new Vector2(SCREEN_WIDTH - 30, 100);

        public static readonly Vector2 INITIAL_VELOCITY = new Vector2(1, 2);

        public static readonly Rectangle PLAYER1_INITIAL_RECTANGLE = new Rectangle(
            (int)PLAYER1_INITIAL_POSITION.X,
            (int)PLAYER1_INITIAL_POSITION.Y,
            PLAYER_RECTANGLE_WIDTH,
            PLAYER_RECTANGLE_HEIGHT);

        public static readonly Rectangle PLAYER2_INITIAL_RECTANGLE = new Rectangle(
            (int)PLAYER1_INITIAL_POSITION.X,
            (int)PLAYER1_INITIAL_POSITION.Y,
            PLAYER_RECTANGLE_WIDTH,
            PLAYER_RECTANGLE_HEIGHT);

    }
}