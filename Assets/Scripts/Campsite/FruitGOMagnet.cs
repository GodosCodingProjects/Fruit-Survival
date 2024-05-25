using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitGOMagnet : MonoBehaviour
{
	[HideInInspector]
	public FruitGO fruit;

	[SerializeField]
	AudioSource addSource;
	[SerializeField]
	AudioSource removeSource;

	void OnTriggerEnter(Collider collider)
	{
		if(fruit == null)
		{
			DragManager.AddMagnet(this);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		DragManager.RemoveMagnet(this);
	}

	public void AddFruit(FruitGO fruit)
	{
		if(this.fruit == null)
		{
			this.fruit = fruit;
			DragManager.RemoveMagnet(this);
			addSource.Play();
		}
	}

	public void RemoveFruit(FruitGO fruit)
	{
		if(this.fruit == fruit)
		{
			this.fruit = null;
			removeSource.Play();
		}
	}
}
