using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private ScreenFade screenFade;
    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "LevelManager";
        screenFade = FindObjectOfType<ScreenFade>();
    }

    public void LoadScene(string sceneName, int id = default)
    {
        StartCoroutine(LoadScene(sceneName, id, LoadSceneMode.Single));
    }

    IEnumerator LoadScene(string sceneName,int id,  LoadSceneMode mode)
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

        var SpawnPositions = FindObjectsOfType<SpawnPosition>();
        foreach (var spawn in SpawnPositions)
        {
            if (spawn.id == id)
            {
                
                FindObjectOfType<PlayerMovement>().transform.position = spawn.Position;
            }
        }

        screenFade.FadeOut();
    }
}
