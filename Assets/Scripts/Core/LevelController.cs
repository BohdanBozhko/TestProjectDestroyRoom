using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoSingleton<LevelController>
{
    public event Action OnObjectsCountChanged;

    public List<InteractableObject> InteractableObjects;

    private void Start()
    {
        StartCoroutine(InitRoutine());
    }

    private IEnumerator InitRoutine()
    {
        yield return null;
        MainMenuUI.Instance.InitNewLevel(GameManager.Instance.levelNumber, InteractableObjects.Count);
    }

    public void AddInteractableObject(InteractableObject obj)
    {
        InteractableObjects.Add(obj);
    }

    public void RemoveInteractableObject(InteractableObject obj)
    {
        InteractableObjects.Remove(obj);
        OnObjectsCountChanged?.Invoke();
        if (InteractableObjects.Count < 1)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        GameManager.Instance.LevelEnd(true);
    }
}
