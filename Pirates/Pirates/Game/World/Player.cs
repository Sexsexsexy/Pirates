using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pirates
{
    class Player : WorldObject
    {
        public Vector2 velocity;
        public Vector2 heading;
        public float speed;
        private float accel;
        private float maxspeed;

        private float rotationvelocity;
        private float rotationspeed;
        private float rotationaccel;
        private float rotationmaxspeed;

        private float waterfriction;
        private float inertia;

        public Player()
            : base()
        {
            texturename = "pirateship";
            width = 48;
            height = 64;

            speed = 0;
            accel = 0.01f;
            maxspeed = 1;

            rotationspeed = 0;
            rotationaccel = 0.0001f;
            rotationmaxspeed = 0.01f;

            waterfriction = 0.99f;
            inertia = 0.99f;
            originoffset = new Vector2(0, -height / 4);
        }

        public override void Update(GameTime gt)
        {
            HandleInput();

            speed *= waterfriction;
            rotationspeed *= inertia;

            rotation += rotationvelocity;
            position += velocity;

            base.Update(gt);
        }

        private void HandleInput()
        {
             if ((Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up)) && speed < maxspeed)
                speed += accel;

             heading = new Vector2((float)-Math.Sin(rotation), (float)Math.Cos(rotation));
             velocity = heading * speed;

            if ((Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left)) && rotationspeed > -rotationmaxspeed)
                rotationspeed -= rotationaccel;
            if ((Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right)) && rotationspeed < rotationmaxspeed)
                rotationspeed += rotationaccel;

             rotationvelocity = rotationspeed * speed;
        }
    }
}
