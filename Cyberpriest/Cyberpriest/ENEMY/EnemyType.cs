using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    enum EnemyState { Chase, Patrol };

    class EnemyType : MovingObject
    {
        protected Vector2 startVelocity;

        protected Player player;

        protected Facing enemyFacing;

        protected EnemyState enemyState;

        public int healthPoints;
        protected Vector2 moveDir;

        protected int chasingRange;
        protected int distanceToPlayerY;
        protected int distanceToPlayerX;

        protected float randomizationTime;
        protected float randomizationPeriod;

        protected PokemonGeodude geodude;
        public Vector2 directionToGeo;

        public EnemyType(Texture2D tex, Vector2 pos/*, GameWindow window*/, PokemonGeodude geodude) : base(tex, pos)
        {
            this.geodude = geodude;
        }

        public override void HandleCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gt)
        {

        }

        public virtual float DistanceToGeo()
        {
            directionToGeo = pos - geodude.Position;
            return directionToGeo.Length();
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
                sb.Draw(tex, pos, srRect, Color.White, 0, Vector2.Zero, 1, effect, 1);
        }


        protected virtual void CurrentEnemyState(GameTime gt)
        {
            switch (enemyState)
            {
                case EnemyState.Patrol:
                    pos += velocity;
                    PatrolTimer(gt);
                    break;
                case EnemyState.Chase:
                    int facingOffset = 5;

                    if (player.Position.X > pos.X + facingOffset)
                    {
                        pos.X += startVelocity.X;
                        enemyFacing = Facing.Right;
                    }
                    else if (player.Position.X < pos.X - facingOffset)
                    {
                        pos.X -= startVelocity.X;
                        enemyFacing = Facing.Left;
                    }

                    if (distanceToPlayerX > chasingRange)
                        enemyState = EnemyState.Patrol;
                    break;
            }
        }

        protected virtual void PatrolTimer(GameTime gt)
        {
            randomizationTime += (float)gt.ElapsedGameTime.TotalSeconds;

            if (randomizationTime >= randomizationPeriod)
            {
                RandomDirection();
                randomizationTime = 0.0f;
            }
        }

        protected void EnemyFacing()
        {
            if (enemyFacing == Facing.Left)
            {
                effect = SpriteEffects.None;
            }
            else if (enemyFacing == Facing.Right)
            {
                effect = SpriteEffects.FlipHorizontally;
            }

        }

        protected virtual void RandomDirection()
        {
            int random = rand.Next(0, 2);

            //Left
            if (random == 0)
            {
                velocity.X = -1f;
                enemyFacing = Facing.Left;
            }

            //Right
            if (random == 1)
            {
                velocity.X = 1f;
                enemyFacing = Facing.Right;
            }
        }

        public float Velocity
        {
            get
            {
                return velocity.X;
            }
            set
            {
                velocity.X = value;
            }
        }
    }
}