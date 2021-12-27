using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("Object Components")]
    [SerializeField] private ParticleSystem interactVfx;
    [SerializeField] private Collider objCollider;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color hitColor;
    [Header("Timing")]
    [SerializeField] private float delayToCloseInteract = 0.5f;
    [SerializeField] private float delayToDestroy = 0.2f;
    [SerializeField] private float delayToDistanceInteract = 0.5f;

    Sequence interactionSequence;

    private bool canInteract = true;
    public bool CanInteract
    {
        get
        {
            return canInteract;
        }
        set
        {
            canInteract = value;
        }
    }

    private void Start()
    {
        LevelController.Instance.AddInteractableObject(this);
       interactionSequence = DOTween.Sequence();
    }

    public void CloseInteract()
    {
        canInteract = false;
        StartCoroutine(CloseInteractionRoutine());
    }

    public void DistanceInteract()
    {
        canInteract = false;
        StartCoroutine(DistanceInteractionRoutine());
    }

    private IEnumerator CloseInteractionRoutine()
    {
        Vector3 hitScale = new Vector3(1.1f, 1.1f, 1.1f);
        float interactionTime = 0.15f;

        yield return new WaitForSeconds(delayToCloseInteract);
        meshRenderer.material.DOColor(hitColor, interactionTime).SetLoops(2, LoopType.Yoyo);
        transform.DOScale(hitScale, interactionTime).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutBounce);

        yield return new WaitForSeconds(delayToDestroy);
        Taptic.Medium();
        DestroyObject();
    }

    private IEnumerator DistanceInteractionRoutine()
    {
        Vector3 jumpScale = new Vector3(1.3f, 1.3f, 1.3f);
        float jumpPower = 2f;
        float interactionTime = 0.2f;

        yield return new WaitForSeconds(delayToDistanceInteract);
        interactionSequence.Join(transform.DOJump(transform.position, jumpPower, 1, interactionTime));
        interactionSequence.Join(transform.DOScale(jumpScale, interactionTime).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutQuint));
        interactionSequence.Append(meshRenderer.material.DOColor(hitColor, 0.1f).SetLoops(2, LoopType.Yoyo));
        
        yield return new WaitForSeconds(delayToDestroy);
        Taptic.Light();
        DestroyObject();
    }


    private void DestroyObject()
    {
        objCollider.enabled = false;
        meshRenderer.enabled = false;
        interactVfx.Play();
        LevelController.Instance.RemoveInteractableObject(this);
    }
}
