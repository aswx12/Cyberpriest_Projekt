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

        public Inventory(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            occupied = false;
        }

        public override void Update(GameTime gt)
        {

        }

        public override void HandleCollision(GameObject other)
        {

        }

        public override Rectangle GetHitBox => base.GetHitBox;

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
