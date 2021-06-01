using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyberpriest
{
    class EnemyGreed : EnemyType
    {
        public EnemyGreed(Texture2D tex, Vector2 pos, PokemonGeodude geodude) : base(tex, pos, geodude)
        {
        }
    }
}
