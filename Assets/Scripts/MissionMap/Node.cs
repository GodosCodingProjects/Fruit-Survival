
using UnityEngine;

public class Node : MonoBehaviour
{
	public Map.Pos pos;
	bool[] validPaths;

	void Awake()
	{
		pos = new Map.Pos();
		validPaths = new bool[Map.maxColmuns];
	}

	void OnMouseUpAsButton()
	{
		Map.CheckMovePlayer(pos);
	}

	public void SetValidPath(int i)
	{
		if(i >= 0 && i < Map.maxColmuns)
		{
			validPaths[i] = true;
		}
	}
	public bool GetValidPath(int i)
	{
		return validPaths[i];
	}
}
