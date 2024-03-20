using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class EventManager
{
	public delegate void Event();
	public delegate void MinuteAddEvent(int minutes);
	public delegate void TimeSetEvent(DateTime time);

	public static Event OnInventoryChange;
	public static Event OnBellyChange;
	public static Event OnHealthChange;

	public static MinuteAddEvent OnAddMinutes;
	public static TimeSetEvent OnSetTime;
	public static Event OnSleep;

	public static Event OnMissionLoss;
	public static Event OnMissionWin;
}
