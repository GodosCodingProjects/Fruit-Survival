
using System.Collections;
using UnityEngine;

public class SleepButton : MonoBehaviour
{
	IEnumerator currentSleep = null;

	public void OnClick()
	{
		if(currentSleep == null)
		{
			if(!MapProperties.GetBuiltTent())
			{
				MapProperties.BuiltTent();
				//return;
			}

			if(EventManager.OnSleep != null)
				EventManager.OnSleep();

			currentSleep = Sleep();
			StartCoroutine(currentSleep);
		}
	}

	IEnumerator Sleep()
	{
		yield return new WaitForSeconds(2.5f);

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
		currentSleep = null;
	}
}
