﻿using System;
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

        protected EnemyState enemyState;

        protected int healthPoints;
        protected Vector2 moveDir;

        protected int chasingRange;
        protected int distanceToPlayerY;
        protected int distanceToPlayerX;

        protected float randomizationTime;
        protected float randomizationPeriod;

        protected double hitTimer;

        public EnemyType(Texture2D tex, Vector2 pos, GameWindow window) : base(tex, pos)
        {
            healthPoints = 100;
            velocity = new Vector2(1, 0);
            this.pos = pos;
            isActive = true;
            isHit = false;
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

            if(other is Bullet)
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

            pos += velocity;
            velocity.Y += gravity;

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
                sb.Draw(tex, pos, Color.White);
        }
    }
}