using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenSceneButton : MonoBehaviour
{
    [SerializeField]
    string sceneName;

    public void OnClick()
    {
        SceneLoadHelper.LoadScene(sceneName);
    }
}
