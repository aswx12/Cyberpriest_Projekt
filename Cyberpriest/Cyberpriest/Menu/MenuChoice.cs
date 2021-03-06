using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class MenuChoice
    {
        public float posX { get; set; }
        public float posY { get; set; }

        public string Text { get; set; }
        public bool Selected { get; set; }

        public Action ClickAction { get; set; }
        public Rectangle HitBox { get; set; }
    }
}