using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingManager : MonoBehaviour
{
    [SerializeField]
    FruitGOMagnet[] magnets;

    void Update()
    {
        for(int i = 0; i < magnets.Length; ++i)
        {
            if(magnets[i].fruit != null)
            {
				MapProperties.SetGarden(i, (int)magnets[i].fruit.fruitType);
			}
		}
    }
}
