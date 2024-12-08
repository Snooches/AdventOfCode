namespace Utilities;
public record Point(int X, int Y)
{
	public static Point operator +(Point a, Vector v) => new(a.X + v.X, a.Y + v.Y);
	public static Point operator -(Point a, Vector v) => a + -v;

	public static Vector operator -(Point a, Point b) => new(b.X - a.X, b.Y - a.Y);
}