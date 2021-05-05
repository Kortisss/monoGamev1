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
        private readonly Microsoft.Xna.Framework.Graphics.Texture2D texture;
        private Vector2 position;

        public Rakieta(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            position = new Vector2(210, 480);
            this.texture = texture;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.Texture2D rakieta, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Rectangle rectGracza = new Rectangle(
                        (int)GetPosition().X,
                        (int)GetPosition().Y,
                        rakieta.Width,
                        rakieta.Height);

            spriteBatch.Draw(rakieta, rectGracza, Color.White);


        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void MoveL()
        {
            if (0 <= position.X || position.X <= 400)
            {
                position.X -= 5;
            }
        }
        public void MoveR()
        {
            if (0 <= position.X || position.X <= 400)
            {
                position.X += 5;
            }
        }
        public void MoveU()
        {
            if (0 <= position.Y || position.Y <= 477)
            {
                position.Y -= 5;
            }
            
        }
        public void MoveD()
        {
            if (0 <= position.Y || position.Y <= 477)
            {
                position.Y += 5;
            }
        }
    }
}
