﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pirates
{
    class Ripples
    {
        private Texture2D texture;
        private float[,] Buffer1;
        private float[,] Buffer2;
        private bool[,] collisiongrid;

        private Color[] heightmap;
        private float dampening;

        private int width;
        private int height;

        public Vector2 scale;

        public int frameskip = 2;
        private int frame;

        GraphicsDevice gd;

        public Vector2 pixelOffset = Vector2.One;

        public Ripples(GraphicsDevice gd, Point size, Point screensize, float dampening)
            :base()
        {
            this.gd = gd;
            width = size.X;
            height = size.Y;

            scale = new Vector2(screensize.X / width, screensize.Y / height);

            Buffer1 = new float[width, height];
            Buffer2 = new float[width, height];
            collisiongrid = new bool[width, height];

            heightmap = new Color[size.X * size.Y];

            texture = new Texture2D(gd, width, height, true, SurfaceFormat.Color);

            this.dampening = dampening;
        }

        public void Load(ContentManager c)
        {
            //texture = c.Load<Texture2D>("pixel");
        }

        public void Draw(SpriteBatch sb)
        {
            frame++;

            if (frame % frameskip != 0)
                process();
            else
                frame = 0;

            sb.Draw(texture, -pixelOffset, Color.White);
        }

        private void process()
        {
            gd.Textures[0] = null;

            int width = Buffer1.GetLength(0);
            int height = Buffer1.GetLength(1);

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    Color c = Color.Transparent;

                    if (!collisiongrid[x,y])
                    {
                        float newheight =
                            (Buffer1[x - 1, y]
                            + Buffer1[x + 1, y]
                            + Buffer1[x, y + 1]
                            + Buffer1[x, y - 1]) / 4 - Buffer2[x, y];

                        Buffer2[x, y] = newheight * dampening;


                        float val = Buffer2[x, y];

                        if (val > 0.01f)
                        {
                            c = Color.White;
                            c.A = (byte)(255 * val * 0.5f);
                        }
                        if (val < -0.01f)
                        {
                            c = Color.Black;
                            c.A = (byte)(255 * -val * 0.5f);
                        }
                    }
                    else
                        c = Color.DarkBlue;

                    heightmap[x + y * width] = c;
                }
            }

            texture.SetData<Color>(heightmap);

            float[,] temp = Buffer1;
            Buffer1 = Buffer2;
            Buffer2 = temp;
        }

        public void ripple(float amount, int x, int y)
        {
            x += width / 2;
            y += height / 2;

            if (!(x > 0 && x < Buffer1.GetLength(0) - 1
                && y > 0 && y < Buffer1.GetLength(1) - 1))
                return;

            Buffer2[x, y] = amount;
        }
       
        public void addcoll(int x, int y)
        {
            if (!(x - 1 > 0 && x + 1 < collisiongrid.GetLength(0) - 1
                && y - 1 > 0 && y + 1 < collisiongrid.GetLength(1) - 1))
                return;

            //collisiongrid[x, y] = !collisiongrid[x, y];
            collisiongrid[x, y] = true;
        }
    }
}
