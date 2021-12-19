using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    private Canvas tutorialUI;

    private void OnValidate()
    {
        tutorialUI = GetComponent<Canvas>();
    }

    private void Start()
    {
        Subscribe(true);
    }

    private void OnDisable()
    {
        Subscribe(false);
    }

    private void Subscribe(bool subscribe)
    {
        if (subscribe)
        {
            GameManager.Instance.OnLevelLoadingComplete += GameManager_LevelLoadingComplete;
            InputSystem.Instance.OnTouch += InputSystem_Touch;
        }   
        else
        {
            GameManager.Instance.OnLevelLoadingComplete -= GameManager_LevelLoadingComplete;
        }
    }

    private void InputSystem_Touch()
    {
        Show(false);
    }

    private void GameManager_LevelLoadingComplete(int level)
    {
        Show(true);
    }

    public void Show(bool show)
    {
        tutorialUI.enabled = show;
    }




}
