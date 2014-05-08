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

        private Point defsize;

        private Player ship;

        public PiratesWorld(GraphicsDevice gd, Point defsize, Point size)
            : base(defsize, size)
        {
            this.defsize = defsize;
            watersize = new Point(defsize.X / 2, defsize.Y/2);
            waterscale = 2;
            waterscalematrix = resmatrix * Matrix.CreateScale(waterscale);
            water = new Ripples(gd, watersize, 0.94f);
            ship = new Player();

            addAsset(ship);
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            Vector2 ripplepos = ship.position + new Vector2(ship.heading.X * ship.width, ship.heading.Y * ship.height) / 2 + new Vector2(defsize.X / 2, defsize.Y / 2);
            water.ripple(ship.speed * 2, (int)(ripplepos.X) / 2, (int)(ripplepos.Y) / 2);

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
