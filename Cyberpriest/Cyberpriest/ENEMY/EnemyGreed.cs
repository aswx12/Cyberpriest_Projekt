using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class EnemyGreed : EnemyType
    {
        Bullet bullet;
        Bullet bullet2;
        Bullet bullet3;

        Key key;

        int shotCount;
        double shootCD;
        public static List<Bullet> greedBulletList;

        public EnemyGreed(Texture2D tex, Vector2 pos, Player player, PokemonGeodude geodude) : base(tex, pos, geodude)
        {
            this.player = player;

            tileSize = new Point(128, 128);
            greedBulletList = new List<Bullet>();

            isActive = true;
            isHit = false;

            shotCount = 1;
            shootCD = 0;

            effect = SpriteEffects.None;
            moveDir = new Vector2(50, 50);

            enemyState = EnemyState.Patrol;
            velocity = new Vector2(1, 0);
            startVelocity = velocity;
            chasingRange = 2000;

            randomizationPeriod = 4;
            rand = new Random();
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            srRect = new Rectangle(0, 0, tex.Width / 5, tex.Height);
            healthPoints = 2000;

            frameInterval = 100;
            spritesFrame = 5;
            spriteSize = 128;
        }

        public override void HandleCollision(GameObject other)
        {
            velocity.Y = 0;
            isGrounded = true;

            if (other is Bullet && other.isActive == true && isActive == true)
            {
                healthPoints -= 50;
                other.isActive = false;
            }

            if (other is Player)
                isHit = true;
        }

        public override void Update(GameTime gt)
        {
            for (int j = 0; j < greedBulletList.Count(); j++)
            {
                if (bullet.isActive == false)
                {
                    greedBulletList.RemoveAt(j);
                    j--;
                }
            }

            if (healthPoints <= 0)
            {
                if (isActive)
                    GameStats.coinCollected += 50;

                Key.keyPickedUp = true;
                isActive = false;
            }

            if (pos.X >= 4550 || pos.X <= 3500)
            {
                velocity *= -1;
            }

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            distanceToPlayerX = (int)player.Position.X - (int)pos.X;
            distanceToPlayerY = (int)player.Position.Y - (int)pos.Y;

            ShootCooldown(gt);
            Movement();
            CurrentEnemyState(gt);
            EnemyFacing();
            Combat(gt);
            DistanceToGeo();
            Animation(gt);
        }


        public override float DistanceToGeo()
        {
            directionToGeo = pos - geodude.Position;
            return directionToGeo.Length();
        }

        private void Movement()
        {
            if (distanceToPlayerX < 0)
                distanceToPlayerX = distanceToPlayerX * -1;

            if (distanceToPlayerX < chasingRange)
            {
                moveDir = player.Position - pos;
                enemyState = EnemyState.Chase;
            }
            else
            {
                enemyState = EnemyState.Patrol;
            }
        }

        void Combat(GameTime gt)
        {
            if (enemyState == EnemyState.Chase && isActive == true)
            {
                if (shotCount > 0)
                {
                    RangedEnemyBullet.bossBullet = true;
                    bullet = new RangedEnemyBullet(AssetManager.star1, pos, moveDir, enemyFacing);
                    moveDir.Y -= 100;
                    moveDir.X -= 100;
                    bullet2 = new RangedEnemyBullet(AssetManager.star1, pos, moveDir, enemyFacing);
                    moveDir.Y += 200;
                    moveDir.X += 200;
                    bullet3 = new RangedEnemyBullet(AssetManager.star1, pos, moveDir, enemyFacing);

                    greedBulletList.Add(bullet);
                    greedBulletList.Add(bullet2);
                    greedBulletList.Add(bullet3);
                    bullet.isActive = true;

                    shotCount--;

                }
            }

            if (enemyState == EnemyState.Chase || enemyState == EnemyState.Patrol)
            {
                foreach (RangedEnemyBullet bullet in greedBulletList)
                {
                    bullet.Velocity = new Vector2(5, 5);
                    moveDir = player.Position - bullet.Position;
                    bullet.Update(gt);
                }
            }
        }

        public void ShootCooldown(GameTime gt)
        {
            if (shotCount <= 0)
                shootCD += gt.ElapsedGameTime.TotalSeconds;

            double cooldown = 10;

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
                sb.Draw(tex, pos, srRect, Color.White, 0, Vector2.Zero, 1, effect, 1);


            foreach (RangedEnemyBullet bullet in greedBulletList)
            {
                bullet.Draw(sb);
            }
        }
    }
}
