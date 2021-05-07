using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class AssetManager
    {
        #region Platform

        public static Texture2D smallVTile;
        public static Texture2D largeVTile;
        public static Texture2D smallPlatform;
        public static Texture2D bigSquare;
        public static Texture2D tallPlatform;
        public static Texture2D squarePlatform;
        public static Texture2D longPlatform;

        #endregion

        #region Player

        public static Texture2D player;

        #endregion

        #region Enemy

        public static Texture2D enemyGhost;
        public static Texture2D enemyDemon;
        public static Texture2D enemySkeleton;
        public static Texture2D bossCleopatra;
        public static Texture2D bossCerberus;

        #endregion

        #region Stationary Enemy

        public static Texture2D purpleFire;
        public static Texture2D redFire;

        #endregion

        #region PowerUp

        public static Texture2D item;
        public static Texture2D redWhitepil;
        public static Texture2D orangeBlackPil;
        public static Texture2D magnet;
        public static Texture2D chest;
        public static Texture2D boots;
        public static Texture2D energy;
        public static Texture2D gloves1;
        public static Texture2D gloves2;
        public static Texture2D wing;
        public static Texture2D crate;
        public static Texture2D mysteryBox;
        public static Texture2D pokeball;
        public static Texture2D specialPokeball;
        public static Texture2D avocado;
        public static Texture2D thugLifeSmoke;
        public static Texture2D thugLife;
        public static Texture2D bomb;
        public static Texture2D smallBomb;
        public static Texture2D star1;
        public static Texture2D star2;
        public static Texture2D emeraldEye;
        public static Texture2D rubyEye;
        public static Texture2D diamond;
        public static Texture2D diamond2;

        #endregion

        #region Armor

        public static Texture2D silverBoots;
        public static Texture2D silverChestplate;
        public static Texture2D silverHelmet;
        public static Texture2D silverPants;

        #endregion

        #region Weapon

        public static Texture2D bloodySword;
        public static Texture2D blueSword;
        public static Texture2D redSword;
        public static Texture2D semiAutomaticPistolVer1;
        public static Texture2D semiAutomaticPistolVer2; 
        public static Texture2D semiAutomaticPistolVer3; 
        public static Texture2D shieldBlue;
        public static Texture2D shieldRed;

        #endregion

        #region Background

        //public static Texture2D inventoryBG;
        public static Texture2D backgroundLvl1;
        public static Texture2D inventorySlot;

        #endregion 

        #region HUD

        public static Texture2D heartSprite;
        public static Texture2D coinSprite;

        #endregion

        #region Font

        public static SpriteFont normalFont;
        public static SpriteFont selectedFont;

        #endregion

        public static void LoadAssets(ContentManager content)
        {
            #region Platfrom

            smallVTile = content.Load<Texture2D>("SmallVTile");
            largeVTile = content.Load<Texture2D>("LargeVTile");
            smallPlatform = content.Load<Texture2D>("SmallPlatform");
            bigSquare = content.Load<Texture2D>("BigSquare");
            tallPlatform = content.Load<Texture2D>("TallPlatform");
            squarePlatform = content.Load<Texture2D>("SquarePlatform");
            longPlatform = content.Load<Texture2D>("LongPlatform");

            #endregion

            #region Player

            player = content.Load<Texture2D>("player1");

            #endregion

            #region Enemy
         
            enemyGhost = content.Load<Texture2D>("Enemy1");
            enemyDemon = content.Load<Texture2D>("enemy2");
            enemySkeleton = content.Load<Texture2D>("enemy3");
            bossCleopatra = content.Load<Texture2D>("Boss_Lust");
            bossCerberus = content.Load<Texture2D>("Boss_Gluttony");

            #endregion

            #region Stationary Enemy

            purpleFire = content.Load<Texture2D>("PurpleFireSprite");
            redFire = content.Load<Texture2D>("RedFireSprite");

            #endregion

            #region PowerUp

            redWhitepil = content.Load<Texture2D>("l0_sprite_01");
            orangeBlackPil = content.Load<Texture2D>("l0_sprite_02");
            magnet = content.Load<Texture2D>("l0_sprite_03");
            chest = content.Load<Texture2D>("l0_sprite_04");
            boots = content.Load<Texture2D>("l0_sprite_05");
            energy = content.Load<Texture2D>("l0_sprite_06");
            gloves1 = content.Load<Texture2D>("l0_sprite_07");
            gloves2 = content.Load<Texture2D>("l0_sprite_08");
            wing = content.Load<Texture2D>("l0_sprite_09");
            crate = content.Load<Texture2D>("l0_sprite_10");
            mysteryBox = content.Load<Texture2D>("l0_sprite_11");
            pokeball = content.Load<Texture2D>("l0_sprite_12");
            specialPokeball = content.Load<Texture2D>("l0_sprite_13");
            avocado = content.Load<Texture2D>("l0_sprite_14");
            thugLifeSmoke = content.Load<Texture2D>("l0_sprite_15");
            thugLife = content.Load<Texture2D>("l0_sprite_16");
            bomb = content.Load<Texture2D>("l0_sprite_17");
            smallBomb = content.Load<Texture2D>("l0_sprite_18");
            star1 = content.Load<Texture2D>("l0_sprite_19");
            star2 = content.Load<Texture2D>("l0_sprite_20");
            emeraldEye = content.Load<Texture2D>("l0_sprite_21");
            rubyEye = content.Load<Texture2D>("l0_sprite_22");
            diamond = content.Load<Texture2D>("l0_sprite_23");
            diamond2 = content.Load<Texture2D>("l0_sprite_24");
            item = content.Load<Texture2D>("PotionsSprite");

            #endregion

            #region Armor

            silverBoots = content.Load<Texture2D>("Armor_Boots");
            silverChestplate = content.Load<Texture2D>("Armor_Chestplate");
            silverHelmet = content.Load<Texture2D>("Armor_Helmet");
            silverPants = content.Load<Texture2D>("Armor_Pants");

            #endregion

            #region Weapon

            bloodySword = content.Load<Texture2D>("Bloody_Sword");
            blueSword = content.Load<Texture2D>("Blue_Sword");
            redSword = content.Load<Texture2D>("Red_Sword");
            semiAutomaticPistolVer1 = content.Load<Texture2D>("Gun_Ver1");
            semiAutomaticPistolVer2 = content.Load<Texture2D>("Gun_Ver2");
            semiAutomaticPistolVer3 = content.Load<Texture2D>("Gun_Ver3");
            shieldBlue = content.Load<Texture2D>("Shield1");
            shieldRed = content.Load<Texture2D>("Shield2");

            #endregion

            #region Background

            inventoryBG = content.Load<Texture2D>("hole");
            backgroundLvl1 = content.Load<Texture2D>("BG_Lvl_1");
            inventorySlot = content.Load<Texture2D>("InventorySlot");

            #endregion 

            #region HUD

            heartSprite = content.Load<Texture2D>("HeartSprite");
            coinSprite = content.Load<Texture2D>("CoinSprite");

            #endregion

            #region Font

            normalFont = content.Load<SpriteFont>(@"Font\normalFont");
            selectedFont = content.Load<SpriteFont>(@"Font\selectedFont");

            #endregion
        }
    }
}
