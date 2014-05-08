using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pirates
{
    public class Rnd
    {
        private static Random rnd = new Random();
        private static Color c = Color.White;

        public static float ZeroToOne()
        {
            return (float)rnd.NextDouble();
        }

        public static float OneToOne()
        {
            return (float)(rnd.NextDouble() - 0.5f) * 2;
        }

        public static int next(int start, int end)
        {
            return rnd.Next(start, end);
        }

        public static Color color()
        {
            c.R = (byte) next(0, 256);
            c.B = (byte) next(0, 256);
            c.G = (byte) next(0, 256);

            return c;
        }
    }
}
