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
    public class Ball : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Vector2 position, screenSize;
        float speed, direction;
        Texture2D texture;
        Game1 game;

        public Ball(Game1 game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 screenSize)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.screenSize = screenSize;
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

        public void Launch()
        {
            Random r = new Random();
            direction = (float)r.NextDouble();
            speed = r.Next(3, 10);
        }

        public void Reset()
        {
            position.X = screenSize.X / 2;
            position.Y = screenSize.Y / 2;
            direction = 0;
            speed = 0;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            position.X += (float)Math.Cos((double)(direction * 2 * Math.PI)) * speed;
            position.Y += (float)Math.Sin((double)(direction * 2 * Math.PI)) * speed;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
