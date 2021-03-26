using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class Item : Inventory
    {
        Random rand = new Random();

        public bool isCollected;
 
        public List<GameObject> inventory;
        public Item(Texture2D tex, Vector2 pos, List<GameObject> invetory) : base(tex, pos)
        {
            tileSize = new Point(64, 64);
            isActive = true;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            srRect=new Rectangle(tileSize.X * 0, tileSize.Y * 0, tileSize.X, tileSize.Y);
            isCollected = false;
            this.inventory = invetory;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isActive)
                isCollected = true;
        }

        public override void HandleCollision(GameObject other)
        {

       

        }

        //public Vector2 GetItemPos
        //{
        //    get
        //    {
        //        return itemPos;
        //    }

        //    set
        //    {
        //        itemPos = value;
        //    }
        //}

        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
                sb.Draw(tex, pos, srRect, Color.White);
        }

        public void DrawInInventory(SpriteBatch sb, Vector2 itemPos)
        {
            foreach(Item item in inventory)
            {
                if (isCollected)
                    sb.Draw(tex, itemPos, srRect, Color.White);
            }
        }
    }
}

