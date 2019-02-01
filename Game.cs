using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Squared.Render;
using Squared.Render.Convenience;

namespace FNATest {
    public class TestGame : MultithreadedGame {
        public GraphicsDeviceManager Graphics;
        public DefaultMaterialSet Materials;
        public Texture2D Texture;

        public TestGame () {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void LoadContent () {
            base.LoadContent();

            Texture = Texture2D.FromStream(Graphics.GraphicsDevice, File.OpenRead(@"E:\Documents\Projects\FNATest\bunny.jpeg"));

            Materials = new DefaultMaterialSet(RenderCoordinator) {
                ViewTransform = ViewTransform.CreateOrthographic(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight)
            };
        }

        public override void Draw (GameTime gameTime, Frame frame) {
            var ir = new ImperativeRenderer(frame, Materials);
            ir.Clear(color: Color.SteelBlue);
            const float scale = 0.65f;
            var pos = new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight) / 2f;
            ir.Draw(
                Texture, pos, origin: Vector2.One * 0.5f, scale: Vector2.One * scale,
                blendState: BlendState.Opaque,
                samplerState: SamplerState.LinearClamp
            );
        }
    }
}
