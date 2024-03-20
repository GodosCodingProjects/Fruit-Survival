using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHelper : MonoBehaviour
{
	static SceneLoadHelper instance;

	[HideInInspector]
	public List<GameObject> sceneContents;

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

		sceneContents = new List<GameObject>();
	}

	public static void UnloadScene(string sceneName)
	{
		instance.sceneContents.RemoveAt(instance.sceneContents.Count - 1);
		instance.StartCoroutine(instance.Unloading(sceneName));
	}

	private IEnumerator Unloading(string sceneName)
	{
		AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);

		while (ao.isDone)
		{
			yield return null;
		}

		sceneContents[sceneContents.Count - 1].SetActive(true);
	}

	public static void LoadScene(string sceneName)
	{
		instance.StartCoroutine(instance.Loading(sceneName));
		instance.sceneContents[instance.sceneContents.Count - 1].SetActive(false);
	}

	private IEnumerator Loading(string sceneName)
	{
		AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

		while (ao.isDone)
		{
			yield return null;
		}
	}

	public static void AddSceneContent(GameObject sceneContent)
	{
		instance.sceneContents.Add(sceneContent);
	}

	public static void GoBackToScene(string sceneName)
	{
		// Checking if the scene exists on my stack
		bool sceneFound = false;
		foreach(var sceneContent in instance.sceneContents)
		{
			if(sceneContent.scene.name.Equals(sceneName))
			{
				sceneFound = true;
				break;
			}
		}

		// The scene exists, delete all scenes higher up on the stack
		if(sceneFound)
		{
			for(int i = instance.sceneContents.Count - 1; i >= 0 ; --i)
			{
				string name = instance.sceneContents[i].scene.name;
				if(name.Equals(sceneName))
				{
					return;
				}
				else
				{
					UnloadScene(name);
				}
			}
		}
	}
}
