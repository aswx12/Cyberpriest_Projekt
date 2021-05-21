using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class GameStats
    {
        public static Health health;
        public static int coinCollected;
        public static int maxAmmo;
        public static int currentAmmo;

        public GameStats()
        {
            health = new Health(AssetManager.fullHealthbar, Vector2.Zero);

            coinCollected = 0;
            maxAmmo = 16;
        }

        public void Draw(SpriteBatch sb)
        {
            if (Game1.GetState == GameState.Play)
            {
                sb.DrawString(AssetManager.normalFont, "Coins: " + coinCollected, new Vector2(1500, 15), Color.Gold);
                sb.DrawString(AssetManager.normalFont, "Ammo: " + currentAmmo + "/" + maxAmmo, new Vector2(750, 15), Color.LightGray);
                health.Draw(sb);
            }
        }
    }
}
