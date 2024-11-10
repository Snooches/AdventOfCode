using Utilities.Interfaces;

namespace AoC2020.Day21;

public class InputDataConverter : IInputDataConverter<IEnumerable<Food>>
{
	public IEnumerable<Food> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			Food food = new();
			string[] split = line.Split();
			int index = split.ToList().IndexOf("(contains");
			if (index < 0)
				index = split.Length;
			foreach (string ingredient in split[0..index])
				food.Ingredients.Add(ingredient);
			if (index < split.Length - 1)
				foreach (string allergen in split[(index + 1)..])
					food.Allergens.Add(allergen.Trim(')').Trim(','));
			yield return food;
		}
	}
}