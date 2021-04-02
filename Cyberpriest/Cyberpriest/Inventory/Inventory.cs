using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class Inventory : StationaryObject
    {

        public bool empty;
        public Inventory(Texture2D tex, Vector2 pos) : base(tex,pos)
        {
            //hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            empty = true;
        }

        public override void HandleCollision(GameObject other)
        {
            if (other is Item)
            {
                empty = false;
                //Console.WriteLine(empty);
            }
            else
                empty = true;

            Console.WriteLine(empty);
        }

        public Vector2 GetSlotPos
        {
            get
            {
                return pos;
            }
        }

        public new void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }
    }
}
