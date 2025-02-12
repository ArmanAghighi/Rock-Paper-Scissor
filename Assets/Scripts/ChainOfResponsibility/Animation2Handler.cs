using UnityEngine;
using System.Collections;

public class Animation2Handler : ChainOfResponsibility
{
    private Animator animator;

    [SerializeField] private GameObject startCounter;
    [SerializeField] private string triggerString;

    private void Awake() => animator = startCounter.GetComponent<Animator>();

    protected override IEnumerator Process()
    {
        animator.SetTrigger(triggerString);
        yield return new WaitForSeconds(3f);
        Continue();
    }

}
