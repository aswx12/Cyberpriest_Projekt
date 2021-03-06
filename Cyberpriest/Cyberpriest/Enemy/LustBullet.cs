using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class LustBullet : Bullet
    {
        public LustBullet(Texture2D tex, Vector2 pos, Facing facing) : base(tex, pos, facing)
        {
            lifeSpan = 5f;
            isActive = true;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
            {
                sb.Draw(tex, pos, Color.White);
            }
        }
    }
}
