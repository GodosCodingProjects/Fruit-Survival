
using TMPro;
using UnityEngine;

public class GardenButton : MonoBehaviour
{
	TextMeshProUGUI text;

	void Start()
	{
		text = GetComponentInChildren<TextMeshProUGUI>();
		if(!MapProperties.GetBuiltGarden())
		{
			text.text = "Build garden";
		}
		else
		{
			text.text = "Go to garden";
		}
	}

	public void OnClick()
	{
		if(!MapProperties.GetBuiltGarden())
		{
			text.text = "Go to garden";
			MapProperties.BuiltGarden();
			return;
		}

		SceneLoadHelper.LoadScene("Garden");
	}
}
