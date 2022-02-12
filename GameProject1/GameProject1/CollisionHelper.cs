using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject1
{
    public static class CollisionHelper
    {
        /// <summary>
        /// Detects a collision between two bounding rectangles
        /// </summary>
        /// <param name="a">the first rectanle</param>
        /// <param name="b">the second rectanlge</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right || a.Top > b.Bottom || a.Bottom < b.Top);
        }
    }
}
