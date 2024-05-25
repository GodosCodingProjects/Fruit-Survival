using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InGameEventManager : MonoBehaviour
{
	static InGameEventManager instance = null;

	[SerializeField]
	InGameEventPopup popup;

	[SerializeField]
	PlayerMover playerMover;

	[SerializeField]
	AudioSource openSource;

	void Start()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public enum InGameEvent
	{
		MISSION_START,
		WOLVES,
		SAD,
		HAPPY,
		THORNY_PATH,
		FAIRY_LAKE,
		DOUBLE_RECIPE,
		AWAKE_LATE,
		AWAKE_EARLY,
		REACHED_OBJECTIVE,
		REACHED_OBJECTIVE_2,
	}

	static string[] messages = new string[]
	{
		"From the base camp, you see a plane passing in the air, flying dangerously low. It crashes deep into the forest! You decide to go check if someone survived the crash.",
		"You are attacked by a group of wolves!",
		"You are feeling sad :(",
		"You are feeling happy! :)",
		"You go through a field of thorny bushes. It hurts!",
		"You encounter a beautiful lake. The water seems to be glowing slightly. Do you want to take a break here?",
		"Somehow, you seem to have cooked more than usual.",
		"You slept poorly and woke up a little bit late today.",
		"The sun came shining earlier than usual today. You woke up early.",
		"You arrive at your destination. You find the crashed airplane you saw in the distance. You don't see any pilot around, but the airplane radio seems to be in working conditions.",
		"You pick it up and decide to camp here for the night, in case the pilot comes back. If no one is here in the morning, you'll have to return to the base camp.",
	};

	static string[][] options = new string[][]
	{
		new string[] { "Quickly!" },
		new string[] { "Fight! ... (and lose health and belly)", "... or Flight! (and lose some fruits)" },
		new string[] { "Okay" },
		new string[] { "Yeah" },
		new string[] { "Ouch :s" },
		new string[] { "Yes. A quick bath wouldn't hurt.", "No, I'm in a hurry." },
		new string[] { "Nice!" },
		new string[] { "Ugh." },
		new string[] { "HEYYYAAAAY! WHAT'S GOING ON?" },
		new string[] { "Take the radio back to base camp" },
		new string[] { "Maybe someone will answer to the radio?" },
	};

	static UnityAction[][] callbacks = new UnityAction[][]
	{
		new UnityAction[] { Ok },
		new UnityAction[] { Fight, Flight },
		new UnityAction[] { Ok },
		new UnityAction[] { Ok },
		new UnityAction[] { () => { PlayerManager.AddHealth(-20); } },
		new UnityAction[] { Bathe, Ok },
		new UnityAction[] { Ok },
		new UnityAction[] { () => { EventManager.OnSetTime(new DateTime(2000, 1, 1, 6, 36, 0)); } },
		new UnityAction[] { () => { EventManager.OnSetTime(new DateTime(2000, 1, 1, 5, 0, 0)); } },
		new UnityAction[] { ReachedObjective2 },
		new UnityAction[] { Ok },
	};
	static void Ok() { }
	static void Fight()
	{
		PlayerManager.AddHealth(-20);
		PlayerManager.AddBelly(-10);
		MapProperties.FoughtWolves();
	}
	static void Flight()
	{
		for(int i = 0; i < PlayerManager.foodInventory.Count; ++i)
		{
			if (PlayerManager.foodInventory[i] > 0)
			{
				PlayerManager.foodInventory.Remove(i);
				if(EventManager.OnInventoryChange != null)
					EventManager.OnInventoryChange();
				return;
			}
		}
	}
	static void Bathe() {
		PlayerManager.AddHealth(100);
		PlayerManager.AddBelly(100);

		if(EventManager.OnAddMinutes != null)
			EventManager.OnAddMinutes(45);
	}

	static void ReachedObjective2()
	{
		instance.StartCoroutine("ReachedObjective2_IE");
	}

	IEnumerator ReachedObjective2_IE()
	{
		yield return new WaitForSeconds(0.1f);
		DisplayEvent(InGameEvent.REACHED_OBJECTIVE_2);
	}

	public static void DisplayEvent(InGameEvent e)
    {
		instance.openSource.Play();

		instance.popup.gameObject.SetActive(true);
		instance.popup.DisplayEvent(
			messages[(int)e],
			options[(int)e],
			callbacks[(int)e]
        );
	}

	public static void HideEvent()
    {
		instance.popup.gameObject.SetActive(false);
    }

	public static bool IsOpened()
	{
		return instance.popup.gameObject.activeSelf;
	}
}
