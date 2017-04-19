using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MultiPongClient
{
    public class PongGame : Game
    {
        GraphicsDeviceManager gdm;
        SpriteBatch spriteBatch;

        Ball ball;

        Pad player1, player2;

        public PongGame()
        {
            gdm = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
            spriteBatch = new SpriteBatch(gdm.GraphicsDevice);
            ball = new Ball(createCircleText(Ball.RADIUS), new Vector2(200, 150), new Vector2(1, 2),
                gdm.GraphicsDevice.Viewport.Height);

            player1 = new Pad(createRectangle(20, 180), new Vector2(10, 100));
            player2 = new Pad(createRectangle(20, 180), new Vector2(gdm.GraphicsDevice.Viewport.Width - 30, 100));
        }

        protected override void Draw(GameTime gameTime)
        {
            gdm.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            ball.Draw(spriteBatch);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            spriteBatch.End();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ball.Update(gameTime);

            if (ball.Bounds.Intersects(player1.Bounds)
                || ball.Bounds.Intersects(player2.Bounds))
                ball.Bounce(new Vector2(-1, 1));

            var mouseState = Mouse.GetState();
            //TODO: calculate difference in mouseState Y
            //var diff
        }

        public Texture2D createRectangle(int width, int height)
        {
            Texture2D texture = new Texture2D(gdm.GraphicsDevice, width, height);
            Color[] colorData = new Color[width * height];

            for (int i = 0; i < colorData.Length; i++)
                colorData[i] = Color.DarkBlue;

            texture.SetData(colorData);
            return texture;
        }

        public Texture2D createCircleText(int radius)
        {
            Texture2D texture = new Texture2D(gdm.GraphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }
    }
}