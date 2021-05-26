using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class StationaryObject : AnimatedObject
    {
        public StationaryObject(Texture2D tex, Vector2 pos) : base(tex,pos)
        {
   
        }

        public override void HandleCollision(GameObject other)
        {

        }

        public override void Update(GameTime gt)
        {

        }

        public override void Draw(SpriteBatch sb)
        {

        }
    }
}
