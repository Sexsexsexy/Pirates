using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates
{
    class Projectile : PhysObject
    {
        public bool alive;
        public Ripples water;

        private Timer lifetimer;

        public Projectile(Ripples water)
            :base(8,8, 1)
        {
            texturename = "cannonball";
            width = 8;
            height = 8;

            alive = false;

            this.water = water;

            lifetimer = new Timer(40);
        }

        public override void Update(GameTime gt)
        {
            lifetimer.Update(gt);


            if (alive)
                base.Update(gt);

            if (lifetimer.finished)
                die();

        }

        public override void Draw(SpriteBatch sb)
        {
            if(alive)
                base.Draw(sb);
        }

        public void respawn(Vector2 startforce, Vector2 pos)
        {
            lifetimer.start();
            alive = true;
            body.clear();
            setPos(pos);
            body.applyForce(startforce);
        }

        private void die()
        {
            alive = false;
            water.ripple(2, (int)position.X, (int)position.Y);
        }
    }
}
