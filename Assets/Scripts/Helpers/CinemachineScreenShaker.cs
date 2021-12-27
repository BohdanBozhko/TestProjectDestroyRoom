using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CinemachineScreenShaker : MonoSingleton<CinemachineScreenShaker>
{
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _perlinCamera;
    private float screenShakeTimer;

    private void OnValidate()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _perlinCamera = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _perlinCamera.m_AmplitudeGain = 0;
    }

    public void ScreenShake(float intensity, float time)
    {
        _perlinCamera.m_AmplitudeGain = intensity;
        screenShakeTimer = time;
    }

    private void Update()
    {
        if (screenShakeTimer > 0)
        {
            screenShakeTimer -= Time.deltaTime;
            if (screenShakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin _perlinCamera = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                _perlinCamera.m_AmplitudeGain = 0f;
            }
        }
    }
}
