namespace Utilities;

using System.Numerics;

public record Vector<T>(T X, T Y) where T: INumber<T>
{
	public static Vector<T> operator -(Vector<T> v) => new(-v.X, -v.Y);

	public static Vector<T> operator *(Vector<T> v, T m) => new(v.X * m, v.Y * m);
}

// [Obsolete("Use generic Type instead.")]
// public record Vector(int X, int Y) : Vector<int>(X,Y) { }