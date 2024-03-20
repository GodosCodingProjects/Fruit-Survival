using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SleepButton : MonoBehaviour
{
	public void OnClick()
	{
		if(!MapProperties.GetBuiltTent())
		{
			MapProperties.BuiltTent();
			//return;
		}

		if(EventManager.OnSleep != null)
			EventManager.OnSleep();

		float rand = Random.value;
		if(rand < 0.2f)
		{
			InGameEventManager.DisplayEvent(InGameEventManager.InGameEvent.AWAKE_LATE);
		}
		else if(rand < 0.45f)
		{
			InGameEventManager.DisplayEvent(InGameEventManager.InGameEvent.AWAKE_EARLY);
		}

		SceneLoadHelper.UnloadScene(gameObject.scene.name);
	}
}
