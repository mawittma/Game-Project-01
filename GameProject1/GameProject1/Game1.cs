using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace GameProject1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //private Texture2D yellowRect;
        //private Texture2D grassRect;
        ExplosionParticleSystem _explosions;
        private Texture2D background;
        private Chicken chicken;
        private List<CarSprite> cars;
        private bool playing = false;
        private SpriteFont earthbound;
        private bool scoring = true;
        private int score = 0;
        private SoundEffect Explosion;
        private SoundEffect Powerup;
        private Song BackgroundMusic;
        private List<CarSprite> titleCars;
        private List<Chicken> titleChicken;
        private float shakeTime ;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 650;
            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _explosions = new ExplosionParticleSystem(this, 20);
            Components.Add(_explosions);
            int carCounter = 1;
            chicken = new Chicken();
            titleChicken = new List<Chicken>();
            for(int i = 120; i <800; i+= 500)
            {
                for(int j = 310; j < 400; j += 65)
                {
                    Chicken c = new Chicken();
                    c.position = new Vector2(i, j);
                    if (j > 350)
                    {
                        c.dead = true;
                        c.position = new Vector2(i-65, j - 50);
                    }

                    titleChicken.Add(c);
                }
                
            }
            cars = new List<CarSprite>();
            for(int i = 100; i < 550; i += 70)
            {
                cars.Add(new CarSprite(i,50,225,carCounter));
                carCounter++;
                cars.Add(new CarSprite(i, 325, 520,carCounter));
            }

            titleCars = new List<CarSprite>();
            int carTitleCounter = 0;
            for (int i = 50; i < 800; i += 500) for (int j = 100; j < 650; j += 400) titleCars.Add(new CarSprite(j,i,i,carTitleCounter++));

            
          
            base.Initialize();

        }

        protected override void LoadContent()
        {
            earthbound = Content.Load<SpriteFont>("EarthboundFont");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            /*yellowRect = new Texture2D(GraphicsDevice, 1, 1);
            yellowRect.SetData(new Color[] { Color.Yellow });
            grassRect = new Texture2D(GraphicsDevice, 1, 1);
            grassRect.SetData(new Color[] { Color.Green });*/
            background = Content.Load<Texture2D>("Background");
            chicken.LoadContent(Content);
            foreach (Chicken c in titleChicken) c.LoadContent(Content);
            foreach (var car in cars) car.LoadContent(Content);
            foreach (var car in titleCars) car.LoadContent(Content);
            Explosion = Content.Load<SoundEffect>("Explosion");
            Powerup = Content.Load<SoundEffect>("Powerup");
            BackgroundMusic = Content.Load<Song>("Makaih Beats - NothingWasTheSame (makaih.com)");
            
            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                playing = true;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(BackgroundMusic);
            }
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
             
                foreach (var car in cars)
                {
                    if(car.Bounds.CollidesWith(chicken.Bounds))
                    {
                        
                        scoring = true;
                        Explosion.Play();
                        chicken.dead = true;
                        shakeTime = 0f;
                        shakeTime += (float)gameTime.TotalGameTime.TotalSeconds;
                        
                        _explosions.PlaceExplosion(chicken.position);
                        
                        MediaPlayer.Pause();

                        //System.Threading.Thread.Sleep(1000);
                        //MediaPlayer.Pause();
                        //playing = false;
                        //chicken.Collided = true;
                        //score = 0;
                        //chicken.dead = false;
                    }
                }
                if(chicken.position.Y < 100 && scoring)
                {
                    score++;
                    scoring = false;
                    Powerup.Play();
                }
                if(chicken.position.Y > 570 && !scoring)
                {
                    score++;
                    scoring = true;
                    Powerup.Play();
                }
            }
            else
            {
                foreach (var car in titleCars) car.Update(gameTime, _graphics);
                foreach (var car in titleCars)
                {
                    foreach (var car2 in titleCars)
                    {
                        if (car.carNumber != car2.carNumber && car.Bounds.CollidesWith(car2.Bounds))
                        {
                            car.Colliding = true;
                            car2.Colliding = true;
                        }
                    }
                }
            }
            
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            
            /*if(chicken.dead)
            {
                chicken.Draw(gameTime, _spriteBatch);
                System.Threading.Thread.Sleep(1000);
                MediaPlayer.Pause();
                playing = false;
                chicken.Collided = true;
                score = 0;
                chicken.dead = true;
            }*/
            if(playing)
            {
                if(chicken.dead)
                {
                    var radius = 30.0;
                    Vector2 offset = new Vector2(0, 0);
                    System.Random random = new System.Random();
                    var randomAng = random.Next(60);
                    offset = new Vector2((float)(Math.Sin(randomAng) * radius),(float)(Math.Cos(randomAng) * radius));
                    radius *= .9;
                    if((float)gameTime.TotalGameTime.TotalSeconds - shakeTime > 1 || radius <= 0)
                    {
                        chicken.dead = false;
                        chicken.Reset = true;
                        playing = false;
                        
                        score = 0;
                        
                        
                    }
                    Matrix transform = Matrix.CreateTranslation(offset.X, offset.Y, 0);
                    _spriteBatch.Begin(transformMatrix: transform);
                    _spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    chicken.Draw(gameTime, _spriteBatch);
                    foreach (var car in cars) car.Draw(gameTime, _spriteBatch);
                    _spriteBatch.DrawString(earthbound, "Score: " + score, new Vector2(10, 10), Color.White);
                    _spriteBatch.End();
                }
                else
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    chicken.Draw(gameTime, _spriteBatch);
                    foreach (var car in cars) car.Draw(gameTime,_spriteBatch);
                    _spriteBatch.DrawString(earthbound, "Score: " + score, new Vector2(10, 10), Color.White);
                    _spriteBatch.End();
                }
                //for(int i = 160; i < 550; i+=70) for(int j = 50; j < 700; j+= 150) _spriteBatch.Draw(yellowRect,new Vector2(j,i), new Rectangle(0, 0, 75, 10), Color.White);
                //_spriteBatch.Draw(grassRect, new Vector2(0, 0), new Rectangle(0, 0, 800, 100), Color.White);
                //_spriteBatch.Draw(grassRect, new Vector2(0, 570), new Rectangle(0, 0, 800, 100), Color.White);
                
            }
            else
            {
                _spriteBatch.Begin();
                foreach (var car in titleCars) car.Draw(gameTime, _spriteBatch);
                foreach (var c in titleChicken) c.Draw(gameTime, _spriteBatch);
                _spriteBatch.DrawString(earthbound, "Press 'Enter' to Play", new Vector2(150, 200), Color.White);
                _spriteBatch.DrawString(earthbound, "Press 'ESC' to Quit", new Vector2(150, 400), Color.White);
                _spriteBatch.End();

            }
            
            //_spriteBatch.End();
            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}
