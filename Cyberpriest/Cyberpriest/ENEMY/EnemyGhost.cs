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
        EnemyState enemyState;
        int distanceToPlayerY;
        int distanceToPlayerX;
        Player player;
        Vector2 startVel;
        private float randomizationTime;
        private float randomizationPeriod = 2;
  

        public EnemyGhost(Texture2D tex, Vector2 pos, GameWindow window, Player player) : base(tex, pos, window)
        {
            tileSize.X = 128;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            rand = new Random();
            this.player = player;
            isActive = true;
            healthPoints = 200;
            vel = new Vector2(1, 1);
            startVel = vel;
        }

        public override void HandleCollision(GameObject other)
        {
        }

        public override void Update(GameTime gt)
        {
            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);

            distanceToPlayerX = (int)player.GetPos.X - (int)pos.X;
            distanceToPlayerY = (int)player.GetPos.Y - (int)pos.Y;

            if (distanceToPlayerX < 0)
                distanceToPlayerX = distanceToPlayerX * -1;
                

                if (distanceToPlayerX < 250)
                {
                    moveDir = player.GetPos - pos;
                    enemyState = EnemyState.Chase;
                }
                else
                {
                    enemyState = EnemyState.Patrol;
                }

            switch (enemyState)
            {
                case EnemyState.Patrol:
                    pos += vel * moveDir * 0.01f;
                    PatrolTimer(gt);
                    break;
                case EnemyState.Chase:
                    pos += startVel * moveDir * 0.01f;
                    if (distanceToPlayerX > 250)
                        enemyState = EnemyState.Patrol;

                    break;
            }

            Console.WriteLine("distanceToPlayerX " + distanceToPlayerX);
            Console.WriteLine("enemyState " + enemyState);
            Console.WriteLine("moveDir " + moveDir);
            Console.WriteLine("enemyState " + enemyState);
            Console.WriteLine("vel " + vel);
        }

        void RandomDirection()
        {
            int random = rand.Next(0, 4);

            if(random == 0) //Down right
            {
                vel.X = 1f;
                vel.Y = 2f;
            }

            if (random == 1) //Down left
            {
                vel.X = -1f; 
                vel.Y = 2f;
            }

            if (random == 2) //Up left
            {
                vel.X = -1f;
                vel.Y = -2f;
            }

            if (random == 3) //Up right
            {
                vel.X = 1f;
                vel.Y = -2f;
            }
        }

        void PatrolTimer(GameTime gt)
        {
            randomizationTime += (float)gt.ElapsedGameTime.TotalSeconds;

            if(randomizationTime >= randomizationPeriod)
            {
                RandomDirection();
                randomizationTime = 0.0f;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
                if(moveDir.X < 0)
                {
                    sb.Draw(tex, pos, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 1);
                }
                else
                    sb.Draw(tex, pos, Color.Red);
        }

    }
}
