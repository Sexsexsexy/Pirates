using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pirates
{
    class Wind
    {
        private Vector2[,] field;
        private int width;
        private int height;

        private Sprite[,] winddebug;
        private bool debugwind = true;

        private Point screensize;

        public Vector2 scale;

        public Wind(Point screensize, Point windsize)
        {
            width = windsize.X;
            height = windsize.Y;

            scale = new Vector2(screensize.X / windsize.X, screensize.Y / windsize.Y);

            field = new Vector2[width, height];
            winddebug = new Sprite[width, height];

            this.screensize = screensize;
        }

        public List<Sprite> Generate()
        {
            List<Sprite> debugs = new List<Sprite>();

            for (float angle = 0; angle < (float)Math.PI * 2; angle += 0.01f)
            {
                for (float r = 0; r < field.GetLength(0); r++)
                {
                    int x = (int)(r * Math.Cos(angle));
                    int y = (int)(r * Math.Sin(angle));
                    Vector2 winddirection = new Vector2(-y, x);

                    if(winddirection != Vector2.Zero)
                        winddirection.Normalize();

                    x += field.GetLength(0) / 2;
                    y += field.GetLength(1) / 2;

                    if (x > 0 && x < field.GetLength(0) &&
                        y > 0 && y < field.GetLength(1))
                    {
                        field[x, y] = winddirection;

                        if (debugwind)
                        {
                            winddebug[x, y] = new Sprite();
                            winddebug[x, y].position = new Vector2(x, y) * screensize.X / field.GetLength(0) - new Vector2(screensize.X, screensize.Y) / 2;
                            winddebug[x, y].width = 1;
                            winddebug[x, y].height = 10;
                            winddebug[x, y].rotation = angle + (float)Math.PI;

                            debugs.Add(winddebug[x, y]);
                        }

                    }
                }
            }

            return debugs;

        }

        public Vector2 getWind(int x, int y)
        {
            x += width / 2;
            y += height / 2;

            if (!(x > 0 && x < field.GetLength(0) - 1
                && y > 0 && y < field.GetLength(1) - 1))
                return Vector2.Zero;

            return field[x, y];
        }
    }
}
