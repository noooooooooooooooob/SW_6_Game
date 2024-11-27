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
    PlayerMovement playerMovement;
    void Start()
    {
        BossDefeated = false;
        PlayerDefeated = false;
        inTransition = false;

        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        sceneManager.PreLoadNextScene();
        fade = GameObject.Find("FadePanel").GetComponent<Fade>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    void Update()
    {

    }

    public void SetBossDefeated()
    {
        BossDefeated = true;
        playerMovement.VictoryRun();
    }

    public void PrepNextScene()
    {
        Debug.Log("StartFade");
        StartCoroutine(fade.FadeOut());
        Invoke("StartNextScene", fade.fadeDuration + 0.3f);
    }

    public void StartNextScene()
    {
        sceneManager.NextScene();
    }

    public void SetPlayerDefeated()
    {
        PlayerDefeated = true;
        sceneManager.LoadScene(0);
        // StartCoroutine(fade.FadeOut());
    }

}
