using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class EnemyBullet : Bullet
    {
        public EnemyBullet(Texture2D tex, Vector2 pos, Facing facing) : base(tex, pos, facing)
        {
        }
    }
}
