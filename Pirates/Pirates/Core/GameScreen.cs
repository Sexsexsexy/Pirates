using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates
{
    abstract class GameScreen : IUpdateable, IDrawable
    {
        private List<IUpdateable> updateables;
        private List<IDrawable> drawables;
        protected SimpleCamera2D camera;
        protected Sprite background;
        protected Matrix resmatrix;
        protected Vector2 resscale;

        public GameScreen(Point defaultsize, Point screensize)
        {
            resscale = new Vector2((float)screensize.X / defaultsize.X, (float)screensize.Y / defaultsize.Y);
            resmatrix = Matrix.CreateScale(resscale.X, resscale.Y,  1);
            updateables = new List<IUpdateable>();
            drawables = new List<IDrawable>();

            camera = new SimpleCamera2D(screensize, defaultsize);
            background = new Sprite();
            background.width = defaultsize.X;
            background.height = defaultsize.Y;
            background.originoffset = new Vector2(-defaultsize.X, -defaultsize.Y) / 2;
        }

        public virtual void Update(GameTime gt)
        {
            //camera.UpdateControls(true, true, false);
            for (int i = 0; i < updateables.Count; i++)
                updateables[i].Update(gt);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, resmatrix);
            background.Draw(sb);
            sb.End();

            sb.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, camera.GetView());

            for (int i = 0; i < drawables.Count; i++)
                drawables[i].Draw(sb);

            sb.End();
        }

        protected virtual void removeAsset(ScreenAsset asset)
        {
            if (asset is IUpdateable)
                updateables.Remove(asset as IUpdateable);

            drawables.Remove(asset);
        }

        protected virtual void addAsset(ScreenAsset asset)
        {
            if (asset is IUpdateable)
                updateables.Add(asset as IUpdateable);

            drawables.Add(asset);
        }

        protected virtual void ClearAll()
        {
            updateables.Clear();
            drawables.Clear();
        }
    }
}
