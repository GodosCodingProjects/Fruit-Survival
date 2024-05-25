
using UnityEngine;

public class FarmingManager : MonoBehaviour
{
    [SerializeField]
    FruitGOMagnet[] magnets;
	[SerializeField]
	FruitGOGenerator fruitGenerator;

	void Start()
	{
		// Spawn the fruits inside the magnets
		for(int i = 0; i < magnets.Length; ++i)
		{
			if(MapProperties.GetGarden()[i] != -1)
			{
				FruitGO fruitGO = fruitGenerator.SpawnFruitAt(
					MapProperties.GetGarden()[i], 
					magnets[i].transform.position
				).GetComponent<FruitGO>();

				magnets[i].AddFruit(fruitGO);
				fruitGO.AddMagnet(magnets[i]);
			}
		}
	}

	void Update()
    {
        for(int i = 0; i < magnets.Length; ++i)
        {
            if(magnets[i].fruit == null)
            {
				if(MapProperties.GetGarden()[i] != -1)
				{
					// Check if null in garden, otherwise, make null and add fruit to inventory
					PlayerManager.foodInventory.Add(MapProperties.GetGarden()[i]);
					MapProperties.SetGarden(i, -1);
					if(EventManager.OnInventoryChange != null)
						EventManager.OnInventoryChange();
				}
			}
            else if((int)magnets[i].fruit.fruitType != MapProperties.GetGarden()[i])
            {
				// Add fruit in the garden and remove from inventory
				PlayerManager.foodInventory.Remove((int)magnets[i].fruit.fruitType);
				MapProperties.SetGarden(i, (int)magnets[i].fruit.fruitType);
				if(EventManager.OnInventoryChange != null)
					EventManager.OnInventoryChange();
			}
		}
    }
}
