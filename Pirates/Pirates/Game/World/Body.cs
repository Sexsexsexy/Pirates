using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pirates
{
    class Body
    {
        public Vector2 position;
        public Vector2 velocity;

        public float rotation;
        public float rotationspeed;

        public float friction;
        public float rotationfriction;

        public float mass { get; private set; }
        public float inertia { get; private set; }

        public float maxspeed;

        public float width;
        public float height;

        public Vector2 center { get { return new Vector2(width, height) / 2; }}

        public Body(float width, float height, float density)
        {
            mass = width * height * density;

            this.width = width;
            this.height = height;

            inertia = mass * (width * width + height * height) / 12;

            maxspeed = 10;
        }

        public void Update()
        {
            position += velocity;
            rotation += rotationspeed;
        }

        public void applyForce(Vector2 force)
        {
            applyForce(force, center);
        }
        public void applyForce(Vector2 force, Vector2 point)
        {
            if(velocity.LengthSquared() < maxspeed)
                velocity += force / mass;

            rotationspeed += ((point.X - center.X) * force.Y - (point.Y - center.Y) * force.X) / inertia;
        }
        public void applyTorque(float torque)
        {
            rotationspeed += torque / inertia;
        }

        public void clear()
        {
            velocity = Vector2.Zero;
            rotationspeed = 0;
        }
    }
}
