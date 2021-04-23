﻿using Microsoft.Xna.Framework;
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
        Player player;
        EnemyState enemyState;
        Vector2 startVel;

        private int chasingRange;
        private int distanceToPlayerY;
        private int distanceToPlayerX;

        private float randomizationTime;
        private float randomizationPeriod;

        public EnemyGhost(Texture2D tex, Vector2 pos, GameWindow window, Player player) : base(tex, pos, window)
        {
            this.player = player;
            isActive = true;
            tileSize.X = 128;
            healthPoints = 200;

            enemyState = Cyberpriest.EnemyState.Patrol;

            //Starting moveDir
            moveDir = new Vector2(50, 50); 
            vel = new Vector2(1, 1);
            startVel = vel;
            chasingRange = 250;

            randomizationPeriod = 2;
            rand = new Random();

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
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
            
            Movement();

            EnemyState(gt);

            Console.WriteLine("distanceToPlayerX " + distanceToPlayerX);
            Console.WriteLine("enemyState " + enemyState);
            Console.WriteLine("moveDir " + moveDir);
            Console.WriteLine("enemyState " + enemyState);
            Console.WriteLine("vel " + vel);
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
                    pos += vel * moveDir * 0.01f;
                    PatrolTimer(gt);
                    break;
                case Cyberpriest.EnemyState.Chase:
                    pos += startVel * moveDir * 0.01f;
                    if (distanceToPlayerX > chasingRange)
                        enemyState = Cyberpriest.EnemyState.Patrol;
                    break;
            }
        }

        void RandomDirection()
        {
            int random = rand.Next(0, 4);

            //Down right
            if (random == 0) 
            {
                vel.X = 1f;
                vel.Y = 2f;
            }

            //Down left
            if (random == 1)
            {
                vel.X = -1f; 
                vel.Y = 2f;
            }

            //Up left
            if (random == 2) 
            {
                vel.X = -1f;
                vel.Y = -2f;
            }

            //Up right
            if (random == 3) 
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
                    sb.Draw(tex, pos, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 1);
                else
                    sb.Draw(tex, pos, Color.Red);
        }

    }
}
