using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    enum GeodudeState { Follow, Attack };

    class PokemonGeodude : MovingObject
    {
        GeodudeState geodudeState;

        Facing geoDudeFacing;

        PowerUp powerUp;
        Player player;
        List<EnemyType> enemyList;

        Vector2 moveDir;
        Vector2 direction;

        int chasingRange = 225;

        Vector2 goal;

        public PokemonGeodude(Texture2D tex, Vector2 pos, PowerUp powerUp, Player player, List<EnemyType> enemyList) : base(tex, pos)
        {
            this.powerUp = powerUp;
            this.player = player;
            this.enemyList = enemyList;

            geodudeState = GeodudeState.Follow;
            geoDudeFacing = Facing.Right;
            frameInterval = 100;
            spritesFrame = 6;
            srRect = new Rectangle(0, 0, tex.Width / 6, tex.Height);
        }

        public override void HandleCollision(GameObject other)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (powerUp.poweredUp)
            {
                velocity = new Vector2(4, 4);

                Animation(gameTime);
                GeoFacing();
                CurrentState();
                GeoStateLogic();

                hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width / 6, tex.Height);
            }           
        }

        public override void Draw(SpriteBatch sb)
        {
            if (powerUp.poweredUp)
                sb.Draw(tex, pos, srRect, Color.White, 0, Vector2.Zero, 1, effect, 1);
        }

        public void GeoAttack()
        {
            foreach (EnemyType ene in enemyList)
            {
                goal = ene.Position;
                direction = goal - pos;

                float distance = direction.Length();

                direction.Normalize();

                if (distance < chasingRange && isActive == true && ene.isActive)
                {
                    pos += velocity * direction;
                }
                else
                    geodudeState = GeodudeState.Follow;
            }
        }

        private void GeoStateLogic()
        {
            foreach (EnemyType enemy in enemyList)
            {
                if (enemy.DistanceToGeo() < chasingRange && enemy.isActive)
                {
                    geodudeState = GeodudeState.Attack;

                    if (enemy.Position.X > pos.X)
                        geoDudeFacing = Facing.Right;
                    else
                        geoDudeFacing = Facing.Left;
                }
            }
        }

        private void CurrentState()
        {
            switch (geodudeState)
            {
                case GeodudeState.Follow:
                    moveDir = player.Position - pos;
                    pos += velocity * moveDir * 0.01f;

                    if (player.Position.X > pos.X)
                        geoDudeFacing = Facing.Right;
                    else
                        geoDudeFacing = Facing.Left;
                    break;

                case GeodudeState.Attack:

                    GeoAttack();

                    break;
            }
        }

        protected void GeoFacing()
        {
            if (geoDudeFacing == Facing.Right)
            {
                effect = SpriteEffects.None;
            }
            else if (geoDudeFacing == Facing.Left)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
        }

    }
}