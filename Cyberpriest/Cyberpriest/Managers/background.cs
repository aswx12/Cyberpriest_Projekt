using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest.Menu
{
    class Background
    {
        Texture2D backgroundlvl1;
        public Vector2 position;

        public Background(Texture2D backgroundlvl1, int positionX, int positionY)
        {
            this.backgroundlvl1 = backgroundlvl1;
            this.position = new Vector2(positionX, positionY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundlvl1, position, null, Color.White);
        }
    }
}
