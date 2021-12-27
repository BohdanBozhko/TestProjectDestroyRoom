using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : AbstractAnimatedCanvas
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private ParticleSystem vfx;

    private void OnValidate()
    {
        canvas = GetComponent<Canvas>();
        animator = GetComponent<Animator>();
    }


    public override void Show()
    {
        base.Show();
        vfx.Play();
    }

    private void Start()
    {
        nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void OnDestroy()
    {
        nextLevelButton.onClick.RemoveListener(NextLevel);
    }

    private void NextLevel()
    {
        Hide();
        GameManager.Instance.NextLevel();
    }
}
