


public static class Food
{
	public static readonly int NB_FRUITS = 4;
	public enum foods
	{
		A, B, C, D,
		AA, AB, AC, AD,
		BB, BC, BD,
		CC, CD,
		DD
	}

	public static readonly string[] foodNames = new string[]
	{
		"Apple", "Banana", "Coconut", "Durian",
		"Apple purée", "Banana purée", "Sweet juice", "Smelly apple",
		"Banana split", "Tropical meal", "Smelly banana",
		"Coconut water", "Smelly coconut",
		"Smelly smelly"
	};

	public static int[] fruitSprites = new int[]{ -1, -1, -1, -1 };

	public static readonly int[] bellyValues = new int[]
	{ 
		15, 20, 10, 30,
		30, 35, 20, 50,
		40, 25, 55,
		10, 30,
		70
	};

	public static readonly int[] healthValues = new int[]
	{
		0, 0, 5, -20,
		0, 0, 5, -15,
		0, 5, -15,
		15, 0,
		-30
	};

	public static readonly foods[,] recipes = new foods[,]
	{
		{ foods.AA, foods.AB, foods.AC, foods.AD },
		{ foods.AB, foods.BB, foods.BC, foods.BD },
		{ foods.AC, foods.BC, foods.CC, foods.CD },
		{ foods.AD, foods.BD, foods.CD, foods.DD }
	};
}
