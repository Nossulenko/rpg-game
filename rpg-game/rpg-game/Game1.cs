﻿using Microsoft.Xna.Framework;
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

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(player_Sprite, player.Position, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
