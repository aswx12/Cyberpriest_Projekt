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
        int inventorySlotNr;
        public Inventory(Texture2D tex, Vector2 pos,int inventorySlotNr) : base(tex,pos)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            empty = true;
            this.inventorySlotNr = inventorySlotNr;
        }

        public override void Update(GameTime gt)
        {
            //empty = true;
            //Console.WriteLine(empty);
            //Console.WriteLine(inventorySlotNr);
        }

        public override void HandleCollision(GameObject other)
        {
            //Console.WriteLine(empty);
            if (other is Item)
            {
                empty = false;
            }
            //if (other is Inventory)
            //    empty = true;
        }

        public override Rectangle getHitbox => base.getHitbox;

        public int GetSlotNr
        {
            get
            {
                return inventorySlotNr;
            }
        }

        public override Vector2 GetPos
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
