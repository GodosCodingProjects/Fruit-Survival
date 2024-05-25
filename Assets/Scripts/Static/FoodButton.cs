
using UnityEngine;

public class FoodButton : MonoBehaviour
{
    [HideInInspector]
    public Food.foods foodType;

    public AudioSource eatingSource;

    public void OnClick()
    {
        PlayerManager.Eat(foodType);
        eatingSource.Play();

		if(EventManager.OnAddMinutes != null)
			EventManager.OnAddMinutes(24);
	}
}
