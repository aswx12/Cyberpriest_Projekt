using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest.HeadArvClass
{
    class Wall : StationaryObject
    {
        public Wall(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            
        }



        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }
    }
}
