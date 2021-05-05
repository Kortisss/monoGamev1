using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;

namespace monoGamev1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;

        private Rakieta gracz;
        private Texture2D control, rakieta, tlo;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            rakieta = Content.Load<Texture2D>("rocket1");
            control = Content.Load<Texture2D>("control");
            tlo = Content.Load<Texture2D>("niebo");

            gracz = new Rakieta(rakieta);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gracz.MoveU();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(tlo, new Rectangle(0,0,480,800), Color.White);
            spriteBatch.Draw(control, new Vector2(0, 583), Color.White);
            gracz.Draw(rakieta, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
