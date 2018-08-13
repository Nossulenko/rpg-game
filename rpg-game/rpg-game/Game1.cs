using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg_game
{
    enum Dir
    {
        Down,
        Up,
        Left,
        Right
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Player
        Texture2D    player_Sprite; 
        Texture2D    playerDown_Sprite;
        Texture2D    playerLeft_Sprite;
        Texture2D    playerRight_Sprite;
        Texture2D    playerUp_Sprite;

        //Obstacles
        Texture2D   bush_Sprite;
        Texture2D   tree_Sprite;

        //Enemies
        Texture2D   eyeEnemy_Sprite;
        Texture2D   snakeEnemy_Sprite;

        //Misc
        Texture2D   bullet_Sprite;
        Texture2D   heart_Sprite;

        // Create a player object
        Player player = new Player();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player_Sprite = Content.Load<Texture2D>("Player/player");
            playerUp_Sprite = Content.Load<Texture2D>("Player/PlayerUp");
            playerRight_Sprite = Content.Load<Texture2D>("Player/playerRight");
            playerLeft_Sprite = Content.Load<Texture2D>("Player/playerLeft");
            playerDown_Sprite = Content.Load<Texture2D>("Player/playerDown");

            bullet_Sprite = Content.Load<Texture2D>("Misc/bullet");
            heart_Sprite = Content.Load<Texture2D>("Misc/heart");

            eyeEnemy_Sprite = Content.Load<Texture2D>("Enemies/eyeEnemy");
            snakeEnemy_Sprite = Content.Load<Texture2D>("Enemies/snakeEnemy");

            bush_Sprite = Content.Load<Texture2D>("Obstacles/bush");
            tree_Sprite = Content.Load<Texture2D>("Obstacles/tree");

            player.animations[0] = new AnimatedSprite(playerDown_Sprite, 1, 4);
            player.animations[1] = new AnimatedSprite(playerUp_Sprite, 1, 4);
            player.animations[2] = new AnimatedSprite(playerLeft_Sprite, 1, 4); 
            player.animations[3] = new AnimatedSprite(playerRight_Sprite, 1, 4);

            Enemy.enemies.Add(new Snake(new Vector2(100, 400)));
            Enemy.enemies.Add(new Eye(new Vector2(300, 450)));
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            foreach(Shooting bullet in Shooting.bullets)
            {
                bullet.Update(gameTime);
            }

            foreach (Enemy en in Enemy.enemies)
            {
                en.Update(gameTime, player.Position);
            }

            foreach (Shooting bullet in Shooting.bullets )
            {
                foreach (Enemy en in Enemy.enemies)
                {

                    int sum = bullet.Radius + en.Radius;
                    if (Vector2.Distance(bullet.Position, en.Position) > sum)
                    {
                        bullet.Collision = true;
                    }
                }
            }

            Shooting.bullets.RemoveAll(p => p.Collision == true);

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            player.anim.Draw(spriteBatch, new Vector2(player.Position.X - 48, player.Position.Y - 48));

            spriteBatch.Begin();

            foreach(Enemy en in Enemy.enemies)
            {
                Texture2D spriteToDraw;
                int rad;
                if (en.GetType() == typeof(Snake))
                {
                    spriteToDraw = snakeEnemy_Sprite;
                    rad = 50;
                }
                else
                {
                    spriteToDraw = eyeEnemy_Sprite;
                    rad = 73;
                }
                spriteBatch.Draw(spriteToDraw, new Vector2(en.Position.X - rad, en.Position.Y - rad), Color.White);
            }

            foreach (Shooting bullet in Shooting.bullets)
            {
                spriteBatch.Draw(bullet_Sprite, new Vector2(bullet.Position.X- bullet.Radius, bullet.Position.Y - bullet.Radius), Color.White);
            }

                spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
