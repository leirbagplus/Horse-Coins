using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HorseeCoins
{
    public class Credits
    {
        private Game1 _game;

        private SpriteFont _font;
        private string[] _creditsText; 
        private float _creditsTimer = 0f;
        private float _creditsDuration = 5f;

        public Credits(Game1 game)
        {
            _game = game;
        }
        public void Initialize()
        {
            _font = _game.Content.Load<SpriteFont>("asset/Fonte");

            _creditsText = new string[]
            {
             "Integrantes do grupo: ",
            "Gabriel Pacheco Farias- 01427378",
            "George Jose Cesar da Silva- 01430653",
            "Israel Allan Vilela Viegas - 01433868",
            "Marcos Antonio de Ferreira Guimaraes- 01405752",
            "Pedro Augusto Cardoso Silva - 01403875"
            };

        }
        public void Update(float TotalSeconds, GameTime gameTime)
        {
            _creditsTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_creditsTimer >= _creditsDuration)
            {

                _game.ShowMenu();
                _creditsTimer = 0f;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            float yPos = 400;
            foreach (string line in _creditsText)
            {
                spriteBatch.DrawString(_font, line, new Vector2(350, yPos), Color.Black);
                yPos += _font.LineSpacing + 5; 
            }
        }
    }
}
