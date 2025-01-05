namespace Utilities;

using System.Numerics;

public record Point<T>(T X, T Y) where T : INumber<T>
{
	public static Point<T> operator +(Point<T> a, Vector<T> v) => new(a.X + v.X, a.Y + v.Y);
	public static Point<T> operator -(Point<T> a, Vector<T> v) => a + -v;

	public static Vector<T> operator -(Point<T> a, Point<T> b) => new(b.X - a.X, b.Y - a.Y);
}

// [Obsolete("Use generic Type instead.")]
// public record Point(int X, int Y) : Point<int>(X, Y) { }