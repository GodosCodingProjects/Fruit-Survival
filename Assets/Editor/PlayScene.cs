using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class PlayScene
{
	/*
	 * Thanks to: Fattie 
	 * Found at:
	 * https://answers.unity.com/questions/441246/editor-script-to-make-play-always-jump-to-a-start.html
	 */

	// IN YOUR EDITOR FOLDER, have SimpleEditorUtils.cs.
	// paste in this text.
	// to play, HIT COMMAND-ZERO rather than command-P
	// (the zero key, is near the P key, so it's easy to remember)
	// simply insert the actual name of your opening scene
	// "__preEverythingScene" on the second last line of code below.
	// click command-0 to go to the prelaunch scene and then play

	//[MenuItem("Edit/Play-Unplay, But From Prelaunch Scene %0")]
	//public static void PlayFromPrelaunchScene()
	//{
	//	if(EditorApplication.isPlaying == true)
	//	{
	//		EditorApplication.isPlaying = false;
	//		return;
	//	}
	//	EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
	//	EditorSceneManager.OpenScene(
	//				"Assets/Scenes/MissionMap.unity");
	//	EditorApplication.isPlaying = true;
	//}

	/*
	 * Thanks to: YinXiaozhou
	 * Found at:
	 * https://answers.unity.com/questions/441246/editor-script-to-make-play-always-jump-to-a-start.html
	 */
	// This method is called before any Awake. It's the perfect callback for this feature
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void LoadFirstSceneAtGameBegins()
	{
		if(EditorBuildSettings.scenes.Length == 0)
		{
			Debug.LogWarning("The scene build list is empty. Can't play from first scene.");
			return;
		}

		SceneManager.LoadScene("MissionMap");
	}
}
