using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public static CookingManager instance;
    FruitGOMagnet[] magnets;

	void Start()
    {
		magnets = new FruitGOMagnet[2];
        int i = 0;
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Magnet"))
        {
            magnets[i] = go.GetComponent<FruitGOMagnet>();
            ++i;
        }
	}
    
    public void OnClick()
    {
		List<FruitGO> fruits = new List<FruitGO>();
		foreach(FruitGOMagnet magnet in magnets)
		{
			if(magnet.fruit == null)
			{
				return;
			}

			fruits.Add(magnet.fruit);
		}

		Food.foods recipe = Food.recipes[(int)fruits[0].fruitType, (int)fruits[1].fruitType];
		PlayerManager.foodInventory.Add((int)recipe);

		if(Random.value < 0.2f)
		{
			InGameEventManager.DisplayEvent(InGameEventManager.InGameEvent.DOUBLE_RECIPE);
			PlayerManager.foodInventory.Add((int)recipe);
		}

		foreach(FruitGOMagnet magnet in magnets)
		{
			magnet.RemoveFruit(magnet.fruit);
		}
		foreach(FruitGO fruit in fruits)
		{
			PlayerManager.foodInventory.Remove((int)fruit.fruitType);
			Destroy(fruit.gameObject);
		}

		if(EventManager.OnInventoryChange != null)
			EventManager.OnInventoryChange();
		if(EventManager.OnAddMinutes != null)
			EventManager.OnAddMinutes(48);
	}
}
