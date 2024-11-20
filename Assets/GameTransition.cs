using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTransition : MonoBehaviour
{
    public bool BossDefeated;
    public bool PlayerDefeated;

    SceneManager sceneManager;
    void Start()
    {
        BossDefeated = false;
        PlayerDefeated = false;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        sceneManager.PreLoadNextScene();
    }

    void Update()
    {

    }

    public void SetBossDefeated()
    {
        BossDefeated = true;
        sceneManager.NextScene();
    }

    public void SetPlayerDefeated()
    {
        PlayerDefeated = true;
        sceneManager.LoadScene(0);
    }
}
