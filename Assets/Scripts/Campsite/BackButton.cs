
using UnityEngine;

public class BackButton : MonoBehaviour
{
	public void OnClick()
	{
		SceneLoadHelper.UnloadScene(gameObject.scene.name);
	}
}
