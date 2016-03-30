using SharpDX;
using Color = System.Drawing.Color;

namespace PaintBuddy
{
    public class Paint
    {
        public Color Color;
        public bool IsGlow;
        public Vector3 Location;
        public float Radius;

        public Paint()
        {
        }

        /// <summary>
        ///     New instance of the paint class
        /// </summary>
        /// <param name="location">The location of the Paint module</param>
        /// <param name="radius">The radius of the paint module</param>
        /// <param name="color">The color of the paint module</param>
        /// <param name="isGlow">If the circle is glowy or not</param>
        public Paint(Vector3 location, float radius, Color color, bool isGlow)
        {
            Location = location;
            Radius = radius;
            Color = color;
            IsGlow = isGlow;
        }
    }
}