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
        private Vector2 pos = new Vector2(100, 100);
        private int health = 3;
        private int speed = 200;
        private Dir direction = Dir.Down;
        private bool isWalking = false;
        private KeyboardState kStateOld = Keyboard.GetState();
        private int radius = 56;
        private float healthTime = 0f;

        public AnimatedSprite anim;
        public AnimatedSprite[] animations = new AnimatedSprite[4];

        public float Healthtimer
        {
            get { return healthTime; }
            set { healthTime = value; }
        }
        public int Radius
        {
            get { return radius; }
        }
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value; 
            }
        }

        public Vector2 Position
        {
            get
            {
                return pos;
            }
        }
        public void setX(float newX)
        {
            pos.X = newX;
        }
        public void setY(float newY)
        {
            pos.Y = newY;
        }

        public void Update(GameTime gt)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;

            anim = animations[(int)direction];
            if (healthTime > 0)
            {
                healthTime -= dt;
            }

            if (isWalking)
                anim.Update(gt);
            else
                anim.setFrame(1);

            isWalking = false;

            if (kState.IsKeyDown(Keys.Right))
            {
                direction = Dir.Right;
                isWalking = true;
            }
            if (kState.IsKeyDown(Keys.Left))
            {
                direction = Dir.Left;
                isWalking = true;
            }
            if (kState.IsKeyDown(Keys.Up))
            {
                direction = Dir.Up;
                isWalking = true;
            }
            if (kState.IsKeyDown(Keys.Down))
            {
                direction = Dir.Down;
                isWalking = true;
            }
            if (isWalking)
            {
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

            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
            {
                Shooting.bullets.Add(new Shooting(pos, direction));
            }
            kStateOld = kState;
        }
    }
}
