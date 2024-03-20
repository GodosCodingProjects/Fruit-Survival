using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

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
