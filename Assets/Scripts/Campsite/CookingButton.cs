
using TMPro;
using UnityEngine;

public class CookingButton : MonoBehaviour
{
	TextMeshProUGUI text;

	void Start()
    {
		text = GetComponentInChildren<TextMeshProUGUI>();
		if(!MapProperties.GetBuiltCampfire())
		{
			text.text = "Build campfire";
		}
		else
		{
			text.text = "Go to campfire";
		}
	}

    public void OnClick()
    {
		if(!MapProperties.GetBuiltCampfire())
		{
			text.text = "Go to campfire";
			MapProperties.BuiltCampfire();
			return;
		}

		SceneLoadHelper.LoadScene("Cooking");
	}
}
