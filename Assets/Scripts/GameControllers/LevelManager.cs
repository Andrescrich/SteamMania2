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
        screenFade = ScreenFade.GetInstance();
    }

    private void Start()
    {
    }

    public static event Action OnLoadLevelStart = delegate { };
    public static event Action OnLoadLevelEnd = delegate { };

    public void LoadScene(string sceneName,LoadSceneMode sceneMode, int doorID = default)
    {
        StartCoroutine(LoadScene(sceneName, doorID, sceneMode));
    }

    IEnumerator LoadScene(string sceneName,int id,  LoadSceneMode mode)
    {
        OnLoadLevelStart?.Invoke();
        screenFade.FadeIn();
        while (screenFade.Active)
        {
            yield return null;
            
        }
        yield return new WaitForSeconds(0.3f);
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
                //GameManager.Player.SetPosition(spawn.position);
                FindObjectOfType<PlayerMovement>().transform.position = spawn.Position;
            }
        }
        OnLoadLevelEnd?.Invoke();
        yield return new WaitForSeconds(0.3f);
        
        screenFade.FadeOut();
    }
}
