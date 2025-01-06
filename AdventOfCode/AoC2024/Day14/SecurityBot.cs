namespace AoC2024.Day14;

using Utilities;

public class SecurityBot(Point<int> position, Vector<int> velocity)
{
	public Point<int> Position { get; private set; } = position;
	public Vector<int> Velocity { get; } = velocity;

	public void Move()
	{
		Position += Velocity;
	}

	public void ModuloPosition(int xModulus, int yModulus)
	{
		int newX = Position.X % xModulus;
		if (newX < 0) newX += xModulus;
		int newY = Position.Y % yModulus;
		if (newY < 0) newY += yModulus;
		Position = new Point<int>(newX, newY);
	}
}