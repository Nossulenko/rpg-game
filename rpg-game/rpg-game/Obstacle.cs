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
    class Obstacle
    {
        protected Vector2 position;
        protected int radius;
        public static List<Obstacle> obstacles = new List<Obstacle>();

        public Obstacle(Vector2 newPos)
        {
            position = newPos;
        }
        public Vector2 Position
        {
            get { return position; }
        }

        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }


    }
    
    class Bush : Obstacle
    {
        public Bush(Vector2 newPos) : base(newPos)
        {
            radius = 42;
        }

    }
    class Tree : Obstacle
    {
        public Tree(Vector2 newPos) : base(newPos)
        {
            radius = 56;
        }

    }
}
