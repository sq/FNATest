using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Squared.Render;
using Squared.Render.Convenience;
using Squared.Render.Text;

namespace FNATest {
    public class TestGame : MultithreadedGame {
        public GraphicsDeviceManager Graphics;
        public DefaultMaterialSet Materials;
        public Texture2D Texture;
#if !X64
        public FreeTypeFont Font;
#endif

        public EmbeddedEffectProvider EffectProvider;
        public Material Vpos;

        public TestGame () {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreparingDeviceSettings += (s, e) => {
                e.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.HiDef;
            };
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;

#if XNA
            Window.Title = "FNATest (XNA)";
#else
            Window.Title = "FNATest (FNA)";
#endif
        }

        protected override void LoadContent () {
            base.LoadContent();

#if !X64
            var provider = new EmbeddedFreeTypeFontProvider(RenderCoordinator);
            Font = provider.Load("FiraSans-Medium");
            Font.SizePoints = 20;
            Font.GlyphMargin = 4;
#endif

            Texture = Texture2D.FromStream(Graphics.GraphicsDevice, Assembly.GetExecutingAssembly().GetManifestResourceStream("bunny"));

            try {
                Materials = new DefaultMaterialSet(RenderCoordinator) {
                    ViewTransform = ViewTransform.CreateOrthographic(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight)
                };
            } catch (Exception exc) {
                Console.WriteLine("Shader load failed.");
                Console.WriteLine(exc);
                Thread.Sleep(5000);
                Environment.Exit(1);
                return;
            }

            EffectProvider = new EmbeddedEffectProvider(RenderCoordinator);
            Vpos = new Material(EffectProvider.Load("vpos"), "vpos");
            Materials.Add(Vpos);
        }

        public override void Draw (GameTime gameTime, Frame frame) {
            var ir = new ImperativeRenderer(frame, Materials) {
                AutoIncrementLayer = true
            };
            ir.Clear(color: Color.SteelBlue);
            const float scale = 0.65f;
            var pos = new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight) / 2f;
            ir.Draw(
                Texture, pos, origin: Vector2.One * 0.5f, scale: Vector2.One * scale,
                blendState: BlendState.Opaque,
                samplerState: SamplerState.LinearClamp
            );

#if !X64
            ir.DrawString(
                Font, "Hello, World!", Vector2.Zero, 
                blendState: BlendState.AlphaBlend,
                material: Materials.ScreenSpaceShadowedBitmap
            );
#endif

            var sg = ir.MakeSubgroup();
            sg.AutoIncrementLayer = true;
            sg.FillRectangle(new Rectangle(0, 0, 1024, 1024), Color.Black, customMaterial: Vpos);
            sg.SetViewport(null, true);
        }

        protected override void UnloadContent () {
            Environment.Exit(1);
        }
    }
}
