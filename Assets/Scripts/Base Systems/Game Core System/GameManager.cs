using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public event Action OnInit;
    public event Action<bool> OnLevelEnd;
    public event Action<int> OnLevelLoadingComplete;
    public event Action<int> OnLevelLoadingTriggered;

    private void Start()
    {
        SCS.Loader.OnLevelLoaded += SCS_LevelLoaded;
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
        StartCoroutine(InitLevelRoutine());
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LevelEnd(true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelEnd(false);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }
#endif

    private void SCS_LevelLoaded()
    {
        int levelNumber = SLS.Data.gameLevel;
        OnLevelLoadingComplete?.Invoke(levelNumber);
    }

    private void LoadLevel()
    {
        var levelNumber = SLS.Data.gameLevel;
        OnLevelLoadingTriggered?.Invoke(levelNumber);
    }

    private void UnloadLevel(bool nextLevel)
    {
        if (nextLevel)
        {
            SLS.Data.gameLevel++;
        }
    }

    private void LevelEnd(bool win)
    {
        OnLevelEnd?.Invoke(win);
    }

    public void NextLevel()
    {
        UnloadLevel(true);
        LoadLevel();
    }

    public void RestartLevel()
    {
        UnloadLevel(false);
        LoadLevel();
    }

 
    private IEnumerator InitLevelRoutine()
    {
        //Skip one frame to make new lifecycle "Init" after Start method
        yield return new WaitForEndOfFrame();
        OnInit?.Invoke();
        LoadLevel();
    }
}
