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
        public EnemyLust(Texture2D tex, Vector2 pos, GameWindow window, Player player) : base(tex, pos, window)
        {
            this.player = player;
            isActive = true;
            isHit = false;
            enemyState = Cyberpriest.EnemyState.Patrol;
            velocity = new Vector2(1, 0);
            startVelocity = velocity;
            chasingRange = 200;

            randomizationPeriod = 2;
            rand = new Random();
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            healthPoints = 1000;
            velocity = new Vector2(2, 0);
        }

        public override void HandleCollision(GameObject other)
        {
            velocity.Y = 0;
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

            if (other is Player)
                isHit = true;
        }

        public override void Update(GameTime gt)
        {
            pos.Y += gravity;

            if (healthPoints <= 0)
            {
                isActive = false;
            }

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            distanceToPlayerX = (int)player.GetPos.X - (int)pos.X;
            distanceToPlayerY = (int)player.GetPos.Y - (int)pos.Y;

            Movement();
            EnemyState(gt);
        }

        private void Movement()
        {
            if (distanceToPlayerX < 0)
                distanceToPlayerX = distanceToPlayerX * -1;

            if (distanceToPlayerX < chasingRange)
            {
                moveDir = player.GetPos - pos;
                enemyState = Cyberpriest.EnemyState.Chase;
            }
            else
            {
                enemyState = Cyberpriest.EnemyState.Patrol;
            }
        }

        private void EnemyState(GameTime gt)
        {
            switch (enemyState)
            {
                case Cyberpriest.EnemyState.Patrol:
                    pos += velocity;
                    PatrolTimer(gt);
                    break;
                case Cyberpriest.EnemyState.Chase:
                    if (player.GetPos.X > pos.X)
                    {
                        pos.X += startVelocity.X;
                    }
                    else if (player.GetPos.X < pos.X)
                    {
                        pos.X -= startVelocity.X;
                    }

                    if (distanceToPlayerX > chasingRange)
                        enemyState = Cyberpriest.EnemyState.Patrol;
                    break;
            }
        }

        void RandomDirection()
        {
            int random = rand.Next(0, 2);

            //Left
            if (random == 0)
            {
                velocity.X = -1f;
            }

            //Right
            if (random == 1)
            {
                velocity.X = 1f;
            }
        }

        void PatrolTimer(GameTime gt)
        {
            randomizationTime += (float)gt.ElapsedGameTime.TotalSeconds;

            if (randomizationTime >= randomizationPeriod)
            {
                RandomDirection();
                randomizationTime = 0.0f;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
            {
                sb.Draw(tex, pos, Color.White);
            }
        }

    }
}
