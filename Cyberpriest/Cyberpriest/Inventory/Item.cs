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
        public Item(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            tileSize = new Point(64, 64);
            isActive = true;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            isCollected = false;
            srRect = new Rectangle(tileSize.X * 0, tileSize.Y * 0, tileSize.X, tileSize.Y);

        }

        public void Update()
        {

        }


        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
                sb.Draw(tex, pos, srRect, Color.White);
        }

        public void DrawInInventory(SpriteBatch sb, Vector2 itemPos)
        {
            if (isCollected)
                sb.Draw(tex, itemPos, srRect, Color.White);

        }
    }
}

