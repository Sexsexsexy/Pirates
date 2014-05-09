using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates
{
    class Player : PhysObject
    {
        public Vector2 heading;
        //public float speed;
        //private float accel;
        //private float maxspeed;

        //private float rotationvelocity;
        //private float rotationspeed;
        //private float rotationaccel;
        //private float rotationmaxspeed;

        //private float waterfriction;
        //private float inertia;

        //private float rudderlength;

        private float windscale;

        private Ripples water;
        private Wind wind;

        private Projectile p;

        public Player(Ripples water, Wind wind)
            : base(48, 64, 5)
        {
            texturename = "pirateship";
            width = 48;
            height = 64;

            setPos(new Vector2(100));

            //windscale = defs

            //speed = 0;
            //accel = 0.1f;
            //maxspeed = 1f;

            //rotationspeed = 0;
            //rotationaccel = 100f;
            //rotationmaxspeed = 1000f;

            //waterfriction = 0.99f;
            //inertia = 0.99f;
            originoffset = new Vector2(0, -height / 4);

            //rudderlength = 10;

            this.water = water;
            this.wind = wind;

            p = new Projectile(water);
        }

        public override void Update(GameTime gt)
        {
            heading = new Vector2((float)-Math.Sin(body.rotation), (float)Math.Cos(body.rotation));

            //if(body.velocity == Vector2.Zero)
            //    body.applyForce(heading);

            body.applyForce(wind.getWind((int) (position.X / wind.scale.X), (int)(position.Y / wind.scale.Y)));

            Vector2 ripplepos = position + new Vector2(heading.X * width, heading.Y * height) / water.scale.X;
            water.ripple(body.velocity.LengthSquared(), (int)(ripplepos.X) / 2, (int)(ripplepos.Y) / 2);

            p.Update(gt);

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            p.Draw(sb);
        }

        private void HandleInput()
        {
          
            //if ((Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up)) && speed < maxspeed)
            //{
            //    speed += accel;
            //    body.applyForce(heading * speed);
            //}

             //if ((Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left)) && rotationspeed > -rotationmaxspeed)
             //{
             //    body.applyTorque(-body.velocity.LengthSquared() * rudderlength);
             //}
             //if ((Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right)) && rotationspeed < rotationmaxspeed)
             //{
             //    body.applyTorque(body.velocity.LengthSquared() * rudderlength);
             //}

             //if (Input.WasKeyPressed(Keys.Space))
             //{
             //    p.respawn(body.velocity + new Vector2(heading.Y,-heading.X) * 2, position);
             //}
        }
    }
}
