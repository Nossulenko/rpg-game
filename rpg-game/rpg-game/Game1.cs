using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
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

        TiledMapRenderer mapRenderer;
        TiledMap myMap;
        Camera2D playerCam;
        Rectangle destRect, sourceRect, screen;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        

        Song[] songs = new Song[3];
        SoundEffect[] soundEffects = new SoundEffect[3];

        bool startScreenOn = true,
            loseScreenOn = false,
            winScreenOn = false,
            endScreenOn = false;

        Texture2D
            beginScreen,
            endScreen,
            winScreen,
            player_Sprite,
            playerDown_Sprite,
            playerLeft_Sprite,
            playerRight_Sprite,
            playerUp_Sprite,
            bush_Sprite,
            tree_Sprite,
            eyeEnemy_Sprite,
            snakeEnemy_Sprite,
            bullet_Sprite,
            heart_Sprite,
            zombieFront_Sprite,
            zombieDown_Sprite,
            zombieUp_Sprite,
            zombieLeft_Sprite,
            zombieRight_Sprite;

        Player player = new Player();
        

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1900;
            graphics.PreferredBackBufferHeight = 1200;
        }

        

        protected override void Initialize()
        {
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
            playerCam = new Camera2D(GraphicsDevice);

            destRect = new Rectangle(100, 100, 96, 96);
            


            base.Initialize();
        }

        // LOAD CONTENT BEGIN<=========================================================
        protected override void LoadContent()
        {
              
                    // Create a new SpriteBatch, which can be used to draw textures.
                    spriteBatch = new SpriteBatch(GraphicsDevice);

                    songs[0] = Content.Load<Song>("Sounds/intro");
                    songs[1] = Content.Load<Song>("Sounds/end");
                    // songs[2] = Content.Load<Song>("Sounds/win");

                    endScreen = Content.Load<Texture2D>("Screens/loseScreen");
                    beginScreen = Content.Load<Texture2D>("Screens/introScreen");
                    winScreen = Content.Load<Texture2D>("Screens/winScreen");

                    bullet_Sprite = Content.Load<Texture2D>("World/bullet");
                    heart_Sprite = Content.Load<Texture2D>("World/heart");

                    eyeEnemy_Sprite = Content.Load<Texture2D>("Monsters/eyeEnemy");
                    snakeEnemy_Sprite = Content.Load<Texture2D>("Monsters/snakeEnemy");

                    bush_Sprite = Content.Load<Texture2D>("Collisions/bush");
                    tree_Sprite = Content.Load<Texture2D>("Collisions/tree");

                    player_Sprite = Content.Load<Texture2D>("Player/player");
                    playerUp_Sprite = Content.Load<Texture2D>("Player/PlayerUp");
                    playerRight_Sprite = Content.Load<Texture2D>("Player/playerRight");
                    playerLeft_Sprite = Content.Load<Texture2D>("Player/playerLeft");
                    playerDown_Sprite = Content.Load<Texture2D>("Player/playerDown");

                    zombieFront_Sprite = Content.Load<Texture2D>("Zombie1/zombieFront");
                    zombieDown_Sprite = Content.Load<Texture2D>("Zombie1/zombie1Down");
                    zombieUp_Sprite = Content.Load<Texture2D>("Zombie1/zombie1Up");
                    zombieLeft_Sprite = Content.Load<Texture2D>("Zombie1/zombie1Left");
                    zombieRight_Sprite = Content.Load<Texture2D>("Zombie1/zombie1Right");

                    //AnimatedSprite zombieWalkDown = new AnimatedSprite(zombieDown_Sprite, 1, 4);
                    //AnimatedSprite zombieWalkUp = new AnimatedSprite(zombieUp_Sprite, 1, 4);
                    //AnimatedSprite zombieWalkLeft = new AnimatedSprite(zombieLeft_Sprite, 1, 4);
                    //AnimatedSprite zombieWalkRight = new AnimatedSprite(zombieRight_Sprite, 1, 4);

                    AnimatedSprite playerWalkDown = new AnimatedSprite(playerDown_Sprite, 1, 4);
                    AnimatedSprite playerWalkUp = new AnimatedSprite(playerUp_Sprite, 1, 4);
                    AnimatedSprite playerWalkLeft = new AnimatedSprite(playerLeft_Sprite, 1, 4);
                    AnimatedSprite playerWalkRight = new AnimatedSprite(playerRight_Sprite, 1, 4);

                    player.animations[0] = playerWalkDown;
                    player.animations[1] = playerWalkUp;
                    player.animations[2] = playerWalkLeft;
                    player.animations[3] = playerWalkRight;

                    screen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                    myMap = Content.Load<TiledMap>("World/game_map");
                     MediaPlayer.Play(songs[0]);

                    //screen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                    //all enemies is an array that contains tail objects and we are stting it = to are maps
                    //enemies layer and all object that that layer contains.

                    TiledMapObject[] allEnemies = myMap.GetLayer<TiledMapObjectLayer>("enemies").Objects;
                    foreach (var en in allEnemies)
                    {
                        // Hier worden de enemies aanghemaakt op basis van de enemies layer op de map
                        string type;
                        en.Properties.TryGetValue("Type", out type);
                        if (type == "Snake")
                        {
                            Monster.enemies.Add(new Snake(en.Position));
                        }
                        else if (type == "Eye")
                        {
                            Monster.enemies.Add(new Eye(en.Position));
                        }
               
                    }

                    TiledMapObject[] allObstacles = myMap.GetLayer<TiledMapObjectLayer>("obstacles").Objects;
                    foreach (var obs in allObstacles)
                    {
                        string type;
                        obs.Properties.TryGetValue("Type", out type);
                        if (type == "Bush")
                        {
                            Obstacle.obstacles.Add(new Bush(obs.Position));
                        }
                        else if (type == "Tree")
                        {
                            Obstacle.obstacles.Add(new Tree(obs.Position));
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
        
        }

        // UPDATE BEGIN<===============================================================
        protected override void Update(GameTime gameTime)
        {
            var aliveMonsters = (Monster.enemies.FindAll(en => !en.Dead));
            KeyboardState keys = Keyboard.GetState();

            if (aliveMonsters.Count == 0)
            {
                winScreenOn = true;
                if (keys.IsKeyDown(Keys.Enter))
                {
                    foreach (Monster en in Monster.enemies)
                    {
                        en.Update(gameTime, player.Pos);


                    }
                    winScreenOn = false;
                    endScreenOn = false;

                    player.Health =+ 5;

                }

            }

           

            //++++++++++++++++++++++++STARTSCREEN+++++++++++++++++++++++


            if (startScreenOn || endScreenOn)
            {
                if (keys.IsKeyDown(Keys.Enter))
                {
                    
                    startScreenOn = false;
                    MediaPlayer.IsMuted = true;
                    
                }
            }
            if (player.Health <= 0)
            {
                
                endScreenOn = true;
                if (keys.IsKeyDown(Keys.Enter))
                {
                    endScreenOn = false;
                    
                    player.Health =+ 5;
                    

                }


            }
            if (startScreenOn == false && endScreenOn == false)
            {
                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsed >= delay)
                {
                    if (frames >= 3)
                    {
                        frames = 0;
                    }
                    else
                    {
                        frames++;
                    }
                    elapsed = 0;
                }
                sourceRect = new Rectangle(96 * frames, 0, 96, 96);

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (player.Health > 0)
                    player.Update(gameTime);

                playerCam.LookAt(player.Pos);

                foreach (Shooting bullet in Shooting.bullets)
                    bullet.Update(gameTime);

                foreach (Monster en in Monster.enemies)
                {
                    en.Update(gameTime, player.Pos);
                   
                    
                }

                foreach (Shooting bullet in Shooting.bullets)
                {
                    foreach (Monster en in Monster.enemies)
                    {
                        int sum = bullet.Radius + en.Rad;
                        if (Vector2.Distance(bullet.Position, en.Pos) < sum)
                        {
                            bullet.Collision = true;
                            en.Health--;
                        }
                        
                    }
                    // Collision between bullet and the obstacles
                    if (Obstacle._collided(bullet.Position, bullet.Radius))
                        bullet.Collision = true;
                }

                foreach (Monster en in Monster.enemies)
                {
                    int sum = player.Rad + en.Rad;
                    if (Vector2.Distance(player.Pos, en.Pos) < sum && player.Healthdelay <= 0)
                    {
                        player.Health--;
                        player.Healthdelay = 1.5f;
                    }

                }
                Shooting.bullets.RemoveAll(p => p.Collision);
                Monster.enemies.RemoveAll(e => e.Health <= 0);

                base.Update(gameTime);
            }

            //++++++++++++++++++++++++STARTSCREEN+++++++++++++++++++++++

            
        }
        // UPDATE END <===============================================================





        // DRAW BEGIN <=================================================================
        protected override void Draw(GameTime gameTime) 
        {
            
                GraphicsDevice.Clear(Color.CornflowerBlue);

            mapRenderer.Draw(myMap, playerCam.GetViewMatrix());

           spriteBatch.Begin(transformMatrix: playerCam.GetViewMatrix());

            if (startScreenOn)
            {
                spriteBatch.Draw(beginScreen, screen, Color.White);
            }
            if (endScreenOn)
            {
                spriteBatch.Draw(endScreen, screen, Color.White);
                
            }
            

            //++++++++++++++++++++++++STARTSCREEN+++++++++++++++++++++++
            if (startScreenOn == false && endScreenOn == false)
            { 
                if (player.Health > 0)
                    player.anim.Draw(spriteBatch, new Vector2(player.Pos.X - 48, player.Pos.Y - 48));

               

                foreach (Monster en in Monster.enemies)
                {
                    Texture2D spriteToDraw;
                    int rad;
                   
                    if (en.GetType() == typeof(Eye))
                    {
                        spriteToDraw = eyeEnemy_Sprite;
                        rad = 50;
                    }
                    else
                    {
                        spriteToDraw = snakeEnemy_Sprite;
                        rad = 50;
                    }
                    
                       
                spriteBatch.Draw(spriteToDraw, new Vector2(en.Pos.X - rad, en.Pos.Y - rad), Color.White);
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

            }
            //++++++++++++++++++++++++STARTSCREEN+++++++++++++++++++++++
                spriteBatch.End();
                spriteBatch.Begin();

            if (startScreenOn)
            {
                spriteBatch.Draw(beginScreen, screen, Color.White);
            }
            if (endScreenOn)
            {
                spriteBatch.Draw(endScreen, screen, Color.White);
            }
           
            //++++++++++++++++++++++++STARTSCREEN+++++++++++++++++++++++
            if (startScreenOn == false && endScreenOn == false)
            {
                for (int i = 0; i < player.Health; i++)
                {
                    spriteBatch.Draw(heart_Sprite, new Vector2(i * 63, 0), Color.White);
                }
            }

            if (winScreenOn)
            {
                spriteBatch.Draw(winScreen, screen, Color.White);
            }

            //++++++++++++++++++++++++STARTSCREEN+++++++++++++++++++++++
            spriteBatch.End();
                base.Draw(gameTime);
        }
        // DRAW END <===========================================================
    }
}
