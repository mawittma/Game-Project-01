using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject1
{
    public class CarSprite
    {
        Texture2D texture;
        public bool flipped = true;
        public int carNumber;
        public Vector2 Position { get; set; }
        /// <summary>
        /// A boolean indicating if the car is colliding with something
        /// </summary>
        public bool Colliding { get; set; }
        /// <summary>
        /// How fast the car will go as a factor of the original velocity
        /// </summary>
        public float speedFactor { get; set; }
        /// <summary>
        /// A vector representing a the velocity of the car
        /// </summary>
        public Vector2 Direction { get; set; }

        private BoundingRectangle bounds;

        /// <summary>
        /// bounding volume of sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;
        public CarSprite(int y, int lowB, int highB,int num)
        {
            System.Random random = new System.Random();
            Position = new Vector2(random.Next(lowB, highB), y);
            Direction = Vector2.UnitX;
            speedFactor = random.Next(70, 100);
            bounds = new BoundingRectangle(Position, 118, 49);
            carNumber = num;
        }

        /// <summary>
        /// Loads the car's texture
        /// </summary>
        /// <param name="contentManager">The content manager to use</param>
        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("car1_spr");
            
        }

        /// <summary>
        /// Updates the car
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            //bounds.X = Position.X;
            //bounds.Y = Position.Y;
            
            // Move the balls
            //Center += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Position += Direction * speedFactor * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Bounce balls off the edge of the screen
            if (Position.X < 0 || Position.X > Graphics.PreferredBackBufferWidth - 118)
            {
                Direction *= -Vector2.UnitX;
                flipped = !flipped;
            }
            //if (Center.Y < radius || Center.Y > Constants.GAME_HEIGHT - radius) Velocity *= -Vector2.UnitY;

            if(Colliding)
            {
                Direction *= -Vector2.UnitX;
                flipped = !flipped;
            }
            Position += Direction * speedFactor * (float)gameTime.ElapsedGameTime.TotalSeconds;
           // Position = Position (Direction);
            bounds.X = Position.X  ;
            bounds.Y = Position.Y;
            // Clear the colliding flag 
            Colliding = false;
        }

        /// <summary>
        /// Draws the ball using the provided spritebatch
        /// </summary>
        /// <param name="gameTime">an object representing time in the game</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Use Green for visual collision indication
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, Position, null, Color.White, 0, new Vector2(0,0), 1f, spriteEffects, 0);
        }

    }
}
