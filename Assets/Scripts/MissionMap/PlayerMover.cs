
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
	public Map.Pos pos;
	[SerializeField]
	public float zOrder = -2;

	[SerializeField]
	float speed = 3;
	IEnumerator currentMove;

	[SerializeField]
	AudioSource footstepsAudio;

	public void Init()
	{
		currentMove = null;
		pos = new Map.Pos();
		Move(pos, true);
	}

	public void Move(Map.Pos pos, bool instant = false)
	{
		if(currentMove != null)
		{
			return;
		}

		this.pos = pos;
		Vector3 position = Map.PositionFromMapPos(pos);

		if (instant)
		{
			transform.position = new Vector3(position.x, position.y, zOrder);
		}
		else
		{
			currentMove = MoveOverTime(new Vector3(position.x, position.y, zOrder));
			StartCoroutine(currentMove);
		}
	}

	IEnumerator MoveOverTime(Vector3 position)
	{
		footstepsAudio.Play();

		Vector3 initialPos = transform.position;

		bool eventWasTriggered = false;

		float delta = 0;
		while ((transform.position - position).sqrMagnitude > Vector3.kEpsilon)
		{
			delta += Time.deltaTime;
			transform.position =
				Vector3.Lerp(initialPos, position, delta * speed);

			// Currently always, for testing purposes
			if(!eventWasTriggered && delta * speed > 0.5)
			{
				if(Random.value < 0.5)
				{
					InGameEventManager.InGameEvent randEvent = 
						(InGameEventManager.InGameEvent)Random.Range(0, 4)
						+ (int)InGameEventManager.InGameEvent.SAD;
					InGameEventManager.DisplayEvent(randEvent);
					footstepsAudio.Pause();
					yield return new WaitUntil(() => { return !InGameEventManager.IsOpened(); });
					footstepsAudio.Play();
				}

				eventWasTriggered = true;
			}

			if(EventManager.OnAddMinutes != null)
				EventManager.OnAddMinutes((int)Mathf.Ceil(speed));

			yield return null;
		}

		if(pos.Row == Map.nRows - 1) // Reached the final node (objective of the mission)
		{
			Map.StartMovingBackwards();
			InGameEventManager.DisplayEvent(InGameEventManager.InGameEvent.REACHED_OBJECTIVE);
		}
		else if (pos.Row == 0)
		{
			SceneManager.LoadSceneAsync("Victory", LoadSceneMode.Single);
			StopCoroutine(currentMove);
			currentMove = null;
			yield return null;
		}

		footstepsAudio.Stop();

		currentMove = null;
		SceneLoadHelper.LoadScene("Campsite");
	}
}
