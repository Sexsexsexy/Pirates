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

        private float rotationspeed;
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
            waterfriction = 0.99f;
            inertia = 0.8f;
            originoffset = new Vector2(0, -height / 4);
        }

        public override void Update(GameTime gt)
        {
            HandleInput();

            speed *= waterfriction;
            rotationspeed *= inertia;

            rotation += rotationspeed;
            position += velocity;

            base.Update(gt);
        }

        private void HandleInput()
        {
             if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
             {
                 if(speed < maxspeed)
                    speed += accel;
             }

             heading = new Vector2((float)-Math.Sin(rotation), (float)Math.Cos(rotation)) * speed;
             velocity = heading;

             if (velocity != Vector2.Zero)
             {
                 if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
                 {
                     rotationspeed = -0.01f * speed;
                 }
                 if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
                 {
                     rotationspeed = 0.01f * speed;
                 }
             }
        }
    }
}
