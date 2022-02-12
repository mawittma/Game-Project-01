using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D yellowRect;
        private Texture2D grassRect;
        private Chicken chicken;
        private List<CarSprite> cars;
        private bool playing = false;
        private SpriteFont earthbound;
        private bool scoring = true;
        private int score = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 650;
            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int carCounter = 1;
            chicken = new Chicken();
            cars = new List<CarSprite>();
            for(int i = 100; i < 550; i += 70)
            {
                cars.Add(new CarSprite(i,50,300,carCounter));
                carCounter++;
                cars.Add(new CarSprite(i, 400, 680,carCounter));
            }

            
          
            base.Initialize();

        }

        protected override void LoadContent()
        {
            earthbound = Content.Load<SpriteFont>("EarthboundFont");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            yellowRect = new Texture2D(GraphicsDevice, 1, 1);
            yellowRect.SetData(new Color[] { Color.Yellow });
            grassRect = new Texture2D(GraphicsDevice, 1, 1);
            grassRect.SetData(new Color[] { Color.Green });
            chicken.LoadContent(Content);
            foreach (var car in cars) car.LoadContent(Content);
            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) playing = true;
            if(playing)
            {
                foreach(var car in cars)
                {
                    foreach(var car2 in cars)
                    {
                        if(car.carNumber != car2.carNumber && car.Bounds.CollidesWith(car2.Bounds))
                        {
                            car.Colliding = true;
                            car2.Colliding = true;
                        }
                    }
                }
                chicken.Update(gameTime,_graphics);
                foreach (var car in cars) car.Update(gameTime,_graphics);
                foreach(var car in cars)
                {
                    if(car.Bounds.CollidesWith(chicken.Bounds))
                    {
                        playing = false;
                        chicken.Collided = true;
                        score = 0;
                        scoring = true;
                    }
                }
                if(chicken.position.Y < 100 && scoring)
                {
                    score++;
                    scoring = false;
                }
                if(chicken.position.Y > 570 && !scoring)
                {
                    score++;
                    scoring = true;
                }
            }
            
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();

            if(playing)
            {
                for(int i = 160; i < 550; i+=70) for(int j = 50; j < 700; j+= 150) _spriteBatch.Draw(yellowRect,new Vector2(j,i), new Rectangle(0, 0, 75, 10), Color.White);
                _spriteBatch.Draw(grassRect, new Vector2(0, 0), new Rectangle(0, 0, 800, 100), Color.White);
                _spriteBatch.Draw(grassRect, new Vector2(0, 570), new Rectangle(0, 0, 800, 100), Color.White);
                chicken.Draw(gameTime, _spriteBatch);
                foreach (var car in cars) car.Draw(gameTime,_spriteBatch);
                _spriteBatch.DrawString(earthbound, "Score: " + score, new Vector2(10, 10), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(earthbound, "Press 'Enter' to Play", new Vector2(200, 200), Color.White);
                _spriteBatch.DrawString(earthbound, "Press 'ESC' to Quit", new Vector2(200, 400), Color.White);
            }
            
            _spriteBatch.End();
            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}
