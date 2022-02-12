using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject1
{
    /// <summary>
    /// a bounding rectanlge for collision detection
    /// </summary>
    public struct BoundingRectangle
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public float X;
        /// <summary>
        /// Y coordinate
        /// </summary>
        public float Y;
        /// <summary>
        /// WIdth of rectangle
        /// </summary>
        public float Width;
        /// <summary>
        /// hieght of bounding rectangle
        /// </summary>
        public float Height;
        /// <summary>
        /// left side of bounding rectanlge
        /// </summary>
        public float Left => X;
        /// <summary>
        /// right side of bounding rectangle
        /// </summary>
        public float Right => X + Width;
        /// <summary>
        /// top side of bounding rectangle
        /// </summary>
        public float Top => Y;
        /// <summary>
        /// bottom side of bounding rectangle
        /// </summary>
        public float Bottom => Y + Height;
        /// <summary>
        /// Assigns bounding rectangle vlaues with all floats
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="width">width of rectangle</param>
        /// <param name="height">height of rectanlge</param>
        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        /// <summary>
        /// Assigns bounding rectangle vlaues with Vector2 and 2 floats
        /// </summary>
        /// <param name="position">holds x and y coordinate</param>
        /// <param name="width">width of rectangle</param>
        /// <param name="height">height of rectanlge</param>
        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }

    }
}
