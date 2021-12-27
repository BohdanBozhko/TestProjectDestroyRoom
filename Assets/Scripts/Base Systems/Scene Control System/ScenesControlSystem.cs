using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public static class SCS
{
    public static ScenesControlSystem Loader => ScenesControlSystem.Instance;
}

public class ScenesControlSystem : MonoSingleton<ScenesControlSystem>
{
    public event Action OnLevelLoaded;

    public ScenesRandomizer scenesRandomizer;
    [Tooltip("Either the base scene or the additional scene must be activated")]
    [SerializeField] private bool isAdditiveSceneActive; 

    public int scenesCount => SceneManager.sceneCountInBuildSettings - 1; //substract base scene
    private int currentSceneIndex = 1;
    private int baseSceneIndex = 0;
    private int lastSceneIndex = -1;


    private void OnEnable()
    {
        GameManager.Instance.OnLevelLoadingTriggered += GameManager_LevelLoadingTriggered;
        DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelLoadingTriggered -= GameManager_LevelLoadingTriggered;
    }

    private void Start()
    {
        OnLevelLoaded?.Invoke();
    }

    private void GameManager_LevelLoadingTriggered(int levelNumber)
    {
        LoadLevel(levelNumber);
    }
    public void LoadLevel(int levelNumber)
    {
        StartCoroutine(LoadLevelRoutine(ConvertLevelNumberToSceneIndex(levelNumber)));
    }

    private int ConvertLevelNumberToSceneIndex(int levelNumber)
    {
        if (levelNumber <= scenesCount)
        {
            return levelNumber;
        }
        else
        {
            return scenesRandomizer.GetRandomSceneIndex();
        }
    }

    public void ReloadLevel() 
    {
        StartCoroutine(ReloadLevelRoutine());
    }

    private IEnumerator LoadLevelRoutine(int sceneIndex)
    { 
        int firstSceneIndex = 1; //Levels start after base scene with index "0"
        yield return ScenesUnloadRoutine(firstSceneIndex);
        currentSceneIndex = sceneIndex;
        yield return StartCoroutine(SceneLoadRoutine());
        yield return null; //Skip one frame after scene has been loaded and then invoke event
        OnLevelLoaded?.Invoke();
    }


    private IEnumerator ReloadLevelRoutine()
    {
        yield return StartCoroutine(SceneUnloadRoutine());
        yield return StartCoroutine(SceneLoadRoutine());
        yield return null; //Skip one frame after scene has been loaded and then invoke event
        OnLevelLoaded?.Invoke(); 
    }

    private IEnumerator SceneLoadRoutine()
    {
        yield return SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Additive);
        if (isAdditiveSceneActive)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(baseSceneIndex));
        }
    }

    private IEnumerator ScenesUnloadRoutine(int firstSceneIndex)
    {
        for (var i = firstSceneIndex; i < SceneManager.sceneCount; i++)
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
        lastSceneIndex = currentSceneIndex;
    }

    private IEnumerator SceneUnloadRoutine()
    {
        if (currentSceneIndex > 0)
        {
            yield return SceneManager.UnloadSceneAsync(currentSceneIndex);
        }
        lastSceneIndex = currentSceneIndex;
    }
}
