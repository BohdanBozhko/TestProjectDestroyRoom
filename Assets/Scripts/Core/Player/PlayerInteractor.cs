using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractor : MonoBehaviour
{
    public event Action OnCollisionHappend;

    [SerializeField] private float destroyingWaveRadius = 7;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.TryGetComponent(out IInteractable interactable))
        {
            if (interactable.CanInteract)
            {
                interactable.CloseInteract();
                OnCollisionHappend?.Invoke();
            }
        }
    }

    public void PlayerMovement_PowerfulJump()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, destroyingWaveRadius);
        foreach (var hitCollider in colliders)
        {
            if (hitCollider.TryGetComponent<IInteractable>(out IInteractable interactable))
            { 
                interactable.DistanceInteract();
            }
        }
    }
}

