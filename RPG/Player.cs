using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGCore.Creatures;

namespace RPG
{
    class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Creature Character { get; set; }

        private Texture2D meme;
        private SpriteBatch spriteBatch;
        private Vector2 playerPosition;
        private InputHelper input;

        public Player(Game game) : base(game)
        {
            input = InputHelper.Current;
            Character = new Creature("Player", 0, 10, 10, 10, 10, 10, 40, 5, 10, 2, 5, 5);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);

            // Sprite
            meme = Game.Content.Load<Texture2D>("annotation_rectangle");
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
                        
            // Movement
            if (input.KeyPressed(Keys.Down))
                playerPosition.Y += 32;

            if (input.KeyPressed(Keys.Right))
                playerPosition.X += 32;

            if (input.KeyPressed(Keys.Left))
                playerPosition.X -= 32;

            if (input.KeyPressed(Keys.Up))
                playerPosition.Y -= 32;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(meme, playerPosition, Color.Black);
            spriteBatch.End();
        }
    }
}
