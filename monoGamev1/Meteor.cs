using Microsoft.Xna.Framework;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monoGamev1
{
    class Meteor
    {
        private int nrKlatki = 0;
        private int ileCykli = 0;
        private Rectangle klatka;
        private readonly Microsoft.Xna.Framework.Graphics.Texture2D texture;
        private Vector2 position, predkosc;
        private readonly int szerokoscKlatki;

        Random generujLL = new Random();
        public Meteor(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            this.texture = texture;
            position = new Vector2(generujLL.Next(100, 400), 0);
            szerokoscKlatki = texture.Width / 3;
            predkosc.X = generujLL.Next(-13, 13);
            predkosc.Y = generujLL.Next(1, 13);

        }

        public void Draw(Microsoft.Xna.Framework.Graphics.Texture2D texture, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
            predkosc.X = generujLL.Next(-13, 13);
            predkosc.Y = generujLL.Next(1, 13);

            Rectangle rectMeteor = new Rectangle(
                        (int)GetPosition().X,
                        (int)GetPosition().Y,
                        texture.Width,
                        texture.Height);

            
            klatka = new Rectangle(nrKlatki * szerokoscKlatki, 0, szerokoscKlatki, texture.Height);
            //nrKlatki++;
            ileCykli++;
            rectMeteor = new Rectangle((int)position.X, (int)position.Y, klatka.Width, klatka.Height);

            spriteBatch.Draw(texture, rectMeteor, klatka, Color.White);
            if (nrKlatki == 3)
                nrKlatki = 0;
        }

        public void Update()
        {
            if (ileCykli == 8)
            {
                nrKlatki++;
                ileCykli = 0;
            }
            
            //position.X += predkosc.X;
            position.Y += predkosc.Y;
            Debug.WriteLine("position (X,Y): " + position.X +","+position.Y);
            if (position.Y > 580)
            {
                startuj();
            }
        }
        public void startuj()
        {
            //position = new Vector2(generujLL.Next(200, 300), 0);
            position.X = generujLL.Next(0,450);
            position.Y = generujLL.Next(-100,0);
            predkosc.X = 0;
            predkosc.Y = 0;
        }
        public Rectangle Kolizja(Rakieta gracz)
        {
            Rectangle graczRectangle = new Rectangle(
                        (int)gracz.GetPosition().X,
                        (int)gracz.GetPosition().Y,
                        (int)gracz.GetSize().X,
                        (int)gracz.GetSize().Y
                        );
            return graczRectangle;
        }
        public Vector2 GetSize()
        {
            return new Vector2(texture.Width / 6, texture.Height);
        }


        public Vector2 GetPosition()
        {
            return position;
        }//zrobione do 21 pkt
    }
}
