using HorseeCoins.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;

namespace HorseeCoins
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private Menu _menu;
        private Player _player;
        private Portal _portal;
        private Portal2 _portal2;
        private GameObject[] _platforms;
        private Castle _castle;
        private Credits _credits;

        private Texture2D _coinTexture;

        private List<Coin> _coins;

        private int _coinsCollected;   

        private bool _interactCastle;

        private SpriteFont _font;
        private SpriteFont _fontWin;

        private Texture2D _backCredits;

        private Texture2D _backgroundGame;


        private bool gameStarted = false;
        private bool _gameOver;
        private bool _credit;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();


            _graphics.PreferredBackBufferWidth = 1280; 
            _graphics.PreferredBackBufferHeight = 720; 
            _graphics.ApplyChanges();


            _coinsCollected = 0;

            _player.Initialize();
            _portal.Position = new Point(1200, 550);
            _portal2.Position = new Point(50, 280);

            _platforms[0].Position = new Point(0, 650);
            _platforms[1].Position = new Point(160, 650);
            _platforms[2].Position = new Point(380, 600);
            _platforms[3].Position = new Point(560, 520);
            _platforms[4].Position = new Point(720, 520);
            _platforms[5].Position = new Point(830, 620);
            _platforms[6].Position = new Point(990, 620);
            _platforms[7].Position = new Point(1150, 620);

            _platforms[8].Position = new Point(0, 360);
            _platforms[9].Position = new Point(320, 420);
            _platforms[10].Position = new Point(600, 350);
            _platforms[11].Position = new Point(800, 420);
            _platforms[12].Position = new Point(960, 420);
            _platforms[13].Position = new Point(1120, 300);

            _platforms[14].Position = new Point(0, 150);
            _platforms[15].Position = new Point(486, 150);
            _platforms[16].Position = new Point(972, 150);



            _credits = new Credits(this);
            _credits.Initialize();
        }   

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            _menu = new Menu();
            _menu.LoadContent(Content);
            _menu.Initialize();


            _backgroundGame = Content.Load<Texture2D>("TileMap/background");


            _font = Content.Load<SpriteFont>("Fonts/Font");
            _fontWin = Content.Load<SpriteFont>("Fonts/fonteWin");


            _castle = new Castle();
            _castle.LoadContent(Content);
            _castle.Initialize();


            Texture2D playerImage = Content.Load<Texture2D>("Player/cobrador_jump");
            _player = new Player(playerImage);


            Texture2D portalImage = Content.Load<Texture2D>("TileMap/portal");
            _portal = new Portal(portalImage);
            Texture2D portalImage2 = Content.Load<Texture2D>("TileMap/portal");
            _portal2 = new Portal2(portalImage2);

            Texture2D platformImage1 = Content.Load<Texture2D>("TileMap/retaDez");
            Texture2D platformImage2 = Content.Load<Texture2D>("TileMap/reta_ouro");
            _platforms = new GameObject[]
            {
            new GameObject(platformImage1), new GameObject(platformImage1),new GameObject(platformImage1),new GameObject(platformImage1),
            new GameObject(platformImage1),new GameObject(platformImage1),new GameObject(platformImage1),new GameObject(platformImage1),
            new GameObject(platformImage1),new GameObject(platformImage1),new GameObject(platformImage1),new GameObject(platformImage1),
            new GameObject(platformImage1),new GameObject(platformImage1),new GameObject(platformImage2),new GameObject(platformImage2),
            new GameObject(platformImage2)
            };


            _coinTexture = Content.Load<Texture2D>("Coin/moeda1_linda");

            _coins = new List<Coin>();

            _coins.Add(new Coin(_coinTexture, new Vector2(1000, 100)));
            _coins.Add(new Coin(_coinTexture, new Vector2(900, 100)));
            _coins.Add(new Coin(_coinTexture, new Vector2(800, 100)));
            _coins.Add(new Coin(_coinTexture, new Vector2(500, 100)));
            _coins.Add(new Coin(_coinTexture, new Vector2(400, 100)));
            _coins.Add(new Coin(_coinTexture, new Vector2(100, 100)));
            _coins.Add(new Coin(_coinTexture, new Vector2(200, 300)));
            _coins.Add(new Coin(_coinTexture, new Vector2(300, 300)));
            _coins.Add(new Coin(_coinTexture, new Vector2(400, 300)));
            _coins.Add(new Coin(_coinTexture, new Vector2(600, 470)));
            _coins.Add(new Coin(_coinTexture, new Vector2(700, 470)));
            _coins.Add(new Coin(_coinTexture, new Vector2(800, 470)));
            _coins.Add(new Coin(_coinTexture, new Vector2(1090, 570)));
            _coins.Add(new Coin(_coinTexture, new Vector2(990, 570)));
            _coins.Add(new Coin(_coinTexture, new Vector2(500, 570)));
            _coins.Add(new Coin(_coinTexture, new Vector2(200, 600)));
            _coins.Add(new Coin(_coinTexture, new Vector2(100, 600)));

            foreach (Coin coin in _coins)
            {
                coin.Initialize();
            }


            _backCredits = Content.Load<Texture2D>("TileMap/background");
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
          
            Rectangle playerBounds = _player.Bounds;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _player.Update(deltaTime);
            _player.CheckBlockers(_platforms);
            _portal.CheckCollision(_player);
            _portal2.CheckCollision(_player);


            foreach (Coin coin in _coins)
            {
                coin.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);

                if (!_gameOver && !coin._isCollected && coin.CheckCollision(playerBounds))
                {
                    coin._isCollected = true;
                    _coinsCollected++;
                }
            }

            _castle.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);

   
            if (!_gameOver && _coinsCollected == _coins.Count)
            {
                _interactCastle = true;

            }


            if (_interactCastle && playerBounds.Intersects(_castle.castleBounds))
            {
                _gameOver = true;
            }

            if (_gameOver)
            {
                _credits.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);
                return;
            }
            if (_credit)
            {

                _credits.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);
                return;
            }

            MouseState mouseState = Mouse.GetState();

            _menu.Update((float)gameTime.ElapsedGameTime.TotalSeconds, gameTime);

            Rectangle playButtonBounds = _menu.playButtonBounds;
            Rectangle exitButtonBounds = _menu.exitButtonBounds;
            Rectangle creditsButtonBounds = _menu.creditsButtonBounds;


            if (mouseState.LeftButton == ButtonState.Pressed && playButtonBounds.Contains(Mouse.GetState().Position))
            {
                StartGame();

            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && exitButtonBounds.Contains(Mouse.GetState().Position))
            {

                Exit(); 
            }
            if (mouseState.LeftButton == ButtonState.Pressed && creditsButtonBounds.Contains(Mouse.GetState().Position))
            {
                Credits();

            }


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            


            _menu.Draw(_spriteBatch);


            if (gameStarted)
            { 
                _spriteBatch.Draw(_backgroundGame, Vector2.Zero, Color.White);

                foreach (Coin coin in _coins)
                {
                    if (!coin._isCollected)
                    {
                        coin.Draw(_spriteBatch);
                    }
                }

                _castle.Draw(_spriteBatch);

                _spriteBatch.DrawString(_font, "Moedas coletadas: " + _coinsCollected, new Vector2(35, 35), Color.White);


                _portal.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
                _portal2.Draw(_spriteBatch);
                foreach (GameObject item in _platforms)
                {
                    item.Draw(_spriteBatch);
                }


                if (_gameOver)
                {

                    _spriteBatch.Draw(_backCredits, Vector2.Zero, Color.White);
                    _spriteBatch.DrawString(_fontWin, "Voce venceu!", new Vector2(310, 200), Color.Black);
                    _credits.Draw(_spriteBatch);
                }

                if (_credit)
                {

                    _spriteBatch.Draw(_backCredits, Vector2.Zero, Color.White);
                    _credits.Draw(_spriteBatch);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void StartGame()
        {
            _player.Initialize(); 
            gameStarted = true; 
        }

        public void EndGame()
        {
            _gameOver = true;
        }

        private void Credits()
        {
            gameStarted = true;
            _player.Initialize();
            _credit = true;
        }

        public void ShowMenu()
        {
            _credit = true;
            _gameOver = false;
            gameStarted = false;
            _coinsCollected = 0;
            _player.Initialize();
            _menu.Initialize();
  
        }


    }
}
