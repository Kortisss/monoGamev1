using Microsoft.Xna.Framework;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monoGamev1
{
    class Rakieta
    {
        private int nrKlatki = 0;
        private Rectangle klatka;
        private readonly Microsoft.Xna.Framework.Graphics.Texture2D texture;
        private Vector2 position;
        private readonly int szerokoscKlatki;

        public Rakieta(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            position = new Vector2(210, 480);
            this.texture = texture;

            szerokoscKlatki = texture.Width / 6;
            klatka = new Rectangle(0 * szerokoscKlatki, 0, szerokoscKlatki, texture.Height);

        }

        public void Draw(Microsoft.Xna.Framework.Graphics.Texture2D texture, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Rectangle rectGracza = new Rectangle(
                        (int)GetPosition().X,
                        (int)GetPosition().Y,
                        texture.Width,
                        texture.Height);

            nrKlatki++;
            klatka = new Rectangle(nrKlatki * szerokoscKlatki, 0, szerokoscKlatki, texture.Height);
            nrKlatki++;
            rectGracza = new Rectangle((int)position.X, (int)position.Y, klatka.Width, klatka.Height);

            spriteBatch.Draw(texture, rectGracza, klatka, Color.White);
            if (nrKlatki == 6)
                nrKlatki = 0;
        }

        public Vector2 GetSize()
        {
            return new Vector2(texture.Width / 6, texture.Height);
        }

        public Rectangle Kolizja(Meteor meteor)
        {
            Rectangle meteorRectangle = new Rectangle(
                        (int)meteor.GetPosition().X,
                        (int)meteor.GetPosition().Y,
                        (int)meteor.GetSize().X,
                        (int)meteor.GetSize().Y
                        );
            return meteorRectangle;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void MoveL()
        {
            position.X -= 5;
            if (position.X <= 0)
                position.X = 0;
        }
        public void MoveR()
        {
            position.X += 5;
            if (position.X >= 405)
                position.X = 405;
        }
        public void MoveU()
        {
            position.Y -= 5;
            if (position.Y <= 0)
                position.Y = 0;

        }
        public void MoveD()
        {
            position.Y += 5;
            if (position.Y >= 480)
                position.Y = 480;
        }
    }
}
