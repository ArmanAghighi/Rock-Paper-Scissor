using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Animation1Handler : ChainOfResponsibility
{
    private Animator animator;
    private int roundIndex = -1;

    [SerializeField] private GameObject roundCounterGameObject;
    [SerializeField] private string triggerString;
    [SerializeField] private Sprite[] roundImages;

    private void Awake() => animator = roundCounterGameObject.GetComponent<Animator>();

    public void ChangeSpriteToNextRound()
    {
        if (roundIndex < roundImages.Length - 1 && !Refree.isDraw)
        {
            roundIndex++;
            roundCounterGameObject.GetComponent<Image>().sprite = roundImages[roundIndex];
            Refree.isDraw = false;
        }
    }

    protected override IEnumerator Process()
    {
        Refree.Instance.AllowPlayerToSelect();
        animator.SetTrigger(triggerString);
        yield return new WaitForSeconds(0.5f);
        ChangeSpriteToNextRound();
        yield return new WaitForSeconds(1.5f);
        Continue();
    }
}

