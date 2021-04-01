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
<<<<<<< Updated upstream
        //public static Texture2D ;
        //public static Texture2D ;
        //public static Texture2D ;
        //public static Texture2D ;
=======
        public static Texture2D inventory;
        public static Texture2D item;
        public static Texture2D walltile;
        public static Texture2D enemy;
>>>>>>> Stashed changes
        //public static Texture2D ;
        //public static Texture2D ;
        //public static Texture2D ;

        public static SpriteFont normalFont;
        public static SpriteFont selectedFont;

        public static void LoadAssets(ContentManager content)
        {
             player= content.Load<Texture2D>("ball");
             platform= content.Load<Texture2D>("plattform");
             bg= content.Load<Texture2D>("background");
<<<<<<< Updated upstream
             //= content.Load<Texture2D>(" ");
             //= content.Load<Texture2D>(" ");
             //= content.Load<Texture2D>(" ");
=======
             inventory= content.Load<Texture2D>("hole");
             item= content.Load<Texture2D>("PotionsSprite");
             walltile= content.Load<Texture2D>("walltile");
             enemy = content.Load<Texture2D>("ball");
>>>>>>> Stashed changes

             normalFont= content.Load<SpriteFont>(@"Font\normalFont");
             selectedFont= content.Load<SpriteFont>(@"Font\selectedFont");
        }
    }
}
