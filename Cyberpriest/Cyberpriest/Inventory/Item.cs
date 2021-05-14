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
        public bool inInventory;

        public Item(int itemId, Texture2D tex, Vector2 pos, Inventory[,] inventory) : base(tex, pos)
        {
            tileSize = new Point(64, 64);
            isActive = true;
            isCollected = false;
            this.inventory = inventory;
            this.itemId = itemId;
            spriteOffset = 20;
            if (itemId == 0)
                srRect = new Rectangle(tileSize.X * 0, tileSize.Y * 0, tileSize.X, tileSize.Y);
            else if (itemId == 1)
                srRect = new Rectangle(tileSize.X * 1, tileSize.Y * 1, tileSize.X, tileSize.Y);
            else if (itemId == 2)
                srRect = new Rectangle(tileSize.X * 2, tileSize.Y * 2, tileSize.X, tileSize.Y);

            hitBox = new Rectangle((int)pos.X, ((int)pos.Y- spriteOffset), tileSize.X, tileSize.Y);
        }

        public override void Update(GameTime gameTime)
        {
            if (isCollected)
            {
                pos = inventory[row, column].Position;
                inventory[row, column].occupied = inInventory;
                hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            }
        }

        public override void HandleCollision(GameObject other)
        {
            if(other is Player)
            {
                isActive = false;
                isCollected = true;
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

        public override Rectangle GetHitBox
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

        public void DrawInInventory(SpriteBatch sb)
        {
            if (isCollected)
                sb.Draw(tex, pos, srRect, Color.White);
        }
    }
}

