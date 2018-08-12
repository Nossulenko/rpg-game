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
            Vector2 moveDir = playerPos - postition;
            moveDir.Normalize();
            postition += moveDir;
        }
    }

    class Snake : Enemy
    {
        public Snake(Vector2 newPos) : base(newPos)
        {

        }
    }
    class Eye : Enemy
    {

        public Eye(Vector2 newPos) : base(newPos)
        {

        }

    }
}
