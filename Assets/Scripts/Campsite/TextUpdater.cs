
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextUpdater : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI txtBelly;
	[SerializeField]
	TextMeshProUGUI txtHealth;

	[SerializeField]
	GameObject fruitButtonPrefab;
	[SerializeField]
	GameObject recipeButtonPrefab;

	List<GameObject> buttons;

	[SerializeField]
	Sprite[] fruitSprites;

	[SerializeField]
	AudioSource eatingSource;

	void Start()
	{
		buttons = new List<GameObject>();

		EventManager.OnBellyChange += DisplayBelly;
		EventManager.OnHealthChange += DisplayHealth;
		EventManager.OnInventoryChange += DisplayInventory;

		DisplayBelly();
		DisplayHealth();
		DisplayInventory();
	}

	private void DisplayBelly() {
		txtBelly.text = "Belly: " + PlayerManager.Belly;
	}

	private void DisplayHealth()
	{
		txtHealth.text = "Health: " + PlayerManager.Health;
	}

	private void DisplayInventory()
	{
		if(buttons.Count > 0)
		{
			buttons.ForEach(x => Destroy(x));
			buttons.Clear();
		}

		for(int i = 0; i < PlayerManager.foodInventory.Count; ++i)
		{
			int food = PlayerManager.foodInventory[i];

			GameObject foodItem = null;

			if(food < Food.NB_FRUITS)
			{
				foodItem = Instantiate(fruitButtonPrefab);
				SetFruitSprite(food, foodItem);
			}
			else
			{
				foodItem = Instantiate(recipeButtonPrefab);
				SetRecipeColor(food, foodItem);
			}

			foodItem.transform.SetParent(transform, false);
			foodItem.transform.localPosition = new Vector3(-100, -(130 + 70 * i), 0);
			buttons.Add(foodItem);
			foodItem.GetComponent<FoodButton>().foodType = (Food.foods)food;
			foodItem.GetComponent<FoodButton>().eatingSource = eatingSource;
		}
	}

	void SetFruitSprite(int food, GameObject foodItem)
	{
		if(Food.fruitSprites[0] == -1)
		{
			List<int> indexes = new List<int> { 0, 1, 2, 3 };
			for(int i = 0; i < 4; ++i)
			{
				int index = Random.Range(0, indexes.Count - 1);
				Food.fruitSprites[i] = indexes[index];
				indexes.RemoveAt(index);
			}
		}

		foodItem.GetComponent<InvFruit>().img.sprite = fruitSprites[Food.fruitSprites[food]];
	}

	void SetRecipeColor(int food, GameObject foodItem)
	{
		int fruit1 = -1;
		int fruit2 = -1;

		for(int n = 0; n < Food.recipes.Length; ++n)
		{
			int i = n / Food.NB_FRUITS;
			int j = n % Food.NB_FRUITS;

			if((int)Food.recipes[i, j] == food)
			{
				fruit1 = i;
				fruit2 = j;
			}
		}

		foodItem.GetComponent<InvRecipe>().img1.sprite = fruitSprites[Food.fruitSprites[fruit1]];
		foodItem.GetComponent<InvRecipe>().img2.sprite = fruitSprites[Food.fruitSprites[fruit2]];
	}
}
