using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HorseeCoins.Engine
{
    public class Player : GameObject
    {
        private const float SPEED_X = 200;
        private const float GRAVITY = 10;
        private float _speedY;
        private const float JUMP_VELOCITY = -5;
        private Rectangle _previousBounds;
        private SpriteEffects _orientation;
        private Rectangle[] _playerWalk;
        private int _index;
        private double _time;


        public Player(Texture2D image) : base(image)
        {
        }

        public override void Initialize()
        {
            _bounds.X = 50;
            _bounds.Y = 550;
            _bounds.Width = 70;
            _bounds.Height = 70;
            _speedY = 0;
            _orientation = SpriteEffects.None;
            _playerWalk = new Rectangle[]
       {
           new Rectangle(0, 0, 80, 69), new Rectangle(80, 0, 80, 69), new Rectangle(160, 0, 80, 69),
           new Rectangle(240, 0, 80, 69), new Rectangle(320, 0, 80, 69), new Rectangle(400, 0, 80, 69),
           new Rectangle(480, 0, 80, 69), new Rectangle(560, 0, 80, 69), new Rectangle(640, 0, 80, 69),
           new Rectangle(720, 0, 80, 69), new Rectangle(800, 0, 80, 69), new Rectangle(880, 0, 80, 69),
            new Rectangle(960, 0, 80, 69), new Rectangle(1040, 0, 80, 69)
       };

            _index = 0;
            _time = 0.0;


        }

        public override void Update(float deltaTime)
        {
            _previousBounds = _bounds;

            float direction = 0;
            if (Input.GetKey(Keys.A))
            {
                direction = -1;
                _orientation = SpriteEffects.None;
            }
            if (Input.GetKey(Keys.D))
            {
                direction = 1;
                _orientation = SpriteEffects.FlipHorizontally;
            }

            if (direction != 0)
            {
                _bounds.X = _bounds.X + (int)(direction * SPEED_X * deltaTime);
            }

            if (Input.GetKey(Keys.Space))
            {
                _speedY = JUMP_VELOCITY;
            }

            _speedY = _speedY + GRAVITY * deltaTime;
            _bounds.Y = _bounds.Y + (int)_speedY;



            _time += deltaTime;
            if (_time > 0.1)
            {
                _time = 0.0;
                _index++;
                if (_index > 10)
                {
                    _index = 0;
                }
            }
           


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_image, _bounds, _playerWalk[_index], Color.White, 0, Vector2.Zero, _orientation, 0);
        }

        public void CheckBlockers(GameObject[] gameObjects)
        {
            foreach (GameObject item in gameObjects)
            {
                Rectangle intersection = Rectangle.Intersect(_bounds, item.Bounds);
                if (intersection.Width > 0)
                {
                    if (_previousBounds.Right <= item.X ||
                        _previousBounds.X >= item.Bounds.Right)
                    {
                        int sign = Math.Sign(_previousBounds.X - _bounds.X);
                        if (sign < 0)
                        {
                            _bounds.X = item.X - _bounds.Width;
                        }
                        else
                        {
                            _bounds.X = item.X + item.Bounds.Width;
                        }
                    }
                }
                if (intersection.Height > 0)
                {
                    if (_previousBounds.Bottom <= item.Y ||
                        _previousBounds.Y >= item.Bounds.Bottom)
                    {
                        int sign = Math.Sign(_previousBounds.Y - _bounds.Y);
                        if (sign < 0)
                        {
                            _bounds.Y = item.Y - _bounds.Height;
                        }
                        else
                        {
                            _bounds.Y = item.Y + item.Bounds.Height;
                        }

                        _speedY = 0;
                    }
                }
            }
        }
    }
}

