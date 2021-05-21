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
        public EnemySkeleton(Texture2D tex, Vector2 pos/*, GameWindow window*/, Player player, PokemonGeodude geodude) : base(tex, pos, geodude)
        {
            this.player = player;
            startPos = pos;
            isActive = true;
            isHit = false;
            healthPoints = 100;
            effect = SpriteEffects.None;
            enemyState = EnemyState.Patrol;
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

            if (other is PokemonGeodude)
            {
                healthPoints -= 100;
                isActive = false;
            }
                
        }

        public override void Update(GameTime gt)
        {
            float i = 0.75f;
            pos.Y += gravity * i;

            if (healthPoints <= 0)
            {
                isActive = false;
            }

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            distanceToPlayerX = (int)player.Position.X - (int)pos.X;
            distanceToPlayerY = (int)player.Position.Y - (int)pos.Y;

            EnemyFacing();
            Movement();
            CurrentEnemyState(gt);
            DistanceToGeo();
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

        public override float DistanceToGeo()
        {
            directionToGeo = pos - geodude.Position;
            return directionToGeo.Length();
        }

        protected override void CurrentEnemyState(GameTime gt)
        {
            switch (enemyState)
            {
                case EnemyState.Patrol:
                    pos += velocity;

                    if (pos.X >= startPos.X + 75)
                    {
                        velocity = -startVelocity;
                        enemyFacing = Facing.Left;
                    }
                    else if (pos.X <= startPos.X - 75)
                    {
                        velocity = startVelocity;
                        enemyFacing = Facing.Right;
                    }
                    //PatrolTimer(gt);
                    break;

                case EnemyState.Chase:
                    if (player.Position.X > pos.X)
                    {
                        pos.X += startVelocity.X;
                        enemyFacing = Facing.Right;
                    }
                    else if (player.Position.X < pos.X)
                    {
                        pos.X -= startVelocity.X;
                        enemyFacing = Facing.Left;
                    }

                    if (distanceToPlayerX > chasingRange)
                        enemyState = EnemyState.Patrol;
                    break;
            }
        }
    }
}

