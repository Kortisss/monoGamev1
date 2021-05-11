using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SharpDX.Direct2D1;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace monoGamev1
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;

        private Rakieta gracz;
        private Meteor meteor1, meteor2;
        private Texture2D control, teksturaRakiety, tlo, teksturaMeteor;

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

            teksturaRakiety = Content.Load<Texture2D>("AnimRakiety");
            teksturaMeteor = Content.Load<Texture2D>("meteor");
            control = Content.Load<Texture2D>("control");
            tlo = Content.Load<Texture2D>("niebo");

            gracz = new Rakieta(teksturaRakiety);
            meteor1 = new Meteor(teksturaMeteor);
            meteor2 = new Meteor(teksturaMeteor);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
                Exit();
            if (state.IsKeyDown(Keys.W))
            {
                gracz.MoveU();
            }
            if (state.IsKeyDown(Keys.S))
            {
                gracz.MoveD();
            }
            if (state.IsKeyDown(Keys.A))
            {
                gracz.MoveL();
            }
            if (state.IsKeyDown(Keys.D))
            {
                gracz.MoveR();
            }
            if (state.IsKeyDown(Keys.Space))
            {

            }

            StringBuilder sb = new StringBuilder();
            foreach(var key in state.GetPressedKeys())
                sb.Append("Keys:").Append(key).Append(" pressed ");

            if (sb.Length > 0)
                Debug.WriteLine(sb.ToString());
            else
                Debug.WriteLine("No key pressed");



            /*
             TouchCollection mscaDotkniete = TouchPanel.GetState();

            foreach (TouchLocation dotyk in mscaDotkniete)
            {
                Vector2 pozDotyk = dotyk.Position;
                Debug.WriteLine(pozDotyk + "\n");

                Debug.WriteLine(dotyk.State + "\n");

                if (dotyk.State == TouchLocationState.Moved)
                {
                    gracz.MoveU();
                }
                else if (dotyk.State == TouchLocationState.Pressed)
                {
                    Debug.WriteLine("strzał!");
                }
            
            }*/



            //gracz.MoveU();

            meteor1.Update();
            meteor2.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(tlo, new Rectangle(0,0,480,800), Color.White);
            gracz.Draw(teksturaRakiety, spriteBatch);
            meteor1.Draw(teksturaMeteor, spriteBatch);
            meteor2.Draw(teksturaMeteor, spriteBatch);

            spriteBatch.Draw(control, new Vector2(0, 583), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
