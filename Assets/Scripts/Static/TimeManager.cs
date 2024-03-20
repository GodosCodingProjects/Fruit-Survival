using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimeManager : MonoBehaviour
{
    DateTime currentTime;

	[SerializeField]
	TextMeshProUGUI txtTime;

	static readonly int MINIMAL_INCREMENT = 15;
	int undisplayedMinutes = 0;

	void Start()
    {
		EventManager.OnAddMinutes += AddMinutes;
		EventManager.OnSetTime += SetTime;
		EventManager.OnSleep += Sleep;

		currentTime = new DateTime(2000, 1, 1, 6, 0, 0);
		Display();
	}

	void Display()
	{
		txtTime.text = "-Time-\n" + currentTime.ToString("HH:mm");
	}

	void Sleep()
    {
		currentTime = new DateTime(2000, 1, 1, 6, 0, 0);
		undisplayedMinutes = 0;
		Display();
    }

	void SetTime(DateTime time)
	{
		currentTime = time;
		Display();
	}

	void AddMinutes(int minutes)
	{
		// Get current time to display
		undisplayedMinutes += minutes;

		// Display
		DisplayIncrements(undisplayedMinutes / MINIMAL_INCREMENT);

		// Remove displayed time
		undisplayedMinutes %= MINIMAL_INCREMENT;
	}

	void DisplayIncrements(int increments)
	{
		currentTime = currentTime.AddMinutes(increments * MINIMAL_INCREMENT);
		PlayerManager.AddBelly(-increments);

		// Player went over their max bedtime
		if(currentTime.Day > 1) // fainting
		{
			PassOut();
		}

		Display();
	}

	void PassOut()
	{
		currentTime = new DateTime(2000, 1, 1, 8, 0, 0);
		undisplayedMinutes = 0;

		Debug.Log("It's way past your bedtime! You fainted...\n\n" +
			"Would you look at that?" +
			"You woke up later than usual and you feel very tired." +
			"\nMaybe go to bed on time tonight?");
		PlayerManager.AddHealth(-20);
		PlayerManager.AddBelly(-20);

		// Close all scenes up to the map
		SceneLoadHelper.GoBackToScene("MissionMap");
	}
}
