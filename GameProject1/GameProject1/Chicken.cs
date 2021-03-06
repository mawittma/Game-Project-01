using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace GameProject1
{
    public class Chicken
    {
        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;

        private bool flipped;
        /// <summary>
        /// if the chicken has collided with something
        /// </summary>

        public bool Reset = false;

        public bool dead = false;

        /// <summary>
        /// the position of chicken
        /// </summary>
        public Vector2 position = new Vector2(300, 650);
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(300, 650), 16, 16);

        /// <summary>
        /// bounding volume of sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("MyChicken");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            // Apply the gamepad movement with inverted Y axis
            position += gamePadState.ThumbSticks.Left * new Vector2(1, -1);
            if (gamePadState.ThumbSticks.Left.X < 0) flipped = true;
            if (gamePadState.ThumbSticks.Left.X > 0) flipped = false;

            // Apply keyboard movement
            if ((keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) && position.Y-60 > 0) position += new Vector2(0, -2);
            if ((keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) && position.Y-6 < 650) position += new Vector2(0, 2);
            if ((keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) && position.X-57 > 0)
            {
                position += new Vector2(-2, 0);
                flipped = false;
            }
            if ((keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) && position.X - 7 < 700)
            {
                position += new Vector2(2, 0);
                flipped = true;
            }
            if(Reset)
            {
                position = new Vector2(300, 650);
                Reset = false;
            }
            bounds.X = position.X-32;
            bounds.Y = position.Y-32;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            
            if(dead)
            {
                spriteBatch.Draw(texture, position, null, Color.White, MathHelper.Pi, new Vector2(64, 64), 1f, spriteEffects, 0);
            }
            else
            {
                
                spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(64, 64), 1f, spriteEffects, 0);
            }
        }
    }
}
