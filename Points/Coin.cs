using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace HorseeCoins
{
    public class Coin
    {

        private Texture2D _coin;
        private Vector2 _position;


        public bool _isCollected { get; set; }


        private List<Coin> _coins = new List<Coin>();


        private Rectangle[] _coinAnimation;
        private int _index;
        private double _time;


        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, 32, 32);
       
        public Coin(Texture2D coin, Vector2 position)
        {
            _coin = coin;
            _position = position;
            _isCollected = false;
        }
        
        public bool CheckCollision(Rectangle playerBounds)
        {
            return Bounds.Intersects(playerBounds);
        }

        public void Initialize()
        {

            _coinAnimation = new Rectangle[]
            {
               new Rectangle(0, 0, 30, 30), new Rectangle(30, 0, 30,30), new Rectangle(60, 0, 30, 30),
               new Rectangle(90, 0, 30, 30), new Rectangle(120, 0, 30, 30), new Rectangle(150, 0, 30, 30),
               new Rectangle(180, 0, 30, 30), new Rectangle(210, 0, 30, 30), new Rectangle(240, 0, 30, 30),
                new Rectangle(270, 0, 30, 30), new Rectangle(300, 0, 30, 30), new Rectangle(330, 0, 30, 30),
                 new Rectangle(360, 0, 30, 30)
            };
            _index = 0;
            _time = 0.0;
        }

        public void Update(float totalSeconds, GameTime gameTime)
        {
            _time += gameTime.ElapsedGameTime.TotalSeconds;
            if (_time > 0.1)
            {
                _time = 0.0;
                _index++;
                if (_index > 6)
                {
                    _index = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_coin, _position, _coinAnimation[_index], Color.White);
        }

    }
}
