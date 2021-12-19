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






}
