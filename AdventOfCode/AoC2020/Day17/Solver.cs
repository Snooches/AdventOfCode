using Utilities;
using Utilities.GameOfLife;
using Utilities.Interfaces;

namespace AoC2020.Day17;

public class Solver : AbstractSolver<bool[,], int, int>
{
	protected override string SolutionTextA => $"After {NumberOfSimulationCycles} Cycles of considering the 3-dimensional space {SolutionValueA} are alive.";
	protected override string SolutionTextB => $"After {NumberOfSimulationCycles} Cycles of considering the 4-dimensional space {SolutionValueB} are alive.";

	public Solver(IInputDataConverter<bool[,]> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
		UpdateGridSizes();
	}

	private void UpdateGridSizes()
	{
		dimensionSize1 = inputData.GetLength(0) + NumberOfSimulationCycles * 2;
		dimensionSize2 = inputData.GetLength(1) + NumberOfSimulationCycles * 2;
		dimensionSizeX = NumberOfSimulationCycles * 2 + 1;
		GridA = new bool[dimensionSize1, dimensionSize2, dimensionSizeX];
		GridB = new bool[dimensionSize1, dimensionSize2, dimensionSizeX, dimensionSizeX];
	}

	public int NumberOfSimulationCycles
	{
		get => _numberOfSimulationCycles;
		set
		{
			_numberOfSimulationCycles = value;
			UpdateGridSizes();
		}
	}

	private int _numberOfSimulationCycles = 1;

	public bool PerformanceOptimized { get; set; } = true;
	private int dimensionSize1;
	private int dimensionSize2;
	private int dimensionSizeX;
	private bool[,,] GridA = new bool[1, 1, 1];
	private bool[,,,] GridB = new bool[1, 1, 1, 1];

	private readonly int[] keepAliveRange = { 2, 3 };
	private readonly int[] reviveRange = { 3 };

	protected override void SolveImplemented()
	{
		if (PerformanceOptimized)
		{
			SolveOptimized();
			return;
		}
		GameOfLifeController<GameOfLifeCell3D, object?> controllerA = new(false);
		GameOfLifeController<GameOfLifeCell4D, object?> controllerB = new(false);
		for (int x = -NumberOfSimulationCycles; x < inputData.GetLength(0) + NumberOfSimulationCycles; x++)
			for (int y = -NumberOfSimulationCycles; y < inputData.GetLength(1) + NumberOfSimulationCycles; y++)
				for (int z = -NumberOfSimulationCycles; z < NumberOfSimulationCycles; z++)
				{
					bool alive = x >= 0 && x < inputData.GetLength(0) &&
											 y >= 0 && y < inputData.GetLength(1) &&
											 z == 0 &&
											 inputData[x, y];
					controllerA.AddCell(new()
					{
						Key = $"{x}/{y}/{z}",
						KeepAliveRange = keepAliveRange,
						ReviveRange = reviveRange,
						IsAlive = alive
					});
					for (int w = -NumberOfSimulationCycles; w < NumberOfSimulationCycles; w++)
					{
						bool alive4D = alive && w == 0;
						controllerB.AddCell(new()
						{
							Key = $"{x}/{y}/{z}/{w}",
							KeepAliveRange = keepAliveRange,
							ReviveRange = reviveRange,
							IsAlive = alive4D
						});
					}
				}
		for (int i = 0; i < NumberOfSimulationCycles; i++)
		{
			controllerA.Step();
			controllerB.Step();
		}
		SolutionValueA = controllerA.LivingCellCount;
		SolutionValueB = controllerB.LivingCellCount;
	}

	private void SolveOptimized()
	{
		for (int x = 0; x < inputData.GetLength(0); x++)
			for (int y = 0; y < inputData.GetLength(1); y++)
				if (inputData[x, y])
				{
					GridA[x + NumberOfSimulationCycles, y + NumberOfSimulationCycles, NumberOfSimulationCycles] = true;
					GridB[x + NumberOfSimulationCycles, y + NumberOfSimulationCycles, NumberOfSimulationCycles, NumberOfSimulationCycles] = true;
				}
		for (int i = 0; i < NumberOfSimulationCycles; i++)
			StepOptimized();
		//Count
		SolutionValueA = SolutionValueB = 0;
		for (int x = 0; x < dimensionSize1; x++)
			for (int y = 0; y < dimensionSize2; y++)
				for (int z = 0; z < dimensionSizeX; z++)
				{
					if (GridA[x, y, z])
						SolutionValueA++;
					for (int w = 0; w < dimensionSizeX; w++)
						if (GridB[x, y, z, w])
							SolutionValueB++;
				}
	}

	private void StepOptimized()
	{
		bool[,,] nextA = new bool[GridA.GetLength(0), GridA.GetLength(1), GridA.GetLength(2)];
		bool[,,,] nextB = new bool[GridB.GetLength(0), GridB.GetLength(1), GridB.GetLength(2), GridB.GetLength(3)];
		for (int x = 0; x < GridB.GetLength(0); x++)
			for (int y = 0; y < GridB.GetLength(1); y++)
				for (int z = 0; z < GridB.GetLength(2); z++)
				{
					if ((GridA[x, y, z] && keepAliveRange.Contains(CountNeighbours(x, y, z)))
						|| (!GridA[x, y, z] && reviveRange.Contains(CountNeighbours(x, y, z))))
						nextA[x, y, z] = true;
					for (int w = 0; w < GridB.GetLength(3); w++)
						if ((GridB[x, y, z, w] && keepAliveRange.Contains(CountNeighbours(x, y, z, w)))
							|| (!GridB[x, y, z, w] && reviveRange.Contains(CountNeighbours(x, y, z, w))))
							nextB[x, y, z, w] = true;
				}
		GridA = nextA;
		GridB = nextB;
	}

	private int CountNeighbours(int x, int y, int z)
	{
		int result = 0;
		for (int xOffset = -1; xOffset <= 1; xOffset++)
			for (int yOffset = -1; yOffset <= 1; yOffset++)
				for (int zOffset = -1; zOffset <= 1; zOffset++)
					if (x + xOffset >= 0 && x + xOffset < GridA.GetLength(0) &&
							y + yOffset >= 0 && y + yOffset < GridA.GetLength(1) &&
							z + zOffset >= 0 && z + zOffset < GridA.GetLength(2) &&
							(xOffset != 0 || yOffset != 0 || zOffset != 0) &&
							GridA[x + xOffset, y + yOffset, z + zOffset])
						result++;
		return result;
	}

	private int CountNeighbours(int x, int y, int z, int w)
	{
		int result = 0;
		for (int xOffset = -1; xOffset <= 1; xOffset++)
			for (int yOffset = -1; yOffset <= 1; yOffset++)
				for (int zOffset = -1; zOffset <= 1; zOffset++)
					for (int wOffset = -1; wOffset <= 1; wOffset++)
						if (x + xOffset >= 0 && x + xOffset < GridB.GetLength(0) &&
								y + yOffset >= 0 && y + yOffset < GridB.GetLength(1) &&
								z + zOffset >= 0 && z + zOffset < GridB.GetLength(2) &&
								w + wOffset >= 0 && w + wOffset < GridB.GetLength(3) &&
								(xOffset != 0 || yOffset != 0 || zOffset != 0 || wOffset != 0) &&
								GridB[x + xOffset, y + yOffset, z + zOffset, w + wOffset])
							result++;
		return result;
	}
}