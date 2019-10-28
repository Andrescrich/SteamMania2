using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private ScreenFade screenFade;
    public override void Awake()
    {
        base.Awake();
        gameObject.name = "LevelManager";
        screenFade = FindObjectOfType<ScreenFade>();
        screenFade.Block();
    }

    private void Start()
    {
        StartCoroutine(WaitSecondsToStart());
    }

    IEnumerator WaitSecondsToStart()
    {
        yield return new WaitForSeconds(1.5f);
        screenFade.FadeOut();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName, LoadSceneMode.Single));
    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        screenFade.FadeIn();
        while (screenFade.Active)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);

        while (!operation.isDone)
        {
            yield return null;
        }

        screenFade.FadeOut();
    }
}
