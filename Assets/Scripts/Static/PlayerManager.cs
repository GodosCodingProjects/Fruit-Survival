
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerManager
{
	public static int Belly
	{
		get;
		private set;
	} = 100;

	public static int Health
	{
		get;
		private set;
	} = 100;

	public static List<int> foodInventory = new List<int>();

	public static void Gather()
	{
		int gatheringLevel = MapProperties.Gathering();
		int nFruitsFound = Random.Range(0 + gatheringLevel, 2 + gatheringLevel);

		for(int i = 0; i < nFruitsFound; ++i)
		{
			foodInventory.Add(Random.Range(0, Food.NB_FRUITS));
		}

		if(EventManager.OnInventoryChange != null)
			EventManager.OnInventoryChange();
	}

	public static void DayEnd()
	{
		if(Belly == 100)
		{
			AddHealth(20);
		}

		AddBelly(-50);
	}

	public static void Eat(Food.foods food)
	{
		if(!foodInventory.Contains((int)food))
			return;

		foodInventory.Remove((int)food);

		AddBelly(Food.bellyValues[(int)food]);
		AddHealth(Food.healthValues[(int)food]);

		if (EventManager.OnInventoryChange != null)
			EventManager.OnInventoryChange();
	}

	public static void AddBelly(int delta)
	{
		if(Belly + delta <= 0)
		{
			AddHealth(Belly + delta);

			Belly = 0;
		}
		else if(Belly + delta >= 100)
		{
			Belly = 100;
		}
		else
		{
			Belly += delta;
		}

		if(EventManager.OnBellyChange != null)
		{
			EventManager.OnBellyChange();
		}
	}

	public static void AddHealth(int delta)
	{
		if(Health + delta <= 0)
		{
			Health = 0;

			if(EventManager.OnMissionLoss != null)
			{
				EventManager.OnMissionLoss();
			}

			SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
		}
		else if(Health + delta >= 100)
		{
			Health = 100;
		}
		else
		{
			Health += delta;
		}

		if(EventManager.OnHealthChange != null)
		{
			EventManager.OnHealthChange();
		}
	}
}