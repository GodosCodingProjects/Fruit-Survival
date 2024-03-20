using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameEventPopup : MonoBehaviour
{
    [SerializeField]
    GameObject popupWindow;
	[SerializeField]
	TextMeshProUGUI questionText;

	[SerializeField]
	GameObject[] buttonGOs;
	Button[] buttons;
	TextMeshProUGUI[] buttonTexts;

	UnityAction[] previousCallbacks;

	void Start()
    {
        buttonTexts = new TextMeshProUGUI[buttonGOs.Length];
        buttons = new Button[buttonGOs.Length];
        int i = 0;
        foreach(var buttonGO in buttonGOs)
        {
            buttonTexts[i] = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            buttons[i] = buttonGO.GetComponent<Button>();
            ++i;

            buttonGO.SetActive(false);
        }

        popupWindow.SetActive(false);
    }

    public void DisplayEvent(string msg, string[] options, UnityAction[] callbacks)
    {
		questionText.text = msg;

        for(int i = 0; i < options.Length; ++i)
        {
			buttonGOs[i].SetActive(true);
			buttons[i].onClick.AddListener(callbacks[i]);
			buttons[i].onClick.AddListener(Hide);
			buttonTexts[i].text = options[i];
        }

        // Memorize callbacks in order to remove them later on
        // (can't simply do a hard reset)
        previousCallbacks = callbacks;
    }

	public void Hide()
	{
		for(int i = 0; i < previousCallbacks.Length; ++i)
		{
			buttonGOs[i].SetActive(false);
            buttons[i].onClick.RemoveAllListeners();
		}

        InGameEventManager.HideEvent();
	}
}
