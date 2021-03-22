using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpriest
{
    class Camera
    {
        private Matrix transform;
        private Vector2 pos;
        private Viewport view;

        public Camera(Viewport view)
        {
            this.view = view;
        }

        public Matrix Transform
        {
            get { return transform; }
        }

        public void SetPosition(Vector2 pos, GameState gameState) //method used to make camera follows player
        {
            this.pos = pos;
            if (gameState == GameState.Play)
                transform = Matrix.CreateTranslation(-pos.X + view.Width / 2, -pos.Y + view.Height / 2, 0);
            else
                transform = Matrix.CreateTranslation(0, 0, 0);
        }
    }
}
