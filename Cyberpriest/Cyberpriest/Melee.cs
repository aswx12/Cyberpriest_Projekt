using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class Melee : GameObject
    {
        
        public Melee(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
        }

        public override void HandleCollision(GameObject other)
        {
            if (other is EnemyType && other.isActive && isActive)
            {
                (other as EnemyType).healthPoints -= 50;
            }
        }

        public override void Update(GameTime gt)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
        }

        public void Draw(SpriteBatch sb,Vector2 pos,Facing facing,SpriteEffects effect)
        {
            this.pos = pos; 
            if (isActive)
            {
                if (facing == Facing.Right)
                    sb.Draw(AssetManager.redSword, pos + new Vector2(20, 10), null, Color.White, 0, Vector2.Zero, 1, effect, 0);
                else if (facing == Facing.Left)
                    sb.Draw(AssetManager.redSword, pos - new Vector2(20, -10), null, Color.White, 0, Vector2.Zero, 1, effect, 0);
            }
        }       
    }
}
