using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Canvas levelUI;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI objectsCountText;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        SetObjectCountText(0, 10);
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
            GameManager.Instance.OnInit += GameManager_Init;
            GameManager.Instance.OnLevelLoadingComplete += GameManager_LevelLoadingComplete;
            //LevelController.Instance.OnObjectsCountChanged += LevelController_ObjectsCountChanged;
        }
        else
        {
            GameManager.Instance.OnInit -= GameManager_Init;
            GameManager.Instance.OnLevelLoadingComplete -= GameManager_LevelLoadingComplete;
        }
    }

    public void Show(bool show)
    {
        levelUI.enabled = show;
    }

    private void GameManager_LevelLoadingComplete(int level)
    {
        SetLevelText(level);
    }

    private void GameManager_Init()
    {
        restartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        GameManager.Instance.RestartLevel();
    }

    private void SetLevelText(int level)
    {
        levelText.text = $"Level {level}";
    }

    private void SetObjectCountText(int brokenObjectsCount, int maxObjectsCount)
    {
        objectsCountText.text = $"{brokenObjectsCount}/{maxObjectsCount}"; 
    }
}
