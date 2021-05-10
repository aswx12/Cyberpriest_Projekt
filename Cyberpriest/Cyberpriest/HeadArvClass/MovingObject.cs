using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    enum Facing { Left, Right, Idle, Jump };
    class MovingObject : GameObject
    {
        protected float lifeSpan;

        protected bool isGrounded;
        protected Vector2 velocity;
        protected float gravity;

        protected int frame = 0;
        protected float frameRate;

        //protected double frameTimer;
        //protected Rectangle aniRect;

        protected SpriteEffects effect;

        public MovingObject(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            //isGrounded = true;
            gravity = 1f;

            effect = SpriteEffects.None;
            frameRate = 0.2f;
        }

        public override void HandleCollision(GameObject other)
        {
            hitBox.Y = other.hitBox.Y - hitBox.Height;
            pos.Y = hitBox.Y;
        }

        public override void Update(GameTime gt)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }
    }
}
