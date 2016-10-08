using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.UI;
using RPG.World;
using RPGCore.Creatures;

namespace RPG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private static Game1 _instance;
        public static Game1 Instance { get { return _instance; } }
        public SpriteBatch spriteBatch;
        public Player player;
        public Map CurrentMap;
        InputHelper inputHelper;
        GraphicsDeviceManager graphics;
        HealthBar playerHealthBar;

        public Game1()
        {
            inputHelper = new InputHelper();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ExperienceTable.InitializeLevelTable(50, 1.075f, 10, 99);
            CurrentMap = new Map(24, 24);
            
            // Draw the beach
            var groundLayer = CurrentMap.layers[0];
            groundLayer.tiles[0, 0].texture = new Point(1, 0);
            groundLayer.tiles[0, 1].texture = new Point(1, 0);
            groundLayer.tiles[0, 2].texture = new Point(1, 0);
            groundLayer.tiles[1, 0].texture = new Point(1, 0);
            groundLayer.tiles[2, 0].texture = new Point(1, 0);
            groundLayer.tiles[1, 1].texture = new Point(1, 0);
            groundLayer.tiles[2, 1].texture = new Point(1, 0);/*
            CurrentMap = Map.LoadMap();
            //Serialization testing
            /*var mapData = MapSerializer.Serialize(CurrentMap);
            mapData = mapData.Replace("0:", "1:").Replace(":0", ":1");
            CurrentMap = MapSerializer.Deserialize(mapData);*/
            CurrentMap.generateRandomMap();
            CurrentMap = Map.LoadMap();
            Instance.Drawing += CurrentMap.Draw;
            player = new Player(this);
            //playerHealthBar = new HealthBar(player.Character, new Rectangle(40, 20, 120, 32));
            Components.Add(player);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            inputHelper.Update();
            OnUpdating(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            OnDrawing(gameTime);
            base.Draw(gameTime);
        }

        public delegate void UpdateEventHandler(GameTime gameTime);
        public event UpdateEventHandler Updating;
        public event UpdateEventHandler Drawing;

        protected virtual void OnUpdating(GameTime gt)
        {
            if (Updating != null)
                Updating(gt);
        }

        protected virtual void OnDrawing(GameTime gt)
        {
            if (Drawing != null)
                Drawing(gt);
        }
    }
}
