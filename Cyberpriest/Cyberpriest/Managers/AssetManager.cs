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
        //public static Texture2D ;
        //public static Texture2D ;
        //public static Texture2D ;

        public static SpriteFont normalFont;
        public static SpriteFont selectedFont;

        public static void LoadAssets(ContentManager content)
        {
            player= content.Load<Texture2D>("player1");
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

            normalFont = content.Load<SpriteFont>(@"Font\normalFont");
            selectedFont= content.Load<SpriteFont>(@"Font\selectedFont");
        }
    }
}
