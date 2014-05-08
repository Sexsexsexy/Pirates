using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates
{
    abstract class ScreenAsset : IDrawable
    {
        public Vector2 position;
        public Color color;

        public float rotation;
        public float depth;

        public SpriteEffects effects;
        public Vector2 scale;

        public ScreenAsset()
        {
            effects = SpriteEffects.None;
            scale = Vector2.One;
            color = Color.White;

            depth = 0;
        }

        public virtual void Draw(SpriteBatch sb)
        {
        }
    }
}
