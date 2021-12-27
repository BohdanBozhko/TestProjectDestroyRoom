using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoSingleton<MainMenuUI> 
{
    [SerializeField] private WinUI winUI;
    [SerializeField] private TutorialUI tutorialUI;
    [SerializeField] private LevelUI levelUI;

    public WinUI WinUI => winUI;
    public TutorialUI TutorialUI => tutorialUI;
    public LevelUI LevelUI => levelUI;

    private void Start()
    {
        Subscribe(true);
    }

    private void OnDestroy()
    {
        Subscribe(false);
    }

    private void GameManager_LevelEnd(bool win)
    {
        winUI.Show();
        levelUI.Hide();
    }

    private void Subscribe(bool subscribe)
    {
        if (subscribe)
        {
            GameManager.Instance.OnLevelLoadingComplete += GameManager_LevelLoadingComplete;
            GameManager.Instance.OnLevelEnd += GameManager_LevelEnd;
            
        }
        else
        {
            GameManager.Instance.OnLevelEnd -= GameManager_LevelEnd;
            GameManager.Instance.OnLevelLoadingComplete -= GameManager_LevelLoadingComplete;
        }
    }

    public void InitNewLevel(int level, int maxObjectCount)
    {
        levelUI.Show();
        levelUI.LevelUpdate(level, maxObjectCount);
    }

    private void GameManager_LevelLoadingComplete(int level)
    {
        tutorialUI.Show();
    }
}
