using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FNATest {
    public class TestGame : Game {
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SB;
        public Texture2D Texture;

        public TestGame () {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void LoadContent () {
            base.LoadContent();

            SB = new SpriteBatch(Graphics.GraphicsDevice);
            Texture = Texture2D.FromStream(Graphics.GraphicsDevice, File.OpenRead("bunny.jpeg"));
        }

        protected override void Draw (GameTime gameTime) {
            base.Draw(gameTime);

            Graphics.GraphicsDevice.Clear(Color.SteelBlue);

            SB.Begin();
            const float scale = 0.65f;
            var pos = (new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight) - (new Vector2(Texture.Width, Texture.Height) * scale)) / 2f;
            SB.Draw(Texture, pos, null, Color.White, 0f, Vector2.One * 0.5f, scale, SpriteEffects.None, 0f);
            SB.End();
        }
    }
}
