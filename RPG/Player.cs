using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG
{
    class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        private Texture2D meme;


        private Vector2 playerPosition;
        public Player(Game game) : base(game)
        {
            
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
            KeyboardState helper = Keyboard.GetState();
            
            // Movement
            if (helper.IsKeyDown(Keys.Down))
                playerPosition.Y += 5.0f;

            if (helper.IsKeyDown(Keys.Right))
                playerPosition.X += 5.0f;

            if (helper.IsKeyDown(Keys.Left))
                playerPosition.X -= 5.0f;

            if (helper.IsKeyDown(Keys.Up))
                playerPosition.Y -= 5.0f;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(meme, playerPosition, Color.Black);
            spriteBatch.End();
        }
    }
}
