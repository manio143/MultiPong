using Microsoft.Xna.Framework;
using System.IO;

namespace MultiPongCommon
{
    public static class Extensions
    {
        public static byte[] GetBytes(this Vector2 vec)
        {
            byte[] buffer;

            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(vec.X);
                    bw.Write(vec.Y);
                }

                buffer = ms.ToArray();
            }

            return buffer;
        }
    }
}
