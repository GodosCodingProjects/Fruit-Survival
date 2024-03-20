using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpecific : MonoBehaviour
{
    void Start()
    {
        SceneLoadHelper.AddSceneContent(gameObject);
    }
}
