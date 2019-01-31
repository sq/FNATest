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
            SB.Draw(Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 0.33f, SpriteEffects.None, 0f);
            SB.End();
        }
    }
}
