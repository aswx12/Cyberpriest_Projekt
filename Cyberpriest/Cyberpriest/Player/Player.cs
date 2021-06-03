using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cyberpriest
{
    class Player : MovingObject
    {
        public List<Bullet> bulletList = new List<Bullet>();
        List<PowerUp> powerUpList;
        Bullet bullet;
        public Melee melee;
        public Facing playerFacing;

        int bY, bX;
        int normalVel = 3;
        int dashLength = 150;
        int jumpHeight = 12;
        int dashCount;
        int shotCount;
        int maxFallDistance = 2500;
        int flyHeight = 5;

        float timer;
        float iFrameTimer = -1;
        float hitBoxOffset = 0.5f;

        double nextBlinkTime;
        double dashTimer;
        double shotTimer;

        double affectedTimer;

        public static bool invincible;
        bool downPlatform;
        bool blinking;
        public bool charmed;
        bool canShoot;
        public static bool dead;

        public Player(Texture2D tex, Vector2 pos, GameWindow window, List<PowerUp> powerUpList) : base(tex, pos)
        {
            charmed = false;
            isGrounded = true;
            startPos = pos;
            velocity = new Vector2(0, 0);
            bY = window.ClientBounds.Height;
            bX = window.ClientBounds.Width;
            playerFacing = Facing.Right;
            this.powerUpList = powerUpList;
            srRect = new Rectangle(0, 0, tex.Width, tex.Height);
            dashCount = 1;
            dashTimer = 0;

            shotCount = 1;
            shotTimer = 0;

            affectedTimer = 0;

            isHit = false;
            dead = false;
            rand = new Random();

            melee = new Melee(AssetManager.redSword, pos);

            frameInterval = 125;
            spritesFrame = 17;

        }

        public override void HandleCollision(GameObject other)
        {
            velocity.Y = 0;
            isGrounded = true;

            if (other is LustBullet)
            {
                other.isActive = false;
                charmed = true;

                return;
            }

            if(other is RangedEnemyBullet && other.isActive == true)
            {
                if (iFrameTimer <= 0 && GameStats.health.hitBox.Width >= 0)
                {
                    GameStats.health.hitBox.Width -= 20;
                }

                isHit = true;
                Console.WriteLine(GameStats.health.hitBox.Width);
                return;
            }

            if (other is EnemyType && other.isActive == true)
            {
                if (iFrameTimer <= 0 && GameStats.health.hitBox.Width >= 0)
                {
                    GameStats.health.hitBox.Width -= 20;
                }

                isHit = true;
                Console.WriteLine(GameStats.health.hitBox.Width);
                return;
            }

            if (other is Item)
            {
                return;
            }

            if (other is PowerUp)
            {
                return;
            }

            if (other is Bullet)
            {
                return;
            }

            if (other is Coin)
            {
                GameStats.coinCollected += 1;
                return;
            }

            if (downPlatform == true && other is Platform)
            {
                hitBox.Y = other.hitBox.Y - hitBox.Height;
                pos.Y = hitBox.Y;
            }
        }

        public override void Update(GameTime gt)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, 64, 64);

            for (int j = 0; j < bulletList.Count(); j++)
            {
                if (bullet.isActive == false)
                {
                    bulletList.RemoveAt(j);
                    j--;
                }
            }

            Console.WriteLine("amount of bullets in list: " + bulletList.Count());

            Console.WriteLine("Current position: " + pos);
            //Console.WriteLine("Current mouse: " + KeyMouseReader.mouseState.X);

            if (GameStats.health.hitBox.Width <= 0 || pos.Y > maxFallDistance) //Placeholder death "method".
            {
                dead = true;
                Game1.newGame = true;
                pos = startPos;
                Game1.GetState = GameState.Menu;
                GameStats.health.hitBox.Width = AssetManager.fullHealthbar.Width;
            }

            if (gt.TotalGameTime.TotalMilliseconds >= nextBlinkTime)
            {
                blinking = !blinking;
                nextBlinkTime = gt.TotalGameTime.TotalMilliseconds + 400;
            }

            foreach (Bullet b in bulletList)
            {
                b.Update(gt);
                b.Movement();
            }

            if (isGrounded)
            {
                downPlatform = true;
            }

            float i = 0.5f;
            velocity.Y += gravity * i; //falling faster and faster

            PlayerFacing();
            Charmed(gt);
            if (charmed == false)
            {
                Control(gt);
            }

            PowerUp(gt);
            ShootCooldown(gt);
            DashCooldown(gt);
            CanShootBullet();
            IFrame(gt);

            pos += velocity;
            hitBox.X = (int)(pos.X >= 0 ? pos.X + hitBoxOffset : pos.X - hitBoxOffset);
            hitBox.Y = (int)(pos.Y >= 0 ? pos.Y + hitBoxOffset : pos.Y - hitBoxOffset);
            
            melee.Update(gt);

            GamePlayManager.levelComplete = false;

            GameStats.currentAmmo = GameStats.maxAmmo - bulletList.Count;

            if (GameStats.currentAmmo <= 0)
            {
                canShoot = false;
            }


        }

        public override void Draw(SpriteBatch sb)
        {
            if (iFrameTimer > 0)
            {
                if (blinking)
                    sb.Draw(AssetManager.player, pos, srRect, Color.White, 0, Vector2.Zero, 1, effect, 0);
            }
            else if (invincible)
            {
                sb.Draw(AssetManager.player, pos, srRect, RandomColor(), 0, Vector2.Zero, 1, effect, 0);
            }
            else if (charmed)
            {
                sb.Draw(AssetManager.playerCharmed, pos, null, Color.White, 0, Vector2.Zero, 1, effect, 0);
            }
            else
            {
                sb.Draw(AssetManager.player, pos, srRect, Color.White, 0, Vector2.Zero, 1, effect, 0);
            }

            foreach (Bullet b in bulletList)
            {
                b.Draw(sb);
            }

            melee.Draw(sb, pos, playerFacing, effect);

        }

        void CanShootBullet()
        {
            if (bulletList.Count == GameStats.maxAmmo)
            {
                canShoot = false;
            }
            else
            {
                canShoot = true;
            }
        }

        void PowerUp(GameTime gameTime)
        {
            foreach (PowerUp powerUp in powerUpList)
            {
                if (powerUp.poweredUp)
                {
                    if (powerUp.GetTexture == AssetManager.wing)
                        FlyingPowerUp();
                    else if (powerUp.GetTexture == AssetManager.boots)
                        JumpingPowerUp();
                    else if (powerUp.GetTexture == AssetManager.energy)
                        SpeedPowerUp();
                    else if (powerUp.GetTexture == AssetManager.star2)
                    {
                        invincible = true;
                        InvincibilityPowerUp(gameTime);
                    }
                }

                if (!powerUp.poweredUp)
                {
                    jumpHeight = 12;
                    if (powerUp.GetTexture == AssetManager.wing)
                        gravity = 1f;
                }
            }
        }

        public void PlayerFacing()
        {
            if (playerFacing == Facing.Right)
            {
                effect = SpriteEffects.None;
            }
            else if (playerFacing == Facing.Left)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
        }

        public void Control(GameTime gt)
        {
            if (KeyMouseReader.keyState.IsKeyDown(Keys.E) && GamePlayManager.levelComplete == true && Key.keyPickedUp == true)
            {
                GamePlayManager.levelNumber++;
                GamePlayManager.currentLevel = "level" + GamePlayManager.levelNumber.ToString();
                GamePlayManager.map = new MapParser("Content/" + GamePlayManager.currentLevel + ".txt");
                GamePlayManager.levelComplete = false;
                Key.keyPickedUp = false;
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
            {
                WalkingAnimation(gt);
                velocity.X = normalVel;

            }
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.A))// && pos.X >= startPos.X
            {
                WalkingAnimation(gt);
                velocity.X = -normalVel;
            }
            else
                velocity.X = 0;

            if (KeyMouseReader.mouseState.X >= bX / 2)
            {
                playerFacing = Facing.Right;
            }
            else if (KeyMouseReader.mouseState.X <= bX / 2)
            {
                playerFacing = Facing.Left;
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.LeftShift))
            {
                if (dashCount > 0)
                {
                    if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
                        velocity.X += dashLength;
                    if (KeyMouseReader.keyState.IsKeyDown(Keys.A))
                        velocity.X -= dashLength;

                    //save dash snapshot here
                    dashCount = 0;
                }
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Space) && isGrounded)
            {
                velocity.Y = -jumpHeight; //jump height               
                isGrounded = false;
                playerFacing = Facing.Jump;
                //srRect = new Rectangle(tileSize.X * 3, tileSize.Y * 0, tileSize.X, tileSize.Y);
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.S) && isGrounded)
            {
                velocity.Y = jumpHeight;
                isGrounded = false;
                downPlatform = false;
                playerFacing = Facing.Jump;
                //srRect = new Rectangle(tileSize.X * 3, tileSize.Y * 0, tileSize.X, tileSize.Y);
            }

            if (KeyMouseReader.LeftClick())
            {
                if (shotCount > 0 && canShoot)
                {
                    bullet = new Bullet(AssetManager.bomb, pos, playerFacing);
                    bulletList.Add(bullet);
                    bullet.isActive = true;

                    shotCount = 0;
                }
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.F))
            {
                melee.isActive = true;
            }
            else
                melee.isActive = false;

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Q))
            {
                PunchingAnimation(gt);
            }
            if (KeyMouseReader.keyState.IsKeyDown(Keys.R))
            {
                KickingAnimation(gt);
            }
        }

        #region Animation

        public void WalkingAnimation(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srRect.X = (frame % spritesFrame) * spriteSize;
                if (frame > 6)
                    frame = 1;
            }
        }

        public void PunchingAnimation(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srRect.X = (frame % spritesFrame) * spriteSize;
                if (frame < 7 || frame > 10)
                    frame = 7;
            }
        }

        public void KickingAnimation(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                srRect.X = (frame % spritesFrame) * spriteSize;
                if (frame < 11 || frame > 15)
                    frame = 11;
            }
        }


        #endregion

        public void IFrame(GameTime gameTime)
        {
            if (iFrameTimer <= 0 && isHit == true)
            {
                iFrameTimer = 3;
            }

            isHit = false;

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            iFrameTimer -= (int)timer;

            if (timer >= 1.0F)
            {
                timer = 0F;
            }
        }

        public void DashCooldown(GameTime gt)
        {
            if (dashCount <= 0)
                dashTimer += gt.ElapsedGameTime.TotalSeconds;

            double cooldown = 1.5;

            if (dashTimer >= cooldown && dashCount == 0)
            {
                dashCount = 1;
                dashTimer = 0;
            }

            Console.WriteLine(dashTimer);
        }

        public void Charmed(GameTime gt)
        {
            if (charmed == true)
                affectedTimer += gt.ElapsedGameTime.TotalSeconds;

            double charmDuration = 2;

            if (affectedTimer >= charmDuration && charmed == true)
            {
                charmed = false;
                if (charmed == false)
                    affectedTimer = 0;
            }
        }

        public void ShootCooldown(GameTime gt)
        {
            if (shotCount <= 0)
                shotTimer += gt.ElapsedGameTime.TotalSeconds;

            double cooldown = 2;

            if (shotTimer >= cooldown && shotCount == 0)
            {
                shotCount = 1;
                if (shotCount == 1)
                    shotTimer = 0;
            }
        }

        #region PowerUps

        private void FlyingPowerUp()
        {
            if (KeyMouseReader.keyState.IsKeyDown(Keys.W))
            {
                velocity.Y = -flyHeight;
                isGrounded = false;

                if (!isGrounded)
                    gravity = 0.1f;
            }
            if (KeyMouseReader.keyState.IsKeyDown(Keys.S) && !isGrounded)
            {
                velocity.Y = flyHeight;
                isGrounded = false;
            }
            if (KeyMouseReader.keyState.IsKeyDown(Keys.D) && !isGrounded)
            {
                velocity.X = flyHeight;
                isGrounded = false;
            }
            if (KeyMouseReader.keyState.IsKeyDown(Keys.A) && !isGrounded)
            {
                velocity.X = -flyHeight;
                isGrounded = false;
            }
        }

        private void JumpingPowerUp()
        {
            jumpHeight = 20;
            //jumpHeight *= 2; not working like it should
        }

        private void SpeedPowerUp()
        {
            if (KeyMouseReader.keyState.IsKeyDown(Keys.D))
            {
                velocity.X = normalVel * 2;

                playerFacing = Facing.Right;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(Keys.A))
            {
                velocity.X = -normalVel * 2;

                playerFacing = Facing.Left;
            }
        }

        private void InvincibilityPowerUp(GameTime gameTime)
        {
            GameStats.health.hitBox.Width = GameStats.health.GetTexLength;
        }

        public Color RandomColor()
        {
            Color[] Colors = new Color[] { Color.Salmon, Color.PaleGoldenrod, Color.Beige, Color.LightGreen, Color.Cyan };

            return Colors[rand.Next(Colors.Length)];
        }


        #endregion

        public override Vector2 Position
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }

        public float Velocity
        {
            get
            {
                return velocity.X;
            }
            set
            {
                velocity.X = value;
            }
        }
    }
}

/* Reusabale code
 
            //public void Cooldown(GameTime gt, int count, double cooldown, double cooldownTimer)
        //{
        //    if (count <= 0)
        //        cooldownTimer += gt.ElapsedGameTime.TotalSeconds;

        //    if (cooldownTimer >= cooldown && count == 0)
        //    {
        //        count = 1;
        //        if (count == 1)
        //            cooldownTimer = 0;
        //    }

        //    Console.WriteLine(count);
        //}

    // public void Cooldown2(GameTime gt, int count, double cooldown)
    // When we dash: dashUsedSnapshot = gt.TotalGameTime.TotalSeconds;
    // 
 
 */
