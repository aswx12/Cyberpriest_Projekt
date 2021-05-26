using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class Coin : StationaryObject
    {
        Player player;
        List<PowerUp> powerUpList;

        float distanceToPlayerX;
        float magnetRange = 25f;

        Vector2 moveDir;
        Vector2 velocity;

        public Coin(Texture2D tex, Vector2 pos, Player player, List<PowerUp> powerUpList) : base(tex, pos)
        {
            frameInterval = 100;
            spritesFrame = 6;

            this.player = player;
            this.powerUpList = powerUpList;

            moveDir = new Vector2(50, 50);
            velocity = new Vector2(1, 1);

            srRect = new Rectangle(0, 0, tex.Width / 6, tex.Height);
        }

        public override void HandleCollision(GameObject other)
        {
            if (other is Player)
            {
                isActive = false;
            }
        }

        public override void Update(GameTime gt)
        {
            distanceToPlayerX = pos.X - player.Position.X;

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            Animation(gt);
            PowerUp();
        }

        void PowerUp()
        {
            foreach (PowerUp powerUp in powerUpList)
            {
                if (powerUp.poweredUp && powerUp.GetTexture == AssetManager.magnet)
                {
                    MagnetPowerUp();
                }
            }
        }

        void MagnetPowerUp()
        {
            if (distanceToPlayerX < magnetRange)
            {
                moveDir = player.Position - pos;
                pos += velocity * moveDir * 0.05f;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
                sb.Draw(tex, pos, srRect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}
