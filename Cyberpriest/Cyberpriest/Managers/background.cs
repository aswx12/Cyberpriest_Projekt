using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest.Menu
{
    class Background : GameObject
    {
        public Background(Texture2D tex, Vector2 pos) : base(tex,pos)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(GamePlayManager.levelNumber <= 2)
            {
                spriteBatch.Draw(tex, pos, Color.White);
            }
            else if(GamePlayManager.levelNumber > 2 && GamePlayManager.levelNumber <= 4)
            {
                spriteBatch.Draw(tex, pos, Color.MediumBlue);
            }
        }

        public override void HandleCollision(GameObject other)
        {
        }

        public override void Update(GameTime gt)
        {
        }
    }
}
