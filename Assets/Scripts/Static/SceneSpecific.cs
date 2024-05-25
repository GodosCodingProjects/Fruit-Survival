
using UnityEngine;

public class SceneSpecific : MonoBehaviour
{
    void Start()
    {
        SceneLoadHelper.AddSceneContent(gameObject);
    }
}
