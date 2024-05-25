
using UnityEngine;

public class DayCycleSounds : MonoBehaviour
{
    [SerializeField]
    AudioSource ambianceSource;
    [SerializeField]
    AudioClip daySounds;
	[SerializeField]
	AudioClip nightSounds;

    AudioClip currentClip;

	void Start()
    {
        //EventManager.OnSetTime += OnTimeSet;
        //EventManager.OnAddMinutes += OnMinutesAdded;

        currentClip = daySounds;
    }

    //void OnTimeSet(DateTime time)
    //{
    //    UpdateAmbiance();
    //}

    //void OnMinutesAdded(int minutes)
    //{
    //    UpdateAmbiance();
    //}

	void Update()
	{
        if(TimeManager.CurrentTime.Hour > 17 || TimeManager.CurrentTime.Hour < 6)
        {
            if(currentClip != nightSounds)
            {
                currentClip = nightSounds;
				ambianceSource.clip = nightSounds;
                ambianceSource.Play();
			}
        }
        else
        {
			if(currentClip != daySounds)
			{
                currentClip = daySounds;
				ambianceSource.clip = daySounds;
				ambianceSource.Play();
			}
		}
    }
}
