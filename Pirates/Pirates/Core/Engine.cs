using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pirates
{
    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sb;
        public static bool Active;

        PiratesWorld world;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            Input.Initialize();
            Data.LoadAllFiles(Content);

            sb = new SpriteBatch(GraphicsDevice);
            Point size = new Point(1280, 720);
            Point defsize = new Point(1280, 720);

            graphics.PreferredBackBufferHeight = size.Y;
            graphics.PreferredBackBufferWidth = size.X;
            graphics.ApplyChanges();

            world = new PiratesWorld(GraphicsDevice, defsize, size);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            world.Draw(sb);

            base.Draw(gameTime);
        }
        
        protected override void OnActivated(object sender, EventArgs args)
        {
            Engine.Active = true;
            base.OnActivated(sender, args);
        }
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            Engine.Active = false;

            base.OnDeactivated(sender, args);
        }

    }
}
