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
    class WingsPowerUp : StationaryObject
    {
        public static bool wingsPUActivated;

        private double activeTimer;
        private double countdown;

        public WingsPowerUp(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
           
            isActive = true;
            
            activeTimer = 10; //how long the power up is active
            countdown = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (wingsPUActivated)
            {
                countdown -= gameTime.ElapsedGameTime.TotalSeconds;
            }
           
            if (countdown <= 0f)
            {
                wingsPUActivated = false;
            }
        }

        public override void HandleCollision(GameObject other)
        {
            if (other is Player)
            {
                isActive = false;
                wingsPUActivated = true;
                countdown = activeTimer;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
                sb.Draw(tex, hitBox, Color.White);
        }
    }
}
