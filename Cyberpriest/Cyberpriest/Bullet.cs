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
        Bullet bullet;
        private float timer;

        public Bullet(Texture2D tex, Vector2 pos, Player player) : base(tex, pos)
        {
            pos = player.GetPos;
            //origin = new Vector2(tex.Width / 2, tex.Height / 2);
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
                isRemoved = true;
            }

            pos += velocity;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
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
