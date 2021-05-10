using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class Bullet : MovingObject
    {
        private float timer;
        private bool bulletTravelRight = true;

        public Bullet(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            
            lifeSpan = 5f;
            isActive = false;
            velocity = new Vector2(6, 0);
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
        }

        public override void HandleCollision(GameObject other)
        {
            hitBox.Y = other.hitBox.Y - hitBox.Height;
            pos.Y = hitBox.Y;
        }

        public override void Update(GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (timer > lifeSpan)
            {
                isActive = false;
            }

            hitBox.X = (int)pos.X;
            hitBox.Y = (int)pos.Y;

            if (Player.playerFacing == Facing.Right)
                bulletTravelRight = true;

            if (Player.playerFacing == Facing.Left)
                bulletTravelRight = false;

            if(bulletTravelRight == true)
            {
                pos += velocity;
            }
            
            if(bulletTravelRight == false)
            {
                pos -= velocity;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if(isActive == true)
            {
                sb.Draw(tex, pos, Color.White);
            }
        }

        public override Vector2 GetPos
        {
            get
            {
                return pos;
            }
        }
    }
}
