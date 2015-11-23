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
    public class Score : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Vector2 position;
        private string scoreString;

        public string ScoreString
        {
            get { return scoreString; }
            set { scoreString = value; }
        }
        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Score(Game game, SpriteBatch spriteBatch,
            SpriteFont font,
            Vector2 position,
            string scoreString,
            Color color)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
            this.scoreString = scoreString;
            this.color = color;
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, scoreString, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
