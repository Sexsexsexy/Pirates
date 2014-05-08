using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates
{
    class Sprite : ScreenAsset
    {
        protected Texture2D texture;
        public string texturename { set { texture = Data.GetTexture(value); width = _width; height = _height; } }
        public virtual int width { set { scale = new Vector2((float)value / spritewidth, scale.Y); _width = value; } get { return _width; } }
        public virtual int height { set { scale = new Vector2(scale.X, (float)value / spriteheight); _height = value; } get { return _height; } }

        public Vector2 origin { get { return new Vector2(spritewidth, spriteheight) / 2; } }
        public Vector2 originoffset;
        public Vector2 drawOffset;

        public Rectangle ?source;
        private int spritewidth
        {
            get
            {
                if (source == null)
                {
                    return texture.Width;
                }
                else
                {
                    return source.Value.Width;
                }
            }
        }
        private int spriteheight
        {
            get
            {
                if (source == null)
                {
                    return texture.Height;
                }
                else
                {
                    return source.Value.Height;
                }
            }
        }

        private int _width;
        private int _height;

        public Sprite()
            :base()
        {
            texturename = "pixel";
            source = null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position + drawOffset, source, color, rotation, origin + originoffset, scale, effects, depth);
        }
    }
}
