using Microsoft.Xna.Framework;
using MultiPongCommon;

namespace MultiPongClient
{
    public abstract class RenderedObject
    {
        public Vector2 Position { get; protected set; }
        protected Vector2 Screen;

        protected RenderedObject(Vector2 screen)
        {
            Screen = screen;
        }
        
        public void Move(Vector2 position)
        {
            Position = position;
        }
    }
}