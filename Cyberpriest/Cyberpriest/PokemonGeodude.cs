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

        int distanceToPlayerX;
        int distanceToEnemyX;
        int chasingRange = 50;

        bool isAttacking;

        Vector2 direction;
        Vector2 goal;
        int towerRange = 100;
        float speed = 2f;

        public PokemonGeodude(Texture2D tex, Vector2 pos, PowerUp powerUp, Player player, List<EnemyType> enemyList) : base(tex, pos)
        {
            this.powerUp = powerUp;
            this.player = player;
            this.enemyList = enemyList;

            isAttacking = false;

            geodudeState = GeodudeState.Follow;

            velocity = new Vector2(0, 0);
            moveDir = new Vector2(50, 50);

            frameInterval = 100;
            spritesFrame = 6;
            srRect = new Rectangle(0, 0, tex.Width / 6, tex.Height);
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("IS IT ATTACKING? " + isAttacking);
            //Console.WriteLine("Enemy: " + FindClosestTarget());

            Console.WriteLine("State: " + geodudeState);
            if (powerUp.poweredUp)
            {
                velocity = new Vector2(4, 4);
            }

            Animation(gameTime);
            CurrentState();
            //Movement();
            //Test();

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width / 6, tex.Height);

            //distanceToPlayerX = (int)player.Position.X - (int)pos.X;

       
        }

        public void Test()
        {
            foreach (EnemyType ene in enemyList)
            {
                goal = ene.Position;
                direction = goal - pos;

                float distance = direction.Length();

                direction.Normalize();

                if (distance < towerRange && isActive == true && ene.isActive)
                {
                    pos += speed * direction;

                }
                else
                {
                    geodudeState = GeodudeState.Follow;
                }
            }
        }

        //private void FindClosestTarget()
        //{
        //    float shortestDistance = 100;

        //    foreach (EnemyType enemy in enemyList)
        //    {
        //        float distance = Vector2.Distance(pos, enemy.Position);

        //        if (distance < shortestDistance)
        //        {
        //            shortestDistance = distance;
        //        }
        //    }

        //    return closestEnemy;
        //}

        //public EnemyType FindClosestTarget()
        //{
        //    if (enemyList.Count == 0)
        //    {
        //        return null;
        //    }

        //    EnemyType closest;

        //    if (enemyList.Count == 1)
        //    {
        //        closest = enemyList.ElementAt<EnemyType>(0);
        //        return closest;
        //    }

        //    closest = enemyList.OrderBy(o => (o.Position - pos).LengthSquared()).First();

        //    return closest;
        //}

        private void Movement()
        {
            if (distanceToPlayerX < 0)
                distanceToPlayerX = distanceToPlayerX * -1;

            //EnemyType enemy = FindClosestTarget();
            foreach (EnemyType enemy in enemyList)
            {
                if (enemy.distanceToGeodudeX < chasingRange)
                {
                    moveDir = enemy.Position - pos;
                    geodudeState = GeodudeState.Attack;
                }
                else if (enemy.distanceToGeodudeX > chasingRange)
                {
                    moveDir = player.Position - pos;
                    geodudeState = GeodudeState.Follow;
                }
            }


        }

        private void CurrentState()
        {
            //EnemyType enemy = FindClosestTarget();

            switch (geodudeState)
            {
                case GeodudeState.Follow:
                    pos += velocity * moveDir * 0.01f;
                    Movement();
                    foreach (EnemyType ene in enemyList)
                    {
                        //distanceToEnemyX = ((int)ene.Position.X - (int)pos.X);
                        
                        
                        if (ene.distanceToGeodudeX <= chasingRange && ene.isActive)
                            geodudeState = GeodudeState.Attack;

                        Console.WriteLine("Distance: " + ene.distanceToGeodudeX);
                    }
                   

                    break;
                case GeodudeState.Attack:
                    //pos += velocity * moveDir * 0.01f;
                    //Test();
                    //if (distanceToEnemyX > chasingRange)
                    //{
                    //    isAttacking = false;
                    //    geodudeState = GeodudeState.Follow;
                    //}
                    Console.WriteLine("In attack");
                    break;
            }
        }

        public override void Animation(GameTime gameTime)
        {
            base.Animation(gameTime);
        }

        public override void HandleCollision(GameObject other)
        {
            if (other is EnemyType)
                isAttacking = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (powerUp.poweredUp) 
                spriteBatch.Draw(tex, hitBox, srRect, Color.White);
        }
    }
}