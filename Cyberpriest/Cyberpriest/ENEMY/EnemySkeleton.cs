using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class EnemySkeleton : EnemyType
    {
        public EnemySkeleton(Texture2D tex, Vector2 pos, GameWindow window, Player player) : base(tex, pos, window)
        {
            this.player = player;
            isActive = true;
            isHit = false;
            healthPoints = 100;

            enemyState = Cyberpriest.EnemyState.Patrol;

            moveDir = new Vector2(50, 50);
            velocity = new Vector2(1, 0);
            startVelocity = velocity;
            chasingRange = 200;

            randomizationPeriod = 2;
            rand = new Random();
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
        }

        public override void HandleCollision(GameObject other)
        {
            velocity.Y = 0;
            isGrounded = true;

            if (other is Platform)
            {
                velocity.Y = 0;
                isGrounded = true;
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

            if (healthPoints <= 0)
            {
                isActive = false;
            }

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            distanceToPlayerX = (int)player.GetPos.X - (int)pos.X;
            distanceToPlayerY = (int)player.GetPos.Y - (int)pos.Y;

            velocity.Y += gravity;

            Movement();
            EnemyState(gt);

            //Console.WriteLine("distanceToPlayerX " + distanceToPlayerX);
            //Console.WriteLine("enemyState " + enemyState);
            //Console.WriteLine("moveDir " + moveDir);
            //Console.WriteLine("enemyState " + enemyState);
            //Console.WriteLine("vel " + velocity);
        }

        private void Movement()
        {
            if (isGrounded == false)
            {
                pos.Y += gravity;
            }


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
                        pos.X += velocity.X;
                    }
                    else if (player.GetPos.X < pos.X)
                    {
                        pos.X -= velocity.X;
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
                if (moveDir.X < 0)
                    sb.Draw(tex, pos, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 1);
                else
                    sb.Draw(tex, pos, Color.Red);
        }

    }
}

