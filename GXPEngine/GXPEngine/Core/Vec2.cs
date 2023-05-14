using System;
using System.IO;
using GXPEngine;
using GXPEngine.Core;

/// <summary>
/// Vec2 Struct by Nelson
/// </summary>
namespace GXPEngine.Core
{
    public struct Vec2
    {
        #region Fields and properties
        public float x;
        public float y;

        /// <summary>
        /// Gets the length (magnitude) of the Vec2.
        /// </summary>
        public float Length => Mathf.Sqrt(x * x + y * y);

        public static Vec2 Zero
        {
            get
            {
                return new Vec2(0, 0);
            }
        }

        /// <summary>
        /// Gets the perpendicular vector on the right side of this vector.
        /// </summary>
        public Vec2 PerpendicularRight => new Vec2(y, 0f - x);

        /// <summary>
        /// Gets the perpendicular vector on the left side of this vector.
        /// </summary>
        public Vec2 PerpendicularLeft => new Vec2(0f - y, x);
        #endregion

        public Vec2(float pX = 0, float pY = 0)
        {
            x = pX;
            y = pY;
        }

        #region Vector Operations
        /// <summary>
        /// Sets the x and y values of the Vec2.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>The set Vec2</returns>
        public Vec2 SetXY(float x, float y)
        {
            this.x = x;
            this.y = y;
            return this;
        }

