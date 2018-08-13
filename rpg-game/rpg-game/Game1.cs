using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

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

        TiledMapRenderer mapRenderer;
        TiledMap myMap;

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
            mapRenderer = new TiledMapRenderer(GraphicsDevice);

            base.Initialize();
        }

        // LOAD CONTENT BEGIN<=========================================================
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

            myMap = Content.Load<TiledMap>("Misc/game_map");

            //all enemies is an array that contains tail objects and we are stting it = to are maps
            //enemies layer and all object that that layer contains.

            TiledMapObject[] allEnemies = myMap.GetLayer<TiledMapObjectLayer>("enemies").Objects;
            foreach (var en in allEnemies)
            {   
                string type;
                en.Properties.TryGetValue("Type", out type);
                if (type == "Snake")
                {
                    Enemy.enemies.Add(new Snake(en.Position));
                }
                else if (type == "Eye")
                {
                    Enemy.enemies.Add(new Eye(en.Position));
                }
               /* else if (type == "New Character")
                {
                    Enemy.enemies.Add(new New Character(en.Position));
                }*/
            }

            TiledMapObject[] allObstacles = myMap.GetLayer<TiledMapObjectLayer>("obstacles").Objects;
            foreach (var o in allObstacles)
            {
                string type;
                o.Properties.TryGetValue("Type", out type);
                if (type == "Bush")
                {
                    Obstacle.obstacles.Add(new Bush(o.Position));
                }
                if (type == "Bush")
                {
                    Obstacle.obstacles.Add(new Bush(o.Position));
                }
            }
            //Enemy.enemies.Add(new Snake(new Vector2(100, 400)));
            //Enemy.enemies.Add(new Eye(new Vector2(300, 450)));

            //Obstacle.obstacles.Add(new Tree(new Vector2(600, 200)));
            //Obstacle.obstacles.Add(new Bush(new Vector2(800, 400)));

        }
        // LOAD CONTENT END<=========================================================

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        // UPDATE BEGIN<===============================================================
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(player.Health > 0)
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
                    if (Vector2.Distance(bullet.Position, en.Position) < sum)
                    {
                        bullet.Collision = true;
                        en.Health--;
                    }
                }
                // Collision between bullet and the obstacles
                if (Obstacle._collided(bullet.Position, bullet.Radius))
                    bullet.Collision = true;
            }

            foreach (Enemy en in Enemy.enemies)
            {
                int sum = player.Radius + en.Radius;
                if (Vector2.Distance(player.Position, en.Position) < sum && player.Healthtimer <= 0)
                {
                    player.Health--;
                    player.Healthtimer = 1.5f;
                }
            }

            Shooting.bullets.RemoveAll(p => p.Collision);
            Enemy.enemies.RemoveAll(e => e.Health <= 0);

            base.Update(gameTime);
        }
        // UPDATE END <===============================================================





        // DRAW BEGIN <=================================================================
        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mapRenderer.Draw(myMap);

            if (player.Health > 0)
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

            // Draw obstacle sprites on the map
            foreach (Obstacle o in Obstacle.obstacles)
            {
                Texture2D spriteToDraw;
                if (o.GetType() == typeof(Tree))
                    spriteToDraw = tree_Sprite;
                else
                    spriteToDraw = bush_Sprite;
                spriteBatch.Draw(spriteToDraw, o.Position, Color.White);

            }

            foreach (Shooting bullet in Shooting.bullets)
            {
                spriteBatch.Draw(bullet_Sprite, new Vector2(bullet.Position.X- bullet.Radius, bullet.Position.Y - bullet.Radius), Color.White);
            }
            for (int i = 0; i < player.Health; i++)
            {
                spriteBatch.Draw(heart_Sprite, new Vector2(i * 63, 0), Color.White);
            }

                spriteBatch.End();

            base.Draw(gameTime);
        }
        // DRAW END <===========================================================
    }
}
