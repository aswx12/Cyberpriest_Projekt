using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class Item : StationaryObject
    {
        public bool isCollected;
        public int itemId;
        public Inventory[,] inventory;
        public int row, column;

        public Item(int itemId, Texture2D tex, Vector2 pos, Inventory[,] inventory) : base(tex, pos)
        {
            tileSize = new Point(64, 64);
            isActive = true;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);

            isCollected = false;

            this.inventory = inventory;
            this.itemId = itemId;

            if (itemId == 0)
                srRect = new Rectangle(tileSize.X * 0, tileSize.Y * 0, tileSize.X, tileSize.Y);
            else if (itemId == 1)
                srRect = new Rectangle(tileSize.X * 1, tileSize.Y * 1, tileSize.X, tileSize.Y);
            else if (itemId == 2)
                srRect = new Rectangle(tileSize.X * 2, tileSize.Y * 2, tileSize.X, tileSize.Y);

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void HandleCollision(GameObject other)
        {
            if(other is Inventory)
            {
                (other as Inventory).empty = false;
            }
                
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

        public Rectangle getHitbox
        {
            get
            {
                return hitBox;
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

        public void DrawInInventory(SpriteBatch sb)//, int row, int column)//, Vector2 itemPos
        {
            //this.row = row;
            //this.column = column;
            pos = inventory[row, column].GetSlotPos;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            //if (isCollected)
                sb.Draw(tex, pos, srRect, Color.White);
        }
    }
}

