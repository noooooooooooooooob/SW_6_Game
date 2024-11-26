using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTransition : MonoBehaviour
{
    public bool BossDefeated;
    public bool PlayerDefeated;
    public bool inTransition;
    SceneManager sceneManager;
    Fade fade;
    void Start()
    {
        BossDefeated = false;
        PlayerDefeated = false;
        inTransition = false;

        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        sceneManager.PreLoadNextScene();
        fade = GameObject.Find("FadePanel").GetComponent<Fade>();

    }

    void Update()
    {

    }

    public void SetBossDefeated()
    {
        BossDefeated = true;
        sceneManager.NextScene();
        StartCoroutine(fade.FadeOut());
    }

    public void SetPlayerDefeated()
    {
        PlayerDefeated = true;
        sceneManager.LoadScene(0);
        StartCoroutine(fade.FadeOut());
    }
}
