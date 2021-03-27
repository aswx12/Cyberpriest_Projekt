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

        public bool isCollected;
        public Vector2 slotPos;
        public int itemId;
        //public List<GameObject> inventory;
        public Inventory[,] inventory;
        public int row, column;

        public Item(int itemId,Texture2D tex, Vector2 pos, Inventory[,] inventoryTest) : base(tex, pos)
        {
            tileSize = new Point(64, 64);
            isActive = true;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            
            isCollected = false;
            //this.slotPos = slotPos;
            this.inventory = inventoryTest;
            this.itemId = itemId;
           //this.inventory = inventory;
           if(itemId ==0)
                srRect = new Rectangle(tileSize.X * 0, tileSize.Y * 0, tileSize.X, tileSize.Y);
            else if (itemId == 1)
                srRect = new Rectangle(tileSize.X * 1, tileSize.Y * 1, tileSize.X, tileSize.Y);
            else if (itemId == 2)
                srRect = new Rectangle(tileSize.X * 2, tileSize.Y * 2, tileSize.X, tileSize.Y);

        }

        public override void Update(GameTime gameTime)
        {
            if (!isActive)
                isCollected = true;
      
            Console.WriteLine(row);
        }

        public override void HandleCollision(GameObject other)
        {

        }

        public Vector2 SetSlotPos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
        }

        public Texture2D GetTexture
        {
            get
            {
                return tex;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
                sb.Draw(tex, pos, srRect, Color.White);
        }

        public void DrawInInventory(SpriteBatch sb,int row,int column)//, Vector2 itemPos
        {
            this.row = row;
            this.column = column;
            pos = inventory[row, column].GetSlotPos;
            if (isCollected)
                sb.Draw(tex, pos, srRect, Color.White);
        }
    }
}

