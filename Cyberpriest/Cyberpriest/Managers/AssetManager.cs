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
        //public static Texture2D ;
        //public static Texture2D ;
        //public static Texture2D ;
        //public static Texture2D ;
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
             //= content.Load<Texture2D>(" ");
             //= content.Load<Texture2D>(" ");
             //= content.Load<Texture2D>(" ");

             normalFont= content.Load<SpriteFont>("normalFont");
             selectedFont= content.Load<SpriteFont>("selectedFont");
        }
    }
}
