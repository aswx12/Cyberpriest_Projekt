using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class EnemyType : GameObject
    {
        public bool isHit;
        public bool isActive;
        float vel;


        public EnemyType(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Point objectSize = new Point(50, 50);
            Rectangle hitBox = new Rectangle((int)pos.X, (int)pos.Y, objectSize.X, objectSize.Y);
            isActive = true;
            isHit = false;
        }

        public override void HandleCollision(GameObject other)
        {
            if(other is Player)
            {
                if(other.pos)
                isHit = true;

                if(isHit == true && isActive == true)
                    Player.live--; //Kanske skapa en PlayerStats i Managers för att hantera saker som currency, uppgraderingar och liv etc?
            }
        }

        public override void Update(GameTime gt)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
                sb.Draw(tex, pos, Color.White);
        }
    }
}
