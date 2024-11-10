using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day21;

public class Solver : AbstractSolver<IEnumerable<Food>, int, string>
{
	protected override string SolutionTextA => $"{SolutionValueA}";
	protected override string SolutionTextB => $"{SolutionValueB}";

	public Solver(IInputDataConverter<IEnumerable<Food>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	private readonly Dictionary<string, List<string>> Allergens = new();
	private readonly Dictionary<string, string?> Ingredients = new();

	protected override void SolveImplemented()
	{
		foreach (Food food in inputData)
		{
			foreach (string ingredient in food.Ingredients)
			{
				if (!Ingredients.ContainsKey(ingredient))
					Ingredients.Add(ingredient, null);
			}
			foreach (string allergen in food.Allergens)
			{
				if (!Allergens.ContainsKey(allergen))
					Allergens.Add(allergen, new());
				Allergens[allergen].AddRange(food.Ingredients.Where(ingredient => !Allergens[allergen].Contains(ingredient)));
			}
		}
		Reduce();
		SolutionValueA = 0;
		List<string> saveIngredients = Ingredients.Keys.Where(ingredient => Ingredients[ingredient] is null).ToList();
		foreach (Food food in inputData)
			SolutionValueA += food.Ingredients.Where(ingredient => saveIngredients.Contains(ingredient)).Count();
		var orderedDangerList = Ingredients.Keys.Where(ingredient => Ingredients[ingredient] is not null).OrderBy(ingredient => Ingredients[ingredient]).ToList();

		SolutionValueB = String.Join(',', orderedDangerList);
	}

	private void Reduce()
	{
		foreach (string allergen in Allergens.Keys)
			if (Allergens[allergen].Count == 1)
				Ingredients[Allergens[allergen][0]] = allergen;
		while (Allergens.Values.Any(possibleIngredients => possibleIngredients.Count > 1))
		{
			foreach (string allergen in Allergens.Keys.Where(allergen => Allergens[allergen].Count > 1))
			{
				var possibleIngredients = inputData.Where(food => food.Allergens.Contains(allergen)).Select(food => food.Ingredients).Aggregate((list1, list2) => list1.Intersect(list2).ToList());
				possibleIngredients.RemoveAll(ingredient => Ingredients[ingredient] is not null);
				Allergens[allergen] = possibleIngredients;
				if (possibleIngredients.Count == 1)
					Ingredients[possibleIngredients[0]] = allergen;
			}
		}
	}
}