using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates
{
    class PiratesWorld:GameScreen
    {
        private Point watersize;
        private float waterscale;
        private Matrix waterscalematrix;
        private Ripples water;

        private Wind wind;
        private Point defsize;

        private Player ship;

        public PiratesWorld(GraphicsDevice gd, Point defsize, Point size)
            : base(defsize, size)
        {
            this.defsize = defsize;
            waterscale = 4;
            watersize = new Point((int)(defsize.X / waterscale), (int)(defsize.Y / waterscale));
            waterscalematrix = resmatrix * Matrix.CreateScale(waterscale);
            water = new Ripples(gd, watersize, defsize, 0.98f);

            wind = new Wind(defsize, new Point(defsize.X / 40, defsize.Y / 40));
            List<Sprite> sprites = wind.Generate();
            for (int i = 0; i < sprites.Count; i++)
                addAsset(sprites[i]);

            ship = new Player(water, wind);

            addAsset(ship);
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, waterscalematrix);

            water.Draw(sb);

            sb.End();

            base.Draw(sb);
        }
    }
}
