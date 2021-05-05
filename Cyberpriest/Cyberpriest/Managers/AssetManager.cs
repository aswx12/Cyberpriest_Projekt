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
        public static Texture2D player;
        public static Texture2D platform;
        public static Texture2D bg;
        public static Texture2D inventory;
        public static Texture2D item;
        public static Texture2D walltile;
        public static Texture2D enemy1;
        public static Texture2D enemy2;
        public static Texture2D purpleFire;
        public static Texture2D redFire;
        public static Texture2D shield1;
        public static Texture2D shield2;
        public static Texture2D heartSprite;
        public static Texture2D coinSprite;
        public static Texture2D tilesetSprite;
        public static Texture2D tilesetSprite2;
        public static Texture2D enemy;
        public static Texture2D smallVTile;
        public static Texture2D largeVTile;
        public static Texture2D smallPlatform;
        public static Texture2D bigSquare;
        public static Texture2D tallPlatform;
        public static Texture2D squarePlatform;
        public static Texture2D longPlatform;
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
        public static Texture2D armorBoots;
        public static Texture2D armorChestplate;
        public static Texture2D armorHelmet;
        public static Texture2D armorPants;
        public static Texture2D bloodySword;
        public static Texture2D blueSword;
        public static Texture2D gunVer1;
        public static Texture2D gunVer2;
        public static Texture2D gunVer3;
        public static Texture2D knife;
        public static Texture2D redSword;
        public static Texture2D bgLVL1;
        //public static Texture2D 
        //public static Texture2D

        public static SpriteFont normalFont;
        public static SpriteFont selectedFont;

        public static void LoadAssets(ContentManager content)
        {
            
            gunVer1 = content.Load<Texture2D>("Gun_Ver1");
            player = content.Load<Texture2D>("player1");
            platform= content.Load<Texture2D>("plattform");
            bg= content.Load<Texture2D>("background");
            inventory= content.Load<Texture2D>("hole");
            item= content.Load<Texture2D>("PotionsSprite");
            walltile= content.Load<Texture2D>("walltile");
            enemy1 = content.Load<Texture2D>("Enemy1");
            enemy2 = content.Load<Texture2D>("enemy2");
            purpleFire = content.Load<Texture2D>("PurpleFireSprite");
            redFire = content.Load<Texture2D>("RedFireSprite");
            shield1 = content.Load<Texture2D>("Shield1");
            shield2 = content.Load<Texture2D>("Shield2");
            heartSprite = content.Load<Texture2D>("HeartSprite");
            coinSprite = content.Load<Texture2D>("CoinSprite");
            tilesetSprite = content.Load<Texture2D>("BrickTilesetSprite");
            tilesetSprite2 = content.Load<Texture2D>("BrickTilesetSprite2");
            smallVTile = content.Load<Texture2D>("SmallVTile");
            largeVTile = content.Load<Texture2D>("LargeVTile");
            smallPlatform = content.Load<Texture2D>("SmallPlatform");
            bigSquare = content.Load<Texture2D>("BigSquare");
            tallPlatform = content.Load<Texture2D>("TallPlatform");
            squarePlatform = content.Load<Texture2D>("SquarePlatform");
            longPlatform = content.Load<Texture2D>("LongPlatform");
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
            armorBoots = content.Load<Texture2D>("Armor_Boots");
            armorChestplate = content.Load<Texture2D>("Armor_Chestplate");
            armorHelmet = content.Load<Texture2D>("Armor_Hemlet");
            armorPants = content.Load<Texture2D>("Armor_Pants.png");
            bloodySword = content.Load<Texture2D>("Bloody_Sword");
            blueSword = content.Load<Texture2D>("Blue_Sword");
            gunVer1 = content.Load<Texture2D>("Gun_Ver1");
            gunVer2 = content.Load<Texture2D>("Gun_Ver2");
            gunVer3 = content.Load<Texture2D>("Gun_Ver3");
            knife = content.Load<Texture2D>("knife");
            redSword = content.Load<Texture2D>("Red_Sword");
            bgLVL1 = content.Load<Texture2D>("BG_Lvl_1");

            normalFont = content.Load<SpriteFont>(@"Font\normalFont");
            selectedFont= content.Load<SpriteFont>(@"Font\selectedFont");
        }
    }
}
