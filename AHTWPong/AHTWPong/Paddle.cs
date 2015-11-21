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
    public class Paddle : DrawableGameComponent
    {
        const float PADDLE_SPEED = 5f;

        Vector2 position, screenSize, initialPosition;
        Texture2D texture;
        SpriteBatch spriteBatch;
        int player;
        Game1 game;

        public Paddle(Game1 game, Vector2 position, Texture2D texture, SpriteBatch spriteBatch, int playerNumber, Vector2 screenSize)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.position = initialPosition = position;
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            this.screenSize = screenSize;
            player = playerNumber;
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

        public void Reset()
        {
            this.position = initialPosition;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (game.state != GameState.GAME)
            {
                base.Update(gameTime);
                return;
            }
            // TODO: Add your update code here
            KeyboardState ks = Keyboard.GetState();

            if ((player == 1) ? ks.IsKeyDown(Keys.A) : ks.IsKeyDown(Keys.Up))
            {
                position.Y -= PADDLE_SPEED;
            }

            if ((player == 1) ? ks.IsKeyDown(Keys.Z) : ks.IsKeyDown(Keys.Down))
            {
                position.Y += PADDLE_SPEED;
            }

            if (position.Y < 0) position.Y = 0f;
            if ((position.Y + texture.Height) > screenSize.Y) position.Y = screenSize.Y - texture.Height;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
