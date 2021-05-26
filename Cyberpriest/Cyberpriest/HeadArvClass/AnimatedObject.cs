using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class AnimatedObject : GameObject
    {
        int frame;
        double frameTimer;
        protected double frameInterval;
        protected int spritesFrame;
        int spriteSize;

        public AnimatedObject(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            spriteSize = 64;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void HandleCollision(GameObject other)
        {
            throw new NotImplementedException();
        }

        public virtual void Animation(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srRect.X = (frame % spritesFrame) * spriteSize;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, hitBox, srRect, Color.White);
        }
    }
}
