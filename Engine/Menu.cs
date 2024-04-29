using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;

namespace HorseeCoins
{
    public class Menu
    {

        private Texture2D _background;

        private Vector2 _playButtonPosition;
        private Texture2D _playButtonWhite;
        bool isMouseOverPlayButton = false;

        private Texture2D _creditsButtonWhite;
        private Vector2 _creditsButtonPosition;
        bool isMouseOverCreditsButton = false;

        private Texture2D _exitButtonWhite;
        private Vector2 _exitButtonPosition;
        bool isMouseOverExitButton = false;


        public Rectangle playButtonBounds => new Rectangle((int)_playButtonPosition.X, (int)_playButtonPosition.Y, _playButtonWhite.Width, _playButtonWhite.Height);

        public Rectangle creditsButtonBounds => new Rectangle((int)_creditsButtonPosition.X, (int)_creditsButtonPosition.Y, _creditsButtonWhite.Width, _creditsButtonWhite.Height);

        public Rectangle exitButtonBounds => new Rectangle((int)_exitButtonPosition.X, (int)_exitButtonPosition.Y, _exitButtonWhite.Width, _exitButtonWhite.Height);
        public void Initialize()
        {

            _playButtonPosition = new Vector2(1380 / 2.0f - _creditsButtonWhite.Width / 2.0f, 400);
            _creditsButtonPosition = new Vector2(1380 / 2.0f - _creditsButtonWhite.Width / 2.0f, 475);
            _exitButtonPosition = new Vector2(1380 / 2.0f - _creditsButtonWhite.Width / 2.0f, 550);
        }
        public void LoadContent(ContentManager content)
        {

            _background = content.Load<Texture2D>("TileMap/Horse");

            _playButtonWhite = content.Load<Texture2D>("TileMap/botao_jogar");

            _creditsButtonWhite = content.Load<Texture2D>("TileMap/botao_creditos");

            _exitButtonWhite = content.Load<Texture2D>("TileMap/botao_sair");

        }

        public void Update(float totalSeconds, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (playButtonBounds.Contains(mouseState.Position))
            {
                isMouseOverPlayButton = true;
            }
            else
            {
                isMouseOverPlayButton = false;
            }
            if (exitButtonBounds.Contains(mouseState.Position))
            {
                isMouseOverExitButton = true;
            }
            else
            {
                isMouseOverExitButton = false;
            }
            if (creditsButtonBounds.Contains(mouseState.Position))
            {
                isMouseOverCreditsButton = true;
            }
            else
            {
                isMouseOverCreditsButton = false;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);

            spriteBatch.Draw(_playButtonWhite, _playButtonPosition, Color.White);
            spriteBatch.Draw(_creditsButtonWhite, _creditsButtonPosition, Color.White);
            spriteBatch.Draw(_exitButtonWhite, _exitButtonPosition, Color.White);

        }

    }
}