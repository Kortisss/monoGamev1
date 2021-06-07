using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SharpDX.Direct2D1;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.UI.ViewManagement;
using Microsoft.Xna.Framework.Audio;

namespace monoGamev1
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;

        public static bool paused = false;

        private SoundEffectInstance wybuchRaz;
        private SoundEffect wybuch;
        private Rakieta gracz;
        private Texture2D control, teksturaRakiety, tlo, teksturaMeteor, koniecGry, pociskTekstura;
        SpriteFont font, font2;

        protected KeyboardState previousKey;
        protected KeyboardState currentKey;

    List<Meteor> meteory = new List<Meteor>();
        public int pociskKolizjaLicznik = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 480;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            teksturaRakiety = Content.Load<Texture2D>("AnimRakiety");
            teksturaMeteor = Content.Load<Texture2D>("meteor");
            wybuch = Content.Load<SoundEffect>("wybuch");
            control = Content.Load<Texture2D>("control");
            koniecGry = Content.Load<Texture2D>("End");
            tlo = Content.Load<Texture2D>("niebo");
            pociskTekstura = Content.Load<Texture2D>("pocisk2D");

            font = Content.Load<SpriteFont>("Fonts");
            font2 = Content.Load<SpriteFont>("KontynuujKoniec");

            wybuchRaz = wybuch.CreateInstance();
            wybuchRaz.Volume = (float)0.5;

            for (int i = 0; i < 3; i++)
                meteory.Add(new Meteor(teksturaMeteor));

            gracz = new Rakieta(teksturaRakiety, pociskTekstura);
        }

        protected override void Update(GameTime gameTime)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!paused)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    gracz.MoveU();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    gracz.MoveD();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    gracz.MoveL();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    gracz.MoveR();
                }

                if (currentKey.IsKeyDown(Keys.Space))
                {
                    gracz.Wystrzel();
                    gracz.LotPociskuUpdate();
                }

                StringBuilder sb = new StringBuilder();
                foreach (var key in Keyboard.GetState().GetPressedKeys())
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

                foreach (Meteor meteor in meteory)
                {
                    meteor.Update();
                    if (meteor.Kolizja(gracz)[0].Intersects(gracz.Kolizja(meteor)))
                    {
                        wybuchRaz.Play();

                        paused = !paused;
                        meteor.startuj();
                        for (int i = 0; i < meteory.Count; i++)
                        {
                            meteory[i].startuj();
                        }
                        pociskKolizjaLicznik = 0;
                    }
                    if (meteor.Kolizja(gracz)[1].Intersects(gracz.Kolizja(meteor)))
                    {
                        meteor.startuj();
                        pociskKolizjaLicznik++;
                    }
                }
                
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.P))
            {
                
                wybuchRaz.Stop();
                paused = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(tlo, new Rectangle(0,0,480,800), Color.White);
            gracz.Draw(teksturaRakiety, pociskTekstura, spriteBatch);

            foreach(Meteor meteor in meteory)
            {
                meteor.Draw(teksturaMeteor, spriteBatch);
            }
            if (paused == true)
            {
                spriteBatch.Draw(koniecGry, new Rectangle(0, 0, 490, 590), Color.White);
                spriteBatch.DrawString(font2, "Wcisnij P aby kontynuowac...", new Vector2(50, 140), Color.White);
            }

            spriteBatch.Draw(control, new Vector2(0, 583), Color.White);
            spriteBatch.DrawString(font, pociskKolizjaLicznik.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
