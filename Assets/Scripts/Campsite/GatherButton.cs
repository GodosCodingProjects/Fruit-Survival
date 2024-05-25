
using UnityEngine;

public class GatherButton : MonoBehaviour
{
    public void OnClick()
    {
		if(EventManager.OnAddMinutes != null)
			EventManager.OnAddMinutes(48);
		PlayerManager.Gather();

		if(MapProperties.GetWolves())
			InGameEventManager.DisplayEvent(InGameEventManager.InGameEvent.WOLVES);
    }
}
