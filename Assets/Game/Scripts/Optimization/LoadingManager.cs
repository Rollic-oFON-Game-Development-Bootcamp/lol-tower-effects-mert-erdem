using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("ExtraScene", LoadSceneMode.Additive);
    }

    [Button]
    void ReloadScene()
    {
        SceneManager.UnloadSceneAsync("UIAndInput");
        var asyncOp = SceneManager.LoadSceneAsync("UIAndInput", LoadSceneMode.Additive);
    }
}
