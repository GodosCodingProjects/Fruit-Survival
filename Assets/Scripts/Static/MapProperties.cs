
using System.Collections.Generic;
using UnityEngine;

/*
 * Saved data for what is contained inside each node of the map.
 * This data can then be used to make sure changes applied to a 
 * node are saved when coming back through that node.
 */
public static class MapProperties
{
    public class NodeProperties
    {
        public int gatherLevel = 2;
        public bool wolvesPresent = false;

        public bool builtTent = false;
        public bool builtCampfire = false;
        public bool builtGarden = false;

		public int[] gardenContent =
		{
			-1, -1, -1,
			-1, -1, -1
		};
	}

	static List<List<NodeProperties>> nodesProperties;
    static NodeProperties currentNodeProperties;
    public static void InitNodesProperties(List<int> shape)
    {
        nodesProperties = new List<List<NodeProperties>>();
		for(int i = 0; i < shape.Count; ++i)
        {
            nodesProperties.Add(new List<NodeProperties>());
            for(int j = 0; j < shape[i]; ++j)
            {
                nodesProperties[i].Add(new NodeProperties());

                if(Random.value < 0.2f)
                {
                    nodesProperties[i][j].wolvesPresent = true;
				}
            }
        }
        EventManager.OnSleep += Grow;
	}

	public static bool GetWolves()
	{
		return currentNodeProperties.wolvesPresent;
	}

	public static bool GetBuiltTent()
	{
		return currentNodeProperties.builtTent;
	}

	public static bool GetBuiltCampfire()
	{
		return currentNodeProperties.builtCampfire;
	}

	public static bool GetBuiltGarden()
	{
		return currentNodeProperties.builtGarden;
	}

	public static void FoughtWolves()
	{
		currentNodeProperties.wolvesPresent = false;
	}

	public static void BuiltTent()
	{
		if(EventManager.OnAddMinutes != null)
			EventManager.OnAddMinutes(36);
		currentNodeProperties.builtTent = true;
	}

	public static void BuiltCampfire()
	{
		if(EventManager.OnAddMinutes != null)
			EventManager.OnAddMinutes(36);
		currentNodeProperties.builtCampfire = true;
	}

	public static void BuiltGarden()
	{
		if(EventManager.OnAddMinutes != null)
			EventManager.OnAddMinutes(36);
		currentNodeProperties.builtGarden = true;
	}

	public static int Gathering()
	{
		return currentNodeProperties.gatherLevel--;
	}

	public static void MovingTo(int row, int col)
	{
		currentNodeProperties = nodesProperties[row][col];
	}

	static void Grow()
    {
        foreach(var row in nodesProperties)
        {
            foreach(var node in row)
            {
                if(Random.value > 0.3f)
                {
					node.gatherLevel = Mathf.Min(node.gatherLevel + 1, 2);
				}
            }
        }
    }

	public static void SetGarden(int i, int fruit)
	{
		currentNodeProperties.gardenContent[i] = fruit;
	}

	public static int[] GetGarden()
	{
		return currentNodeProperties.gardenContent;
	}
}
