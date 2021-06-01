using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class Key : StationaryObject
    {
        public static bool keyPickedUp;

        public Key(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            this.tex = tex;
            this.pos = pos;
            keyPickedUp = false;
        }

        public override void HandleCollision(GameObject other)
        {
            if (other is Player)
            {
                isActive = false;
                keyPickedUp = true;
            }
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
