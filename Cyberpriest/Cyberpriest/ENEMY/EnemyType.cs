using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class EnemyType : MovingObject
    {
        public bool isHit;
<<<<<<< Updated upstream
        public bool isActive;
        float vel;


        public EnemyType(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Point objectSize = new Point(50, 50);
            Rectangle hitBox = new Rectangle((int)pos.X, (int)pos.Y, objectSize.X, objectSize.Y);
=======
        public int healthPoints;

        public EnemyType(Texture2D tex, Vector2 pos, GameWindow window) : base(tex, pos)
        {
            healthPoints = 100;
            vel = new Vector2(1, 0);
            this.pos = pos;
>>>>>>> Stashed changes
            isActive = true;
            isHit = false;
        }

        public override void HandleCollision(GameObject other)
        {
<<<<<<< Updated upstream
            if(other is Player)
            {
                if(other.pos)
                isHit = true;

                if(isHit == true && isActive == true)
                    Player.live--; //Kanske skapa en PlayerStats i Managers för att hantera saker som currency, uppgraderingar och liv etc?
            }
=======
            vel.Y = 0;
            isGrounded = true;

            if (other is Platform)
            {
                vel.Y = 0;
                isGrounded = true;
                hitBox.Y = other.hitBox.Y - hitBox.Height;
                pos.Y = hitBox.Y;
            }

            if (other is Player)
                isHit = true;
>>>>>>> Stashed changes
        }

        public override void Update(GameTime gt)
        {
            pos += vel;
            vel.Y += gravity;

            hitBox.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
<<<<<<< Updated upstream
                sb.Draw(tex, pos, Color.White);
=======
                sb.Draw(tex, pos, Color.Red);
>>>>>>> Stashed changes
        }
    }
}