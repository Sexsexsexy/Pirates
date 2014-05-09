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

        private float rudderlength;

        private Ripples water;
        private Wind wind;

        private Projectile p;

        public Player(Ripples water, Wind wind)
            : base(48, 64, 5)
        {
            texturename = "pirateship";
            width = 48;
            height = 64;

            setPos(new Vector2(0, -100));

            originoffset = new Vector2(0, -height / 4);

            rudderlength = 10;

            this.water = water;
            this.wind = wind;

            p = new Projectile(water);
        }

        public override void Update(GameTime gt)
        {
            heading = new Vector2((float)-Math.Sin(body.rotation), (float)Math.Cos(body.rotation));

            HandleInput();

            if (body.velocity == Vector2.Zero)
                body.applyForce(heading * 10);

            Vector2 winddir = wind.getWind((int)(position.X / wind.scale.X), (int)(position.Y / wind.scale.Y));

            body.applyForce(winddir / 10f);

            Vector2 perp = new Vector2(-heading.Y, heading.X) * (body.velocity.X * -heading.Y + body.velocity.Y * heading.X);
            body.applyForce(-perp * body.mass );

            p.Update(gt);

            base.Update(gt);

            Vector2 ripplepos = position + new Vector2(body.velocity.X * width, body.velocity.Y * height);
            water.ripple(body.velocity.LengthSquared() + 0.4f, (int)(ripplepos.X / water.scale.X), (int)(ripplepos.Y / water.scale.Y));
            water.ripple(body.velocity.LengthSquared() + 0.4f, (int)(ripplepos.X / water.scale.X + 1), (int)(ripplepos.Y / water.scale.Y));
            water.ripple(body.velocity.LengthSquared() + 0.4f, (int)(ripplepos.X / water.scale.X), (int)(ripplepos.Y / water.scale.Y + 1));
            water.ripple(body.velocity.LengthSquared() + 0.4f, (int)(ripplepos.X / water.scale.X + 1), (int)(ripplepos.Y / water.scale.Y + 1));
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            p.Draw(sb);
        }

        private void HandleInput()
        {
            float apprvel = Math.Abs(body.velocity.X) + Math.Abs(body.velocity.Y);
            float torque = body.velocity.Length() * 7f;

            if ((Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left)))
                body.applyTorque(-torque);

            if ((Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right)))
                body.applyTorque(torque);

            if (Input.WasKeyPressed(Keys.Space))
                p.respawn(body.velocity + new Vector2(heading.Y, -heading.X) / 2, position);
        }
    }
}
