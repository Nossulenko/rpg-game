using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace rpg_game
{
    class Enemy
    {
        private Vector2 postition;
        protected int health;
        protected int speed;
        protected int radius;

        public static List<Enemy> enemies = new List<Enemy>();

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Vector2 Position
        {
            get { return postition; }
        }
        public int Radius
        {
            get { return radius; }
        }

        public Enemy(Vector2 newPos)
        {
            postition = newPos;
        } 

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 moveDir = playerPos - postition;
            moveDir.Normalize();
            // Collision between enemy and obstacles
            Vector2 tempPos = postition;
            tempPos += moveDir * speed * dt;
            if (!Obstacle._collided(tempPos, radius))
            {
                postition += moveDir * speed * dt;
            }

            postition += moveDir * speed * dt;
        }
    }

    class Snake : Enemy
    {
        public Snake(Vector2 newPos) : base(newPos)
        {
            speed = 120;
            radius = 42;
            health = 2;

        }
    }
    class Eye : Enemy
    {

        public Eye(Vector2 newPos) : base(newPos)
        {
            speed = 80;
            radius = 45;
            health = 7;
        }

    }
}
