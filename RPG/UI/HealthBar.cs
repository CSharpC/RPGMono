using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGCore.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.UI
{
    public class HealthBar
    {
        int fullWidth;
        Rectangle barPlacement;
        Texture2D healthBar;
        Texture2D barShadow;
        SpriteFont font;
        Vector2 textPos;
        Creature creature;

        public HealthBar(Creature creature, Rectangle rect)
        {
            var c = Game1.Instance.Content;

            barPlacement = rect;
            fullWidth = barPlacement.Width;
            this.creature = creature;
            healthBar = c.Load<Texture2D>("healthbar");
            barShadow = c.Load<Texture2D>("healthbar_shadow");
            font = c.Load<SpriteFont>("Default");
            textPos = new Vector2(barPlacement.X + 12, barPlacement.Y + 7);

            Game1.Instance.Drawing += Draw;
            Game1.Instance.Updating += Update;
        }

        private void Update(GameTime gameTime)
        {
            barPlacement.Width = (int)(fullWidth * (creature.CurHealth / (float)creature.MaxHealth));
        }

        ~HealthBar()
        {
            Game1.Instance.Drawing -= Draw;
        }

        private void Draw(GameTime gameTime)
        {
            var batch = Game1.Instance.spriteBatch;
            batch.Begin();

            batch.Draw(barShadow, new Rectangle(barPlacement.X + 2, barPlacement.Y + 2, barPlacement.Width, barPlacement.Height), Color.White);
            batch.Draw(healthBar, barPlacement, Color.White);
            batch.DrawString(font, "Health: " + creature.HealthText, textPos, Color.White);

            batch.End();
        }
    }
}
