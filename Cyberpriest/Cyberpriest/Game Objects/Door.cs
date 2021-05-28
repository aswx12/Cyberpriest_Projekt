using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class Door : GameObject
    {
        bool openDoor;

        public Door(Texture2D tex, Vector2 pos, bool openDoor) : base(tex, pos)
        {
            this.openDoor = openDoor;
            this.tex = tex;
            this.pos = pos;
        }

        public override void Update(GameTime gt)
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            if(openDoor == true)
            {
                sb.Draw(AssetManager.openDoor, pos, Color.White);
            }
            else if(openDoor == false)
            {
                sb.Draw(AssetManager.closedDoor, pos, Color.White);
            }
            
        }

        public override void HandleCollision(GameObject other)
        {
            if (other is Player && openDoor == false)
            {
                GamePlayManager.levelComplete = true;
            }
        }


    }
}
