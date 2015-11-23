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
        //Setup game.
        public const int PLAYER_ONE = 1;
        public const int PLAYER_TWO = 2;
        public const string PLAYER_ONE_NAME = "Taylor";
        public const string PLAYER_TWO_NAME = "Andrew";
        public const int SCORE_TO_WIN = 3;
        public static int player_one_score = 0;
        public static int player_two_score = 0;
        
        public GameState state;
        public SoundEffect Click, Applause, Ding;

        GraphicsDeviceManager graphics;
        SpriteFont font;
        SpriteBatch spriteBatch;
        public Paddle playerOne, playerTwo;
        public Ball ball;
        Vector2 screenSize;
        CollisionManager collisions;
        public Score score;
        public Score winMessage;
        public Boolean flag;

        public Vector2 ScreenSize
        {
            get { return screenSize; }
            set { screenSize = value; }
        }

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
            font = Content.Load<SpriteFont>("fonts/SpriteFont1");
            Click = Content.Load<SoundEffect>("audio/click");
            Applause = Content.Load<SoundEffect>("audio/applause1");
            Ding = Content.Load<SoundEffect>("audio/ding");
            screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            this.state = GameState.PRE_GAME;

            playerOne = new Paddle(this, new Vector2(1, graphics.PreferredBackBufferHeight / 2 - (paddleLeft.Height / 2)), paddleLeft, spriteBatch, PLAYER_ONE, screenSize);
            playerTwo = new Paddle(this, new Vector2(graphics.PreferredBackBufferWidth - 1 - paddleRight.Width, graphics.PreferredBackBufferHeight / 2 - (paddleRight.Height / 2)), paddleRight, spriteBatch, PLAYER_TWO, screenSize);
            this.ball = new Ball(this, spriteBatch, ball, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            collisions = new CollisionManager(this);

            string winMessageString = "";
            string scoreString = "";
            Vector2 scorePosition = Vector2.Zero;
            Vector2 winMessagePosition = new Vector2(graphics.PreferredBackBufferWidth /2, graphics.PreferredBackBufferHeight /2);
            score = new Score(this, spriteBatch, font, scorePosition, scoreString, Color.White);
            winMessage = new Score(this, spriteBatch, font, winMessagePosition, winMessageString, Color.AliceBlue);
            winMessage.Visible = false;
            Components.Add(winMessage);
            Components.Add(score);
            Components.Add(playerOne);
            Components.Add(playerTwo);
            Components.Add(this.ball);
            Components.Add(collisions);

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
            // Score
            string scoreMessage = string.Format("{0}: {1}\n{2}: {3}", PLAYER_ONE_NAME, player_one_score.ToString(), PLAYER_TWO_NAME, player_two_score.ToString());
            score.ScoreString = scoreMessage;
            if (player_one_score == SCORE_TO_WIN)
            {
                winner(PLAYER_ONE_NAME);
            }
            else if (player_two_score == SCORE_TO_WIN)
            {
                winner(PLAYER_TWO_NAME);
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && state == GameState.PRE_GAME)
            {
                state = GameState.GAME;
                ball.Launch();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R) && (state == GameState.POST_GAME))
            {
                state = GameState.PRE_GAME;
                ball.Reset();
                playerOne.Reset();
                playerTwo.Reset();
                flag = true;
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

        public void winner(string winner)
        {
            state = GameState.POST_GAME;

            //should only play once.
            //TODO: do this one.
            Applause.Play();
            string winMessageString = "Congratulations " + winner;
            Vector2 dimension = font.MeasureString(winMessageString);
            Vector2 messagePos = new Vector2(graphics.PreferredBackBufferWidth / 2 - dimension.X / 2, graphics.PreferredBackBufferHeight / 2 - dimension.Y / 2);
            winMessage.ScoreString = winMessageString;
            winMessage.Position = messagePos;
            winMessage.Visible = true;
            ball.Reset();
            playerOne.Reset();
            playerTwo.Reset();
        }
    }
}
