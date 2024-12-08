namespace Utilities;
public record Vector(int X, int Y){

	public static Vector operator -(Vector v) => new(-v.X, -v.Y);

	public static Vector operator *(Vector v, int m) => new(v.X * m, v.Y * m);
}