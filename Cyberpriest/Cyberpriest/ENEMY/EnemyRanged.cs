using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class EnemyRanged : EnemyType
    {

        RangedEnemyBullet bullet;

        Vector2 bulletOffset;

        int shotCount;
        double shootCD;

        public EnemyRanged(Texture2D tex, Vector2 pos, Player player, PokemonGeodude geodude) : base(tex, pos, geodude)
        {
            //bulletList = new List<Bullet>();

            this.tex = tex;
            this.pos = pos;
            this.geodude = geodude;
            this.player = player;

            bulletOffset = new Vector2(0, tex.Height / 2);

            isActive = true;
            tileSize = new Point(64, 64);
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            srRect = new Rectangle(0, 0, tex.Width / 4, tex.Height);

            spritesFrame = 4;
            frameInterval = 100;

            shotCount = 1;
            shootCD = 0;

            //bulletOffset = new Vector2(0, -(tex.Height / 2));

            moveDir = new Vector2(50, 50);

            healthPoints = 100;
            chasingRange = 250;

            enemyState = EnemyState.Patrol;
        }

        public override void Update(GameTime gt)
        {
            moveDir = player.Position - pos;
            moveDir.Normalize();

            distanceToPlayerX = (int)player.Position.X - (int)pos.X;
            distanceToPlayerY = (int)player.Position.Y - (int)pos.Y;

            if (distanceToPlayerX < 0)
                distanceToPlayerX = -distanceToPlayerX;

            if (distanceToPlayerY < 0)
                distanceToPlayerY = -distanceToPlayerY;

            if (healthPoints <= 0)
            {
                isActive = false;
            }

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            foreach (RangedEnemyBullet bullet in RangedEnemyBullet.enemyBulletList)
            {
                bullet.Update(gt);
            }

            Animation(gt);
            CurrentEnemyState(gt);
            ShootCooldown(gt);
            EnemyFacing();
        }

        protected override void CurrentEnemyState(GameTime gt)
        {
            switch (enemyState)
            {
                case EnemyState.RangedAttack:
                    Combat(gt);
                    break;

                case EnemyState.Patrol:
                    if (distanceToPlayerX <= chasingRange && distanceToPlayerY <= chasingRange)
                    {
                        enemyState = EnemyState.RangedAttack;
                    }
                    break;
            }
        }

        public override void HandleCollision(GameObject other)
        {
            isGrounded = true;

            if (other is Platform)
            {
                hitBox.Y = other.hitBox.Y - hitBox.Height;
                pos.Y = hitBox.Y;
            }

            if (other is Bullet && other.isActive == true && isActive == true)
            {
                healthPoints -= 50;
                other.isActive = false;
            }
        }

        void Combat(GameTime gt)
        {
            if (enemyState == EnemyState.RangedAttack && isActive == true)
            {
                if (shotCount > 0)
                {
                    bullet = new RangedEnemyBullet(AssetManager.star1, pos - bulletOffset, moveDir, enemyFacing);
                    RangedEnemyBullet.enemyBulletList.Add(bullet);

                    shotCount--;

                    for (int j = 0; j < RangedEnemyBullet.enemyBulletList.Count(); j++)
                    {
                        if (bullet.isActive == false)
                        {
                            RangedEnemyBullet.enemyBulletList.RemoveAt(j);
                        }
                    }
                }

                if (distanceToPlayerX >= chasingRange || distanceToPlayerY >= chasingRange)
                {
                    enemyState = EnemyState.Patrol;
                }
            }
        }

        public void ShootCooldown(GameTime gt)
        {
            if (shotCount <= 0)
                shootCD += gt.ElapsedGameTime.TotalSeconds;

            double cooldown = 4;

            if (shootCD >= cooldown && shotCount == 0)
            {
                shotCount = 1;
                if (shotCount == 1)
                    shootCD = 0;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
            {
                sb.Draw(tex, pos, srRect, Color.White, 0, Vector2.Zero, 1, effect, 1);

            }

            foreach (RangedEnemyBullet bullet in RangedEnemyBullet.enemyBulletList)
            {
                bullet.Draw(sb);
            }
        }
    }

}
