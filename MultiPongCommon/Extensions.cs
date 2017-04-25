using Microsoft.Xna.Framework;
using System.IO;

namespace MultiPongCommon
{
    public static class Extensions
    {
        public static byte[] GetBytes(this Vector2 vec)
        {
            byte[] buffer;

            using (var memStream = new MemoryStream())
            {
                using (var binWriter = new BinaryWriter(memStream))
                {
                    binWriter.Write(vec.X);
                    binWriter.Write(vec.Y);
                }

                buffer = memStream.ToArray();
            }

            return buffer;
        }
    }
}
