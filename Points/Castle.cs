using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseeCoins
{
    public class Castle
    {
        //bandeira
        private Texture2D _castre;
        private Rectangle _castlePosition;


        public Rectangle castleBounds => new Rectangle((int)_castlePosition.X, (int)_castlePosition.Y, 48, 48);

        public void LoadContent(ContentManager content)
        {
           
            _castre = content.Load<Texture2D>("TileMap/casa_castelo");
        }
        public void Initialize()
        {

           

         
           
            
            _castlePosition = new Rectangle(1120,51,146,99);

        }
        public void Update(float totalSeconds, GameTime gameTime)
        {
           
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_castre, _castlePosition, null, Color.White);
        }
    }
}
