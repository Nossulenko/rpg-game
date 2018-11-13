using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg_game
{
    class Player
    {
        private Vector2 pos = new Vector2(1300, 1000);
        private int health = 3, speed = 200, rad = 56;
        private float healthDelay = 0f;
        private Dir direction = Dir.Down;
        private bool isWalking = false;
        private KeyboardState previousKeyState = Keyboard.GetState();

        public AnimatedSprite anim;
        public AnimatedSprite[] animations = new AnimatedSprite[4];

       public float Healthdelay
        {
            get { return healthDelay; }
            set { healthDelay = value; }
        }
       public int Rad
        {
            get { return rad; }         
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

       public Vector2 Pos
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

        public void Update(GameTime gt )
        {
            KeyboardState currentKeyState = Keyboard.GetState();
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;

            anim = animations[(int)direction];
            if (healthDelay > 0)
            {
                healthDelay -= dt;
            }

            if (isWalking)
                anim.Update(gt);
            else
                anim.setFrame(1);

            isWalking = false;

            if (currentKeyState.IsKeyDown(Keys.Right))
            {
                direction = Dir.Right;
                isWalking = true;
            }
            if (currentKeyState.IsKeyDown(Keys.Left))
            {
                direction = Dir.Left;
                isWalking = true;
            }
            if (currentKeyState.IsKeyDown(Keys.Up))
            {
                direction = Dir.Up;
                isWalking = true;
            }
            if (currentKeyState.IsKeyDown(Keys.Down))
            {
                direction = Dir.Down;
                isWalking = true;
            }
            if (isWalking)
            {
                Vector2 tempPos = pos;
                switch (direction)
                {
                case Dir.Right:
                    tempPos.X += speed * dt;
                        if (!Obstacle._collided(tempPos, rad))
                        {
                            pos.X += speed * dt;
                        }
                    break;

                case Dir.Left:
                    tempPos.X -= speed * dt;
                        if (!Obstacle._collided(tempPos, rad))
                        {
                            pos.X -= speed * dt;
                        }
                        break;

                case Dir.Down:
                    tempPos.Y += speed * dt;
                        if (!Obstacle._collided(tempPos, rad))
                        {
                            pos.Y += speed * dt;
                        }
                        break;

                case Dir.Up:
                    tempPos.Y -= speed * dt;
                        if (!Obstacle._collided(tempPos, rad))
                        {
                            pos.Y -= speed * dt;
                        }
                        break;
                default:
                    break;
                }
            }

            if (currentKeyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space))
            {
               
                Shooting.bullets.Add(new Shooting(pos, direction));
                
            }
            previousKeyState = currentKeyState;
        }
    }
}
