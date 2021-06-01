using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class RangedEnemyBullet : Bullet
    {

        public static List<RangedEnemyBullet> enemyBulletList = new List<RangedEnemyBullet>();
        private Vector2 direction;
        public static bool bossBullet;

        public RangedEnemyBullet(Texture2D tex, Vector2 pos, Vector2 direction, Facing facing) : base(tex, pos, facing)
        {
            this.direction = direction;
            isActive = true;
            //srRect = new Rectangle(0, 0, tex.Width/6, tex.Height);
            srRect = new Rectangle(0, 0, tex.Width, tex.Height);
            spritesFrame = 4;
            frameInterval = 100;
            velocity = new Vector2(0.5f, 0.5f);
            lifeSpan = 20;

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
            direction.Normalize();
            
            pos += velocity * direction;

            timer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (timer > lifeSpan)
            {
                isActive = false;
            }

            Animation(gt);

            //hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width / 6, tex.Height);
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
                //sb.Draw(tex, pos, Color.White);
                sb.Draw(AssetManager.redFire, pos, srRect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}
