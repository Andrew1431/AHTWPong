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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        Game1 game;

        public CollisionManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = (Game1)game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (game.state != GameState.GAME)
            {
                base.Update(gameTime);
                return;
            }

            Ball ball;
            Paddle playerOne, playerTwo;

            ball = game.ball;
            playerOne = game.playerOne;
            playerTwo = game.playerTwo;

            // Paddle hit
            if ((Rectangle.Intersect(ball.GetCollisionRectangle(), playerOne.GetCollisionRectangle()).Width > 0 && ball.Velocity.X < 0)
                    || (Rectangle.Intersect(ball.GetCollisionRectangle(), playerTwo.GetCollisionRectangle()).Width > 0 && ball.Velocity.X > 0))
            {
                game.Click.Play();
                ball.Velocity.X = -ball.Velocity.X;
            }
            // Ceiling / Bottom hit
            else if (ball.Position.Y - ball.Texture.Height / 2 < 0 || ball.Position.Y + ball.Texture.Height / 2 > game.ScreenSize.Y)
            {
                game.Click.Play();
                ball.Velocity.Y = -ball.Velocity.Y;
            }
            // Left paddle hit
            else if (ball.Position.X - ball.Texture.Width / 2 < 0)
            {
                // player one score logic here
                game.Ding.Play();
                Game1.player_two_score++;
                if (Game1.player_two_score == Game1.SCORE_TO_WIN)
                {
                    game.Applause.Play();
                }
                game.state = GameState.PRE_GAME;
                ball.Reset();
                playerOne.Reset();
                playerTwo.Reset();
            }
            // Right paddle hit
            else if (ball.Position.X + ball.Texture.Width / 2 > game.ScreenSize.X)
            {
                // player two score logic here
                Game1.player_one_score++;
                game.Ding.Play();
                if (Game1.player_one_score == Game1.SCORE_TO_WIN)
                {
                    game.Applause.Play();
                }
                game.state = GameState.PRE_GAME;
                ball.Reset();
                playerOne.Reset();
                playerTwo.Reset();
            }



            

            base.Update(gameTime);
        }
    }
}
