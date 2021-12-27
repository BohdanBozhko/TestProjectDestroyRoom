using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : AbstractAnimatedCanvas
{
    private void OnValidate()
    {
        canvas = GetComponent<Canvas>();
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
            InputSystem.Instance.OnTouch += InputSystem_Touch;
        }   
        else
        {
            InputSystem.Instance.OnTouch -= InputSystem_Touch;
        }
    }

    private void InputSystem_Touch()
    {
        Hide();
    }

 





}
