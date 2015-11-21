using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AHTWPong
{
    public enum GameState { PRE_GAME, GAME, POST_GAME }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const int PLAYER_ONE = 1;
        public const int PLAYER_TWO = 2;
        public GameState state;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Paddle playerOne, playerTwo;
        Ball ball;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D paddleLeft = Content.Load<Texture2D>("images/BatLeft");
            Texture2D paddleRight = Content.Load<Texture2D>("images/BatRight");
            Texture2D ball = Content.Load<Texture2D>("images/Ball");

            Vector2 screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            this.state = GameState.PRE_GAME;

            playerOne = new Paddle(this, new Vector2(1, graphics.PreferredBackBufferHeight / 2 - (paddleLeft.Height / 2)), paddleLeft, spriteBatch, PLAYER_ONE, screenSize);
            playerTwo = new Paddle(this, new Vector2(graphics.PreferredBackBufferWidth - 1 - paddleRight.Width, graphics.PreferredBackBufferHeight / 2 - (paddleRight.Height / 2)), paddleRight, spriteBatch, PLAYER_TWO, screenSize);
            this.ball = new Ball(this, spriteBatch, ball, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

            Components.Add(playerOne);
            Components.Add(playerTwo);
            Components.Add(this.ball);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && state == GameState.PRE_GAME)
            {
                state = GameState.GAME;
                ball.Launch();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R) && (state == GameState.GAME))
            {
                state = GameState.PRE_GAME;
                ball.Reset();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.NavajoWhite);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
