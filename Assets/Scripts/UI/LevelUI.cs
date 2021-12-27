using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelUI : AbstractAnimatedCanvas
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI objectsCountText;
    [SerializeField] private int maxInteractionObjectsCount;
    [SerializeField] private int interactionObjectCount;
    [SerializeField] private ParticleSystem interactionVfx;

    public int Level;


    private void OnDisable()
    {
        LevelController.Instance.OnObjectsCountChanged -= LevelController_ObjectsCountChanged;
    }

    public void LevelUpdate(int level, int maxObjectCount)
    {
        LevelController.Instance.OnObjectsCountChanged += LevelController_ObjectsCountChanged;
        SetMaxObjectCount(maxObjectCount);
        SetObjectCountText(0);
        SetLevelText(level);
    }

    private void LevelController_ObjectsCountChanged()
    {
        interactionObjectCount++;
        SetObjectCountText(interactionObjectCount);
    }

    private void SetLevelText(int level)
    {
        levelText.text = $"Level {level}";
    }

    private void SetObjectCountText(int brokenObjectsCount)
    {
        interactionVfx.Play();
        objectsCountText.text = $"{brokenObjectsCount}/{maxInteractionObjectsCount}"; 
    }

    private void SetMaxObjectCount(int maxObjectCount)
    {
        maxInteractionObjectsCount = maxObjectCount;
    }
}
