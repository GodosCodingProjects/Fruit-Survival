using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FruitGO : MonoBehaviour
{
	public Food.foods fruitType;
	SpriteRenderer sr;

	bool isDragged;
	FruitGOMagnet magnet;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	public void SetType(Food.foods type)
	{
		fruitType = type;
	}

	public void SetSprite(Sprite sprite)
	{
		sr.sprite = sprite;
	}

	void OnMouseDown()
	{
		if(!isDragged && DragManager.AddFruit(this))
		{
			isDragged = true;

			if(magnet != null)
			{
				magnet.RemoveFruit(this);
				magnet = null;
			}
		}
	}

	void OnMouseUp()
	{
		if(isDragged && DragManager.RemoveFruit(this))
		{
			isDragged = false;
		}
	}

	void OnMouseDrag()
	{
		if(isDragged)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3
			(
				mousePos.x,
				mousePos.y,
				transform.position.z
			);
		}
	}

	public void AddMagnet(FruitGOMagnet magnet)
	{
		if(this.magnet != null)
		{
			return;
		}

		this.magnet = magnet;
		transform.position = new Vector3
		(
			this.magnet.transform.position.x,
			this.magnet.transform.position.y,
			transform.position.z
		);

		magnet.AddFruit(this);
	}
}
