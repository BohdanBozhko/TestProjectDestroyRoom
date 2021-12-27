using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVfx : MonoBehaviour
{
    [SerializeField] private ParticleSystem jumpVfx;
    [SerializeField] private ParticleSystem landVfx;
    [SerializeField] private ParticleSystem hitWaveVfx;

    private void Start()
    {
        landVfx.transform.SetParent(null);
    }

    public void PlayerMovement_OnJumpStatusChanged(bool jump)
    {
        jumpVfx.Play();
    }

    public void PlayerMovement_PowerfulJump()
    {
        landVfx.Play();
        landVfx.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        hitWaveVfx.Play();
    }
}
