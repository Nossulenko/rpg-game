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
    class Shooting
    {
        private Vector2 pos;
        private int speed = 800;
        private int radius = 15;
        private Dir direction;

        public static List<Shooting> bullets = new List<Shooting>();

        public Shooting(Vector2 newPos, Dir newDir)
        {
            pos = newPos;
            direction = newDir;
        }
        public Vector2 Postion
        {
            get
            {
                return pos;
            }
        }
        public int Radius
        {
            get
            {
                return radius;
            }
        }
        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (direction)
            {
                case Dir.Right:
                    pos.X += speed * dt;
                    break;

                case Dir.Left:
                    pos.X -= speed * dt;
                    break;

                case Dir.Down:
                    pos.Y += speed * dt;
                    break;

                case Dir.Up:
                    pos.Y -= speed * dt;
                    break;
                default:
                    break;
            }
        }
    }
}
