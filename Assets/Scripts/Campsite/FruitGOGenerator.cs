using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGOGenerator : MonoBehaviour
{
    private static int columns = 8;
    private static float spacing = 1.2f;

    public GameObject FruitGO;

    [SerializeField]
    private Sprite[] fruitSprites;

    void Start()
    {
		for(int i = 0; i < PlayerManager.foodInventory.Count; ++i)
        {
            int fruit = PlayerManager.foodInventory[i];

            if(fruit >= Food.NB_FRUITS)
            {
                continue;
            }

			GameObject fruitGO = Instantiate(FruitGO,
            new Vector3
            (
                transform.position.x + (i % columns) * spacing,
                transform.position.y - (i / columns) * spacing,
                -2
            ), Quaternion.identity);

			fruitGO.transform.parent = transform;

            FruitGO fruitScript = fruitGO.GetComponent<FruitGO>();
            fruitScript.SetType((Food.foods)fruit);
			SetSprite(fruit, fruitScript);
		}
    }

    void SetSprite(int fruit, FruitGO fruitScript)
    {
        if(Food.fruitSprites[0] == -1)
        {
            List<int> indexes = new List<int>{ 0, 1, 2, 3 };
			for(int i = 0; i < 4; ++i)
            {
                int index = Random.Range(0, indexes.Count - 1);
				Food.fruitSprites[i] = indexes[index];
                indexes.RemoveAt(index);
            }
		}

		fruitScript.SetSprite(fruitSprites[Food.fruitSprites[fruit]]);
	}
}
