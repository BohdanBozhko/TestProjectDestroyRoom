using UnityEngine;


[RequireComponent(typeof(Canvas))]
public class AbstractAnimatedCanvas : MonoBehaviour
{
    [SerializeField] public Canvas canvas;
    [SerializeField] public Animator animator;
    private int Appear;

    private void Awake()
    {
        Appear = Animator.StringToHash("Appear");
    }

    public virtual void Show()
    {
        animator.SetBool(Appear, true);
        canvas.enabled = true;
    }

    public virtual void Hide()
    {
        animator.SetBool(Appear, false);
        canvas.enabled = false;
    }
}
