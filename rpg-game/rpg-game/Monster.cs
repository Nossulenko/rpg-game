using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace rpg_game
{
    class Monster : Player
    {
   
        protected int currentHealth, currentSpeed, monsterRad;
        private Vector2 pos;
        protected bool dead = false;
       

        public static List<Monster> enemies = new List<Monster>();

        public bool Dead
        {
            get { return dead;  }
            set { dead = value; }
        }

        public int Health
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public Vector2 Pos
        {
            get { return pos; }
        }
        public int Rad
        {
            get { return monsterRad; }
        }

        public Monster(Vector2 nextMonsterPos)
        {
            pos = nextMonsterPos;
        } 

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
     
            Vector2 headingDir = playerPos - pos;
            headingDir.Normalize();

            // Collision between enemy and obstacles
            Vector2 tempPos = pos;
            tempPos += headingDir * currentSpeed * dt;
            if (!Obstacle._collided(tempPos, monsterRad))
            {
                pos += headingDir * currentSpeed * dt;
            }

            pos += headingDir * currentSpeed * dt;

           

        }
    }

    class Snake : Monster
    {
        public Snake(Vector2 nextMonsterPos) : base(nextMonsterPos)
        {
            currentSpeed = 50;
            monsterRad = 42;
            currentHealth = 2;
            dead = false;
            
        }
        
        
    }
    class Eye : Monster
    {

        public Eye(Vector2 nextMonsterPos) : base(nextMonsterPos)
        {
            currentSpeed = 50;
            monsterRad = 45;
            currentHealth = 10;
            dead = false;

        }
    }
}
