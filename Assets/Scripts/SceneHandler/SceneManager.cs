using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int TotalSceneCount;
    public bool ManualSceneSelection;
    public int SceneToLoad;
    public int CurrentScene;

    private AsyncOperation preLoadOperation;
    void Start()
    {
        TotalSceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        CurrentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
        }
    }
    public void PreLoadNextScene()
    {
        if (CurrentScene + 1 < TotalSceneCount)
        {
            preLoadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(CurrentScene + 1);
            preLoadOperation.allowSceneActivation = false;
        }
        else
        {
            Debug.Log("CurrentScene " + CurrentScene + " TotalSceneCount " + TotalSceneCount);
            Debug.Log("No more scenes to load");
        }
    }
    public void NextScene()
    {
        if (preLoadOperation != null && preLoadOperation.progress >= 0.9f)
        {
            preLoadOperation.allowSceneActivation = true;
        }
        else
        {
            Debug.Log("Scene not loaded yet");
        }

    }
    public void LoadScene(int scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void ManualSelectedScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
    }
}
