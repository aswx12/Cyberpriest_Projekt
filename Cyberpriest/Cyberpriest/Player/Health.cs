using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class Health : GameObject
    {
        public Health(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            hitBox = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void HandleCollision(GameObject other)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.emptyHealthbar, pos, Color.White);
            spriteBatch.Draw(tex, pos, hitBox, Color.White);
        }
    }
}
