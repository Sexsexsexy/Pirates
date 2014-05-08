using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pirates
{
    public static class Data
    {
        static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();

        public static void LoadAllFiles(ContentManager content)
        {
            string[] files = Directory.GetFiles(content.RootDirectory, "*.xnb");
            LoadFilesFromArray(files, content);

            files = Directory.GetFiles(Path.Combine(content.RootDirectory, "Textures"), "*.xnb", SearchOption.AllDirectories);
            LoadFilesFromArray(files, content);

            files = Directory.GetFiles(Path.Combine(content.RootDirectory, "Fonts"), "*.xnb", SearchOption.AllDirectories);
            LoadFilesFromArray(files, content);

            //files = Directory.GetFiles(Path.Combine(content.RootDirectory, "Sounds"), "*.xnb", SearchOption.AllDirectories);
            //LoadFilesFromArray(files, content);
        }

        private static void LoadFilesFromArray(string[] files, ContentManager content)
        {
            foreach (string file in files)
            {
                string relativePath = file.Substring(file.IndexOf(@"Content\") + @"Content\".Length);
                relativePath = relativePath.Remove(relativePath.LastIndexOf(".xnb"), ".xnb".Length);

                object o = content.Load<object>(relativePath);

                if (o is Texture2D)
                    Textures.Add(relativePath.Substring(relativePath.LastIndexOf('\\') + 1).ToLower(), o as Texture2D);
                if (o is SpriteFont)
                    Fonts.Add(relativePath.Substring(relativePath.LastIndexOf('\\') + 1).ToLower(), o as SpriteFont);
                if (o is SoundEffect)
                    Sounds.Add(relativePath.Substring(relativePath.LastIndexOf('\\') + 1).ToLower(), o as SoundEffect);
               
            }
        }

        public static Texture2D GetTexture(string texture)
        {
            try { return Textures[texture.ToLower()]; }
            catch
            {
                if (Textures.Count > 0)
                    return Textures["default"];
            }
            return null;
        }

        public static SpriteFont GetFont(string font)
        {
            try { return Fonts[font.ToLower()]; }
            catch
            {
                if (Fonts.Count > 0)
                    return Fonts["default"];
            }
            return null;
        }

        public static SoundEffect GetSound(string effect)
        {
            try { return Sounds[effect.ToLower()]; }
            catch
            {
                if (Sounds.Count > 0)
                    return Sounds["default"];
            }
            return null;
        }
    }
}
