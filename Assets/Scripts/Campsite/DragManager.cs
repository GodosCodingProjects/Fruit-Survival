using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DragManager
{
    static FruitGO heldFruit = null;
    static FruitGOMagnet hoveredMagnet = null;

    public static bool AddFruit(FruitGO fruit)
    {
		if(heldFruit != null)
        {
            return false;
        }

        heldFruit = fruit;
        return true;
    }

	public static bool RemoveFruit(FruitGO fruit)
    {
		if(heldFruit != fruit)
        {
            return false;
        }

        if(hoveredMagnet != null)
        {
            heldFruit.AddMagnet(hoveredMagnet);
        }

        heldFruit = null;
        return true;
    }

	public static void AddMagnet(FruitGOMagnet magnet)
    {
		if(hoveredMagnet == null)
        {
			hoveredMagnet = magnet;
		}
    }

	public static void RemoveMagnet(FruitGOMagnet magnet)
    {
		if(hoveredMagnet != magnet)
		{
			return;
		}

        hoveredMagnet = null;
	}
}
