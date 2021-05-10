using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    enum EnemyState { Chase, Patrol };

    class EnemyType : MovingObject
    {
        protected Vector2 startVelocity;

        protected Player player;

        protected EnemyState enemyState;

        protected int healthPoints;
        protected Vector2 moveDir;

        protected int chasingRange;
        protected int distanceToPlayerY;
        protected int distanceToPlayerX;

        protected float randomizationTime;
        protected float randomizationPeriod;

        public EnemyType(Texture2D tex, Vector2 pos/*, GameWindow window*/) : base(tex, pos)
        {

        }

        public override void HandleCollision(GameObject other)
        {

        }

        public override void Update(GameTime gt)
        {

        }

        public override void Draw(SpriteBatch sb)
        {

        }
    }
}