using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pirates
{
    public class Timer : IUpdateable
    {
        private int frames;
        private int counter;
        private bool count;

        public bool loop;

        public bool finished { private set; get; }

        public Timer(int tick)
        {
            count = false;
            frames = tick;
        }

        public void Update(GameTime gt)
        {
            if (count && finished)
            {
                finished = false;
                count = false;

                if (loop)
                    start();
            }

            if (count)
                counter++;

            if (count &&  counter >= frames)
                finished = true;
        }

        public void start()
        {
            if (!count)
            {
                count = true;
                counter = 0;
            }
        }

        public bool counting
        {
            get { return count; }
        }

        public float progress
        {
            get { return (float)counter / frames; }
        }
    }
}
