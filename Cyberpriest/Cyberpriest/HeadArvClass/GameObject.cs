using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    public abstract class GameObject
    {
        protected Vector2 pos;
        protected Texture2D tex;
        protected Random rand;
        protected Rectangle srRect;
        protected Point tileSize;
       
        public Rectangle hitBox;
        public bool isActive = true;
        public bool isHit = false;

        public GameObject(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
            tileSize = new Point(tex.Width, tex.Height);
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tileSize.X, tileSize.Y);
        }

        public abstract void HandleCollision(GameObject other);

        public abstract void Update(GameTime gt);

        public abstract void Draw(SpriteBatch sb);

        public virtual bool IntersectCollision(GameObject other)
        {
            return hitBox.Intersects(other.hitBox);
        }       

        public bool PixelCollision(GameObject other)
        {
            Color[] dataA = new Color[tex.Width * tex.Height];
            tex.GetData(dataA);

            Color[] dataB = new Color[other.tex.Width * other.tex.Height];
            other.tex.GetData(dataB);

            int top = Math.Max(hitBox.Top, other.hitBox.Top);
            int bottom = Math.Min(hitBox.Bottom, other.hitBox.Bottom);
            int left = Math.Max(hitBox.Left, other.hitBox.Left);
            int right = Math.Min(hitBox.Right, other.hitBox.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = dataA[(x - hitBox.Left) + (y - hitBox.Top) * hitBox.Width];
                    Color colorB = dataB[(x - other.hitBox.Left) + (y - other.hitBox.Top) * other.hitBox.Width];

                    if (colorA.A != 0 && colorB.A != 0)//(colorA.A + colorB.A > 200)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public virtual Rectangle GetHitBox
        {
            get
            {
                return hitBox;
            }
        }

        public virtual Vector2 GetPos
        {
            get
            {
                return pos;
            }
        }
    }
}