        /// <summary>
        /// Calculates the distance between two vectors
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public float Distance(Vec2 other)
        {
            float dx = other.x - x;
            float dy = other.y - y;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Calculates the distance between two vectors
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public static float Distance(Vec2 point1, Vec2 point2)
        {
            return point1.Distance(point2);
        }

        /// <summary>
        /// Scales the Vec2 to unit length.
        /// </summary>
        public void Normalize()
        {
            if (Length == 0)
            {
                x = 0;
                y = 0;
            }
            else
            {
                float num = 1f / Length;
                x *= num;
                y *= num;
            }
        }

        /// <summary>
        /// Returns a copy of the Vec2 scaled to unit length.
        /// </summary>
        /// <returns>The normalized copy.</returns>
        public Vec2 Normalized()
        {
            Vec2 result = this;
            result.Normalize();
            return result;
        }
        /// <summary>
        /// Computes the dot product of the current Vec2 instance and another Vec2 object.
        /// </summary>
        /// <param name="other">The Vec2 object to compute the dot product with.</param>
        /// <returns>The scalar dot product of the two vectors.</returns>
        public float Dot(Vec2 other)
        {
            return x * other.x + y * other.y;
        }

        /// <summary>
        /// Calculates the cross product of two 2D vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The cross product of the two vectors.</returns>
        public static float Cross(Vec2 a, Vec2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        public Vec2 PerpendicularUnit(Vec2 normal) // TODO: Better name
        {
            float cross = Cross(normal, this);
            if (cross>0)
                return normal.PerpendicularRight;
            else if (cross<0)
                return normal.PerpendicularLeft;
            else 
                return normal; // Vectors are collinear
        }

        /// <summary>
        /// Projects a vector onto another vector, returning the projected vector.
        /// </summary>
        /// <param name="other">The vector to project onto.</param>
        /// <returns>The projected vector.</returns>
        public Vec2 Project(Vec2 other)
        {
            float scalar = this.Dot(other);
            return other.Normalized() * scalar;
        }

        /// <summary>
        /// Returns unit normal from 2 vectors representing a line segment
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns>Unit normal vector</returns>
        public static Vec2 Normal(Vec2 point1, Vec2 point2)
        {
            Vec2 direction = point2 - point1;
            return (new Vec2(-direction.y, direction.x)).Normalized();
        }

        /// <summary>
        /// Returns a normalized vector that is perpendicular to the current vector in a clockwise direction.
        /// </summary>
        /// <returns>A normalized vector that is perpendicular to the current vector in a clockwise direction.</returns>
        public Vec2 Normal()
        {
            Vec2 direction = this;
            return (new Vec2(-direction.y, direction.x)).Normalized();
        }

        /// <summary>
        /// Reflects the vector along a given normal with a specified coefficient of reflection.
        /// </summary>
        /// <param name="normal">The normal vector to reflect along.</param>
        /// <param name="bounciness">The coefficient of reflection, representing how much velocity is retained after the reflection.</param>
        public void Reflect(Vec2 normal, float bounciness = 1)
        {
            this = this - (1 + bounciness) * this.Project(normal);
        }

        /// <summary>
        /// Inverts a given 2D vector by negating its x and y components and returning a new Vec2 with the result.
        /// </summary>
        /// <param name="vector">The vector to invert</param>
        /// <returns>A new Vec2 representing the inverted vector</returns>
        public static Vec2 Invert(Vec2 vector)
        {
            return new Vec2(-vector.x, -vector.y);
        }

        /// <summary>
        /// Inverts the vector by negating its x and y components.
        /// </summary>
        /// <returns>The inverted vector.</returns>
        public Vec2 Invert()
        {
            this.x = -this.x;
            this.y = -this.y;
            return this;
        }
        #endregion

        #region Trigonometry    
        public static float Deg2Rad(float degrees) => degrees * (Mathf.PI / 180f);
        public static float Rad2Deg(float radians) => radians * (180f / Mathf.PI);

        public static Vec2 GetUnitVectorRad(float radians) => new Vec2(Mathf.Cos(radians), Mathf.Sin(radians));
        public static Vec2 GetUnitVectorDeg(float degrees) => GetUnitVectorRad(Deg2Rad(degrees));

        public static Vec2 RandomUnitVector() => GetUnitVectorDeg(Utils.Random(0f, 360f));

        public Vec2 SetAngleDegrees(float degrees)
        {
            float length = Length;
            this = GetUnitVectorDeg(degrees) * length;
            return this;
        }
        public Vec2 SetAngleRadians(float radians)
        {
            float length = Length;
            this = GetUnitVectorRad(radians) * length;
            return this;
        }

        public float GetAngleRadians()
        {
            return Mathf.Atan2(y, x);
        }
        public float GetAngleDegrees()
        {
            return Rad2Deg(Mathf.Atan2(y, x));
        }

        public Vec2 RotateDegrees(float degrees)
        {
            return this = RotateRadians(Deg2Rad(degrees));
        }
        public Vec2 RotateRadians(float radians)
        {
            return this = new Vec2(x * Mathf.Cos(radians) - y * Mathf.Sin(radians), x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
        }

        public Vec2 RotateAroundDegrees(Vec2 point, float degrees)
        {
            Vec2 temp = this - point;
            return this = temp.RotateDegrees(degrees) + point;
        }
        public Vec2 RotateAroundRadians(Vec2 point, float radians)
        {
            Vec2 temp = this - point;
            return this = temp.RotateRadians(radians) + point;
        }
        #endregion

        #region Operators    
        public static Vec2 operator +(Vec2 left, Vec2 right) => new Vec2(left.x + right.x, left.y + right.y);
        public static Vec2 operator -(Vec2 left, Vec2 right) => new Vec2(left.x - right.x, left.y - right.y);
        public static Vec2 operator *(float left, Vec2 right) => new Vec2(left * right.x, left * right.y);
        public static Vec2 operator *(Vec2 left, float right) => new Vec2(left.x * right, left.y * right);
        public static Vec2 operator /(Vec2 left, float right) => new Vec2(left.x / right, left.y / right);
        public static bool operator ==(Vec2 left, Vec2 right) => (left.x == right.x && left.y == right.y);
        public static bool operator !=(Vec2 left, Vec2 right) => (left.x != right.x && left.y != right.y);
        #endregion

        #region Struct Converters
        /// <summary>
        /// Initializes a new instance of the Vec2 class with the x and y values
        /// from the core GXPEngine Vec2.
        /// </summary>
        /// <param name="vector2">The Vec2 to copy the x and y values from.</param>
        //public Vec2(GXPEngine.Core.Vec2 vector2)
        //{
        //    x = vector2.x;
        //    y = vector2.y;
        //}
        #endregion

        #region Miscellaneous

        /// <summary>
        /// Determines if two floating point values are approximately equal within a given tolerance.
        /// </summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">The second value to compare.</param>
        /// <param name="tolerance">The maximum difference allowed between the two values for them to still be considered approximately equal. Defaults to 0.0001f.</param>
        /// <returns>True if the absolute difference between a and b is less than the given tolerance, false otherwise.</returns>
        public static bool Approximately(float a, float b, float tolerance = 0.0001f)
        {
            return Mathf.Abs(a - b) < tolerance;
        }
        /// <summary>
        /// Determines if two Vec2 objects are approximately equal within a given tolerance level.
        /// </summary>
        /// <param name="a">The first Vec2 to compare.</param>
        /// <param name="b">The second Vec2 to compare.</param>
        /// <param name="tolerance">The maximum difference allowed between the two Vec2 objects.</param>
        /// <returns>True if the two Vec2 objects are approximately equal within the given tolerance, false otherwise.</returns>
        public static bool Approximately(Vec2 a, Vec2 b, float tolerance = 0.0001f)
        {
            return (Mathf.Abs(a.x - b.x) < tolerance && Mathf.Abs(a.y - b.y) < tolerance);
        }


        public override string ToString()
        {
            return String.Format("({0},{1})", x, y);
        }
        public override bool Equals(object obj)
        {
            return obj is Vec2 vec &&
                   x == vec.x &&
                   y == vec.y &&
                   Length == vec.Length;
        }
        public override int GetHashCode()
        {
            int hashCode = -477159744;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            return hashCode;
        }
        #endregion
    }
}