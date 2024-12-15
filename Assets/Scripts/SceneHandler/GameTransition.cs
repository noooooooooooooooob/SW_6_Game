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
    TimeManager timeManager;
    GameManager gameManager;

    void Start()
    {
        BossDefeated = false;
        PlayerDefeated = false;
        inTransition = false;

        sceneManager = GetComponent<SceneManager>(); //Local Files
        gameManager = GetComponent<GameManager>(); //Local Files
        sceneManager.PreLoadNextScene();
        fade = GameObject.Find("FadePanel").GetComponent<Fade>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        timeManager = GameObject.Find("Time").GetComponent<TimeManager>();
    }
    public void SetBossDefeated()
    {
        BossDefeated = true;
        timeManager.UpdateCurScore();
        if (playerMovement != null)
        {
            playerMovement.VictoryRun();
        }
    }

    public void PrepNextScene()
    {
        if (sceneManager.isLastScene())
        {
            gameManager.ShowGameClearUI();
        }
        else
        {
            StartCoroutine(fade.FadeOut());
            Invoke("StartNextScene", fade.fadeDuration + 0.3f);
        }
    }

    public void StartNextScene()
    {
        sceneManager.NextScene();
    }

    public void SetPlayerDefeated()
    {
        if (!PlayerDefeated)
        {
            PlayerDefeated = true;
            gameManager.ShowGameOverUI();
        }
    }
}
