using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DudesInSpace
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static readonly Random RNG = new Random();

        MouseState ms, ms_old;

        private List<Color> paletteList;

        Graphic2D background;
        List<SpaceDude> Dudes;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Dudes = new List<SpaceDude>();

            #region Generate some visually distinct colours for the dudes.
            paletteList = new List<Color>();
            paletteList.Add(new Color(230, 25, 75, 255)); // Red
            paletteList.Add(new Color(60, 180, 75, 255)); // Green
            paletteList.Add(new Color(255, 225, 25, 255));    // Yellow
            paletteList.Add(new Color(0, 130, 200, 255)); // Blue
            paletteList.Add(new Color(245, 130, 48, 255));    // Orange
            paletteList.Add(new Color(145, 30, 180, 255)); // Purple
            paletteList.Add(new Color(70, 240, 240, 255));    // Cyan
            paletteList.Add(new Color(240, 50, 230, 255)); // Magenta
            paletteList.Add(new Color(210, 245, 60, 255));    // Lime
            paletteList.Add(new Color(250, 190, 190, 255));   // Pink
            paletteList.Add(new Color(0, 128, 128, 255)); // Teal
            paletteList.Add(new Color(230, 190, 255, 255));   // Lavender
            paletteList.Add(new Color(170, 110, 40, 255));    // Brown
            paletteList.Add(new Color(255, 250, 200, 255));   // Beige
            paletteList.Add(new Color(128, 0, 0, 255));   // Maroon
            paletteList.Add(new Color(170, 255, 195, 255));   // Mint
            paletteList.Add(new Color(128, 128, 0, 255)); // Olive
            paletteList.Add(new Color(255, 215, 180, 255));   // Apricot
            paletteList.Add(new Color(0, 0, 128, 255));   // Navy
            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = new Graphic2D(Content.Load<Texture2D>("starfield"));

        }

        protected override void Update(GameTime gameTime)
        {
            Vector2 randomSpeed;

            ms = Mouse.GetState();

            if (ms.LeftButton == ButtonState.Pressed && ms_old.LeftButton == ButtonState.Released)
            {
                randomSpeed = new Vector2((float)RNG.NextDouble() - 0.5f, (float)RNG.NextDouble() - 0.5f);
                Dudes.Add(new SpaceDude(Content.Load<Texture2D>("dudeBody"), Content.Load<Texture2D>("dudeVisor"), paletteList[RNG.Next(paletteList.Count)],
                                            400, 240, randomSpeed, 0, randomSpeed.Length() / 10f));
            }

            foreach (var dude in Dudes)
            {
                dude.UpdateMe(GraphicsDevice.Viewport.Bounds);
            }

            ms_old = ms;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            background.DrawMe(_spriteBatch);
            foreach (var dude in Dudes)
            {
                dude.DrawMe(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
