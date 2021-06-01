using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class PowerUp : StationaryObject
    {
        public bool poweredUp;
        private double activeTimer;
        private double countdown;

        public PowerUp(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            tileSize = new Point(16, 16);

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
            srRect = new Rectangle(0, 0, tex.Width / 6, tex.Height);

            isActive = true;
            poweredUp = false;
            activeTimer = 10; //how long the power up is actived
            countdown = 0;

            frameInterval = 100;
            spritesFrame = 6;
        }

        public Texture2D GetTexture
        {
            get
            {
                return tex;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (poweredUp)
            {
                countdown -= gameTime.ElapsedGameTime.TotalSeconds;
            }
           
            if (countdown <= 0f)
            {
                poweredUp = false;
                Player.invincible = false;
            }

            Animation(gameTime);

        }

        public override void HandleCollision(GameObject other)
        {
            if (other is Player)
            {
                isActive = false;
                poweredUp = true;
                countdown = activeTimer;              
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
                sb.Draw(tex, pos, srRect, Color.White);
        }
    }
}
