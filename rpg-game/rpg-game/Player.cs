using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg_game
{
    class Player
    {
        private Vector2 p = new Vector2(100, 100);
        private int h = 3;
        private int s = 200;

        public int Health
        {
            get
            {
                return h;
            }
            set
            {
                h = value; 
            }
        }

        public Vector2 Position
        {
            get
            {
                return p;
            }
        }
        public void setX(float newX)
        {
            p.X = newX;
        }
        public void setY(float newY)
        {
            p.Y = newY;
        }
    }
}
