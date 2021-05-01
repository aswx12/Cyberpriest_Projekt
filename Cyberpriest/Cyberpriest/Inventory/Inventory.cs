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

        public bool occupied;
        int inventorySlotNr;
        public Inventory(Texture2D tex, Vector2 pos,int inventorySlotNr) : base(tex,pos)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            occupied = false;
            this.inventorySlotNr = inventorySlotNr;
        }

        public override void Update(GameTime gt)
        {
           // Console.WriteLine(inventorySlotNr+ ":" +occupied);
        }

        public override void HandleCollision(GameObject other)
        {
            //Console.WriteLine(empty);
            //if (other is Item)
            //{
            //    empty = false;
            //}
            //if (other is Inventory)
            //    empty = true;
        }

        public override Rectangle GetHitBox => base.GetHitBox;

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
