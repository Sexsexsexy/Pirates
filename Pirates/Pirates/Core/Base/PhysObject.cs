using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pirates
{
    class PhysObject : WorldObject
    {
        protected Body body;
        private float physcale;

        public PhysObject(int width, int height, float density)
        {
            physcale = 4;

            body = new Body(width / physcale, height / physcale, density);
        }

        public override void Update(GameTime gt)
        {
            body.Update();

            position = body.position * physcale;
            rotation = body.rotation;

            base.Update(gt);
        }

        public void setPos(Vector2 pos)
        {
            position = pos;
            body.position = position / physcale;
        }
    }
}
