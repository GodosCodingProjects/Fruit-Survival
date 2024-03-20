using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	static Map instance;

	public static readonly int nRows = 5;
	public static readonly int maxColmuns = 3;

	bool goingBackwards = false;

	public class Pos {
		public int Col
		{
			get;
			set;
		} = 0;
		public int Row
		{
			get;
			set;
		} = 0;

		public void Set(int row, int col)
		{
			Row = row;
			Col = col;
		}
		public override string ToString()
		{
			return "(" + Row + ", " + Col + ")";
		}
	}

	[SerializeField]
	GameObject nodePrefab;
	List<List<Node>> nodes;

	[SerializeField]
	GameObject pathPrefab;

	[SerializeField]
	PlayerMover player;

	public static void CheckMovePlayer(Pos pos)
	{
		if(!instance.goingBackwards)
		{
			if(pos.Row == instance.player.pos.Row + 1 &&
			instance.nodes[instance.player.pos.Row][instance.player.pos.Col]
			.GetValidPath(pos.Col))
			{
				MapProperties.MovingTo(pos.Row, pos.Col);
				instance.player.Move(pos);
			}
		}
		else
		{
			if(pos.Row == instance.player.pos.Row - 1 &&
			instance.nodes[pos.Row][pos.Col]
			.GetValidPath(instance.player.pos.Col))
			{
				MapProperties.MovingTo(pos.Row, pos.Col);
				instance.player.Move(pos);
			}
		}
	}

	public static Vector3 PositionFromMapPos(Pos pos)
	{
		return instance.nodes[pos.Row][pos.Col].transform.position;
	}

	public static void StartMovingBackwards()
	{
		instance.goingBackwards = true;
		Debug.Log("Going Backwards now");
	}

	void Awake()
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

	void Start()
	{
		nodes = new List<List<Node>>();

		for(int i = 0; i < nRows; ++i)
		{
			var rand = new System.Random();
			int nNodesInRow = (i == 0 || i == nRows - 1) ? 
				1 : rand.Next(1, maxColmuns + 1);

			nodes.Add(new List<Node>(nNodesInRow));

			for (int j = 0; j < nNodesInRow; ++j)
			{
				Vector2 v2 = UnityEngine.Random.insideUnitCircle;

				Node node = Instantiate(nodePrefab,transform).GetComponent<Node>();
				node.transform.position = new Vector3
				(
					j * 2 - (nNodesInRow - 1) + (v2.x / 2),
					i * 2 - (nRows - 1) + (v2.y / 4),
					-1
				);

				node.pos.Set(i, j);
				nodes[i].Add(node);
			}
		}

		DisplayPaths();

		player.Init();

		List<int> shape = new List<int>();
		for(int i = 0; i < nodes.Count; ++i)
		{
			shape.Add(nodes[i].Count);
		}
		MapProperties.InitNodesProperties(shape);

		StartCoroutine("DisplayStartEvent");
	}

	IEnumerator DisplayStartEvent()
	{
		yield return new WaitForSeconds(0.05f);
		InGameEventManager.DisplayEvent(InGameEventManager.InGameEvent.MISSION_START);
	}

	void DisplayPaths()
	{
		for(int i = 0; i < nodes.Count - 1; ++i)
		{
			int rowCount = nodes[i].Count;
			int nextRowCount = nodes[i + 1].Count;

			for(int j = 0; j < rowCount; ++j)
			{
				int frac = j * nextRowCount / rowCount;
				Node n = nodes[i][j];
				n.SetValidPath(frac);
			}
			for(int j = 0; j < nextRowCount; ++j)
			{
				int frac = j * rowCount / nextRowCount;
				Node n = nodes[i][frac];
				n.SetValidPath(j);
			}

			for(int j = 0; j < rowCount; ++j)
			{
				for(int k = 0; k < nextRowCount; ++k)
				{
					if (nodes[i][j].GetValidPath(k))
					{
						Vector3 p2 = nodes[i][j].transform.position;
						Vector3 p1 = nodes[i + 1][k].transform.position;

						Vector3 diff = p1 - p2;

						float length = diff.magnitude;

						GameObject path = Instantiate(pathPrefab, transform);

						Vector3 pos = p2 + diff / 2;
						path.transform.position = new Vector3(pos.x, pos.y, -1.1f);

						path.transform.localScale = new Vector3(
							0.6f,
							diff.magnitude / 4 - 0.1f,
							1
						);

						path.transform.rotation = Quaternion.LookRotation(
							Vector3.forward,
							diff
						);
					}
				}
			}
		}
	}
}
