using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Cyberpriest
{
    class EnemyGhost : EnemyType
    {
        public EnemyGhost(Texture2D tex, Vector2 pos, Player player, PokemonGeodude geodude) : base(tex, pos, geodude)
        {
            this.player = player;
            isActive = true;
            isHit = false;
            healthPoints = 200;
            effect = SpriteEffects.None;
            enemyState = EnemyState.Patrol;

            moveDir = new Vector2(50, 50);
            velocity = new Vector2(1, 1);
            startVelocity = velocity;
            chasingRange = 250;

            randomizationPeriod = 2;
            rand = new Random();
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            srRect = new Rectangle(0, 0, tex.Width / 6, tex.Height);

            frameInterval = 100;
            spritesFrame = 6;
        }

        public override void HandleCollision(GameObject other)
        {
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
            Console.WriteLine(enemyFacing);

            if (healthPoints <= 0)
            {
                if (isActive)
                    GameStats.coinCollected += 5;

                isActive = false;
            }

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            distanceToPlayerX = (int)player.Position.X - (int)pos.X;
            distanceToPlayerY = (int)player.Position.Y - (int)pos.Y;

            Movement();
            CurrentEnemyState(gt);
            DistanceToGeo();
            EnemyFacing();
            Animation(gt);
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

        protected override void CurrentEnemyState(GameTime gt)
        {
            switch (enemyState)
            {
                case EnemyState.Patrol:
                    pos += velocity;
                    PatrolTimer(gt);
                    break;
                case EnemyState.Chase:
                    pos += startVelocity * moveDir * 0.01f;

                    if (pos.X > player.Position.X)
                        enemyFacing = Facing.Left;
                    else if (pos.X < player.Position.X)
                        enemyFacing = Facing.Right;

                    if (distanceToPlayerX > chasingRange)
                        enemyState = EnemyState.Patrol;
                    break;
            }
        }

        public override float DistanceToGeo()
        {
            directionToGeo = pos - geodude.Position;
            return directionToGeo.Length();
        }

        protected override void PatrolTimer(GameTime gt)
        {
            randomizationTime += (float)gt.ElapsedGameTime.TotalSeconds;

            if (randomizationTime >= randomizationPeriod)
            {
                RandomDirection();
                randomizationTime = 0.0f;
            }
        }

        protected override void RandomDirection()
        {
            int random = rand.Next(0, 4);

            //Down right
            if (random == 0)
            {
                velocity.X = 1f;
                velocity.Y = 2f;
                enemyFacing = Facing.Right;
            }

            //Down left
            if (random == 1)
            {
                velocity.X = -1f;
                velocity.Y = 2f;
                enemyFacing = Facing.Left;
            }

            //Up left
            if (random == 2)
            {
                velocity.X = -1f;
                velocity.Y = -2f;
                enemyFacing = Facing.Left;
            }

            //Up right
            if (random == 3)
            {
                velocity.X = 1f;
                velocity.Y = -2f;
                enemyFacing = Facing.Right;
            }
        }
    }
}
