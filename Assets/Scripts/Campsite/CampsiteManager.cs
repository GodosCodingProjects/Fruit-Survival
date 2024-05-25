
using UnityEngine;

public class CampsiteManager : MonoBehaviour
{
    void Start()
    {
		int[] gardenContent = MapProperties.GetGarden();

		// For every fruit that was farmed previously,
		// add to the player inventory 3x that fruit
		for(int i = 0; i < gardenContent.Length; ++i)
		{
			if(gardenContent[i] >= 0 && gardenContent[i] < Food.NB_FRUITS)
			{
				PlayerManager.foodInventory.Add(gardenContent[i]);
				PlayerManager.foodInventory.Add(gardenContent[i]);
				PlayerManager.foodInventory.Add(gardenContent[i]);

				//Debug.Log("Your patience bears fruits (litteraly)");
			}

			MapProperties.SetGarden(i, -1);
		}

		if(EventManager.OnInventoryChange != null)
			EventManager.OnInventoryChange();
	}
}
