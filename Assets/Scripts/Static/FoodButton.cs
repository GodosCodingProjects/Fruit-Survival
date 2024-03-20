using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodButton : MonoBehaviour
{
    [HideInInspector]
    public Food.foods foodType;

    public void OnClick()
    {
        PlayerManager.Eat(foodType);

		if(EventManager.OnAddMinutes != null)
			EventManager.OnAddMinutes(24);
	}
}
