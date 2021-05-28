using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class EnemyLust : EnemyType
    {
        Bullet bullet;
        public List<Bullet> bulletList = new List<Bullet>();

        int shotCount;
        double shootCD;

        public EnemyLust(Texture2D tex, Vector2 pos/*, GameWindow window*/, Player player, PokemonGeodude geodude) : base(tex, pos, geodude)
        {
            this.player = player;
            isActive = true;
            isHit = false;

            shotCount = 1;
            shootCD = 0;

            effect = SpriteEffects.None;
            moveDir = new Vector2(50, 50);

            enemyState = EnemyState.Patrol;
            velocity = new Vector2(3, 0);
            startVelocity = velocity;
            chasingRange = 2000;

            randomizationPeriod = 4;
            rand = new Random();
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            healthPoints = 1000;
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

            if (other is PokemonGeodude)
                healthPoints -= 100;
        }

        public override void Update(GameTime gt)
        {
            if (healthPoints <= 0)
            {
                if(isActive)
                    GameStats.coinCollected += 50;

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

            PlayerCharmed();
            ShootCooldown(gt);
            Movement();
            CurrentEnemyState(gt);
            EnemyFacing();
            Combat(gt);
            DistanceToGeo();
        }


        public override float DistanceToGeo()
        {
            directionToGeo = pos - geodude.Position;
            return directionToGeo.Length();
        }

        void PlayerCharmed()
        {
            if (player.charmed == true)
            {
                if (player.Position.X < pos.X)
                {
                    player.playerFacing = Facing.Right;
                    player.Velocity = 1f;


                }
                else if (player.Position.X > pos.X)
                {
                    player.playerFacing = Facing.Left;
                    player.Velocity = -1f;
                }
            }
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
                    bullet = new LustBullet(AssetManager.heartSprite, pos, enemyFacing);
                    bulletList.Add(bullet);
                    bullet.isActive = true;

                    shotCount--;
                }
            }

            if (enemyState == EnemyState.Chase || enemyState == EnemyState.Patrol)
            {
                foreach (LustBullet bullet in bulletList)
                {
                    bullet.Velocity = new Vector2(3, 3);
                    moveDir = player.Position - bullet.Position;
                    bullet.Position += bullet.Velocity * moveDir * 0.015f;
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
                sb.Draw(tex, pos, null, Color.White, 0, Vector2.Zero, 1, effect, 1);

            foreach (LustBullet bullet in bulletList)
            {
                bullet.Draw(sb);
            }
        }

    }
}
