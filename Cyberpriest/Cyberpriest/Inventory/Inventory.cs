using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class Inventory
    {
        Texture2D tex;
        Vector2 pos;

        public Inventory(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }
    }
}
