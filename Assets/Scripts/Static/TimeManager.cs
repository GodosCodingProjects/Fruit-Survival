
using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public static DateTime CurrentTime { get; private set; }

	[SerializeField]
	TextMeshProUGUI txtTime;

	static readonly int MINIMAL_INCREMENT = 15;
	int undisplayedMinutes = 0;

	void Start()
    {
		EventManager.OnAddMinutes += AddMinutes;
		EventManager.OnSetTime += SetTime;
		EventManager.OnSleep += Sleep;

		CurrentTime = new DateTime(2000, 1, 1, 6, 0, 0);
		Display();
	}

	void Display()
	{
		txtTime.text = "-Time-\n" + CurrentTime.ToString("HH:mm");
	}

	void Sleep()
    {
		CurrentTime = new DateTime(2000, 1, 1, 6, 0, 0);
		undisplayedMinutes = 0;
		Display();
    }

	void SetTime(DateTime time)
	{
		CurrentTime = time;
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
		CurrentTime = CurrentTime.AddMinutes(increments * MINIMAL_INCREMENT);
		PlayerManager.AddBelly(-increments);

		// Player went over their max bedtime
		if(CurrentTime.Day > 1) // fainting
		{
			PassOut();
		}

		Display();
	}

	void PassOut()
	{
		CurrentTime = new DateTime(2000, 1, 1, 8, 0, 0);
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
