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
        protected Vector2 collidePos; // Location of the collision zone of the sprite 
        protected Vector2 position;
        protected int radius;
        public static List<Obstacle> obstacles = new List<Obstacle>();


        public Vector2 CollidePos
        {
            get { return collidePos; }
        }
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

        public static bool _collided(Vector2 nextPos, int nextRad)
        {
            foreach(Obstacle o in Obstacle.obstacles)
            {
                int sum = o.Radius + nextRad;
                if (Vector2.Distance(o.collidePos, nextPos) < sum)
                {
                    return true;
                }
                
            }
            return false;
        }

    }
    
    class Bush : Obstacle
    {
        public Bush(Vector2 newPos) : base(newPos)
        {
            radius = 20;
            collidePos = new Vector2(position.X + 64, position.Y + 57);
            //Collision zone position
        }

    }
    class Tree : Obstacle
    {
        public Tree(Vector2 newPos) : base(newPos)
        {
            radius = 32;
            collidePos = new Vector2(position.X + 56, position.Y + 150);
            // Collision zone position 
        }

    }
}
