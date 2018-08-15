using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace rpg_game
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Colums { get; set; }
        private int currentFrame;
        private int totalFrames;
        private double timer;
        private double speed;
        public AnimatedSprite(Texture2D texture, int rows, int colums)
        {
            Texture = texture;
            Rows = rows;
            Colums = colums;
            currentFrame = 0;
            totalFrames = Rows * Colums;
            speed = 0.15D;
            timer = speed;
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                currentFrame++; // This is what moves the frame animation
                timer = speed;
            }
             
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Colums, 
                height = Texture.Height / Rows, 
                row = (int)((float)currentFrame / (float)Colums), 
                colum = currentFrame % Colums;
            
            Rectangle sourceRectangle = new Rectangle(width * colum, height * row, width, height), 
                destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            

         
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            
        }

        public void setFrame(int newFrame)
        {
            currentFrame = newFrame;
        }
    }
}