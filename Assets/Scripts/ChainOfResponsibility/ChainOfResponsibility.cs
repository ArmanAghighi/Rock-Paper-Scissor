using UnityEngine;
using System.Collections;

public abstract class ChainOfResponsibility : MonoBehaviour
{
    protected ChainOfResponsibility nextStep;

    protected abstract IEnumerator Process();

    public void SetNext(ChainOfResponsibility next) => nextStep = next;
    
    public virtual void Execute() => StartCoroutine(Process());

    protected void Continue()
    {
        if (Refree.Instance.gameEnd) 
            return;

        if (nextStep != null)        
            nextStep.Execute();
    }
}