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
        public static Texture2D openDoor;
        public static Texture2D closedDoor;

        public static Texture2D blockWhite;
        public static Texture2D rightTileWhite;
        public static Texture2D centerTileWhite;
        public static Texture2D leftTileWhite;
        public static Texture2D rightLTileWhite;
        public static Texture2D leftLTileWhite;
        public static Texture2D tallTileWhite;

        public static Texture2D blockBlue;
        public static Texture2D rightTileBlue;
        public static Texture2D centerTileBlue;
        public static Texture2D leftTileBlue;
        public static Texture2D rightLTileBlue;
        public static Texture2D leftLTileBlue;
        public static Texture2D tallTileBlue;

        #endregion

        #region Player

        public static Texture2D player;

        public static Texture2D playerCharmed;

        public static Texture2D idlePlayer;

        #endregion

        #region Enemy

        public static Texture2D enemyGhost;
        public static Texture2D enemyDemon;
        public static Texture2D enemySkeleton;
        public static Texture2D enemyBug;
        public static Texture2D bossCleopatra;
        public static Texture2D bossCerberus;
        public static Texture2D bossAlighiero;
        public static Texture2D bossPhlegyas;
        #endregion

        #region Trap

        public static Texture2D purpleFire;
        public static Texture2D redFire;

        public static Texture2D purpleFireTest;

        #endregion

        #region PowerUp

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
        public static Texture2D pokemonGeodude;

        #endregion

        #region PickUp

        public static Texture2D item;
        public static Texture2D coinSprite;
        public static Texture2D coin;

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

        public static Texture2D inventoryBG;
        public static Texture2D shopBG;
        public static Texture2D backgroundLvl1;
        public static Texture2D inventorySlot;

        #endregion 

        #region HUD

        public static Texture2D heartSprite;
        public static Texture2D fullHealthbar;
        public static Texture2D emptyHealthbar;

        #endregion

        #region Key

        public static Texture2D keySprite;

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
            openDoor = content.Load<Texture2D>("OpenDoor");
            closedDoor = content.Load<Texture2D>("ClosedDoor");

            blockWhite = content.Load<Texture2D>("BlockWhite");
            leftLTileWhite = content.Load<Texture2D>("LeftLTileWhite");
            rightLTileWhite = content.Load<Texture2D>("RightLTileWhite");
            rightTileWhite = content.Load<Texture2D>("RightTileBlue");
            centerTileWhite = content.Load<Texture2D>("CenterTileWhite");
            leftTileWhite = content.Load<Texture2D>("LeftTileBlue");
            tallTileWhite = content.Load<Texture2D>("TallTileWhite");

            blockBlue = content.Load<Texture2D>("BlockBlue");
            leftLTileBlue = content.Load<Texture2D>("LeftLTileBlue");
            rightLTileBlue = content.Load<Texture2D>("RightLTileBlue");
            rightTileBlue = content.Load<Texture2D>("RightTileBlue");
            centerTileBlue = content.Load<Texture2D>("CenterTileBlue");
            leftTileBlue = content.Load<Texture2D>("LeftTileBlue");
            tallTileBlue = content.Load<Texture2D>("TallTileBlue");
            #endregion

            #region Player

            player = content.Load<Texture2D>("Player1");

            playerCharmed = content.Load<Texture2D>("PlayerCharmed");

            idlePlayer = content.Load<Texture2D>("idlePlayer");

            #endregion

            #region Enemy

            enemyGhost = content.Load<Texture2D>("Enemy1");
            enemyDemon = content.Load<Texture2D>("enemy2");
            enemySkeleton = content.Load<Texture2D>("enemy3");
            enemyBug = content.Load<Texture2D>("enemy4");
            bossCleopatra = content.Load<Texture2D>("Boss_Lust");
            bossCerberus = content.Load<Texture2D>("Boss_Gluttony");
            bossAlighiero = content.Load<Texture2D>("Boss_Greed");
            bossPhlegyas = content.Load<Texture2D>("Boss_Phlegyas");

            #endregion

            #region Trap

            purpleFire = content.Load<Texture2D>("PurpleFireSprite");
            redFire = content.Load<Texture2D>("RedFireSprite");

            purpleFireTest = content.Load<Texture2D>("PurpleFireSpriteTest");

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
            pokemonGeodude = content.Load<Texture2D>("Pokemon Geodude");

            #endregion

            #region PickUp

            item = content.Load<Texture2D>("PotionsSprite");
            coinSprite = content.Load<Texture2D>("CoinSprite");
            coin = content.Load<Texture2D>("coin");

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

            inventoryBG = content.Load<Texture2D>("InventoryBG");
            shopBG = content.Load<Texture2D>("ShopBG");
            backgroundLvl1 = content.Load<Texture2D>("BG_Lvl_1");
            inventorySlot = content.Load<Texture2D>("InventorySlot");

            #endregion 

            #region HUD

            heartSprite = content.Load<Texture2D>("HeartSprite");
            fullHealthbar = content.Load<Texture2D>("FullHealthbar");
            emptyHealthbar = content.Load<Texture2D>("EmptyHealthbar");

            #endregion

            #region Key

            keySprite = content.Load<Texture2D>("Key");

            #endregion

            #region Font

            normalFont = content.Load<SpriteFont>(@"Font\normalFont");
            selectedFont = content.Load<SpriteFont>(@"Font\selectedFont");

            #endregion
        }
    }
}
