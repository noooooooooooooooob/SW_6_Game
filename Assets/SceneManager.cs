using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int SceneToLoad;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
        }
    }

    public void LoadScene(int scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
