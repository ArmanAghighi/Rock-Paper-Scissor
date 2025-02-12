using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DecisionHandler : ChainOfResponsibility
{
    private Animator animatorPlayer;
    private Animator animatorOpponent;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject opponent;
    [SerializeField] private string playerAnimatorString;
    [SerializeField] private string opponentAnimatorString;

    private void Awake()
    {
        animatorPlayer = player.GetComponent<Animator>();
        animatorOpponent = opponent.GetComponent<Animator>();
    }

    protected override IEnumerator Process()
    {
        Refree.Instance.DisallowPlayerToSelect();
        Refree.Instance.TriggerGameStart();
        player.GetComponent<Image>().sprite = Player.player.GetSelectedHero().hero_Sprite;
        opponent.GetComponent<Image>().sprite = Opponent.opponent.GetOpponentHero().hero_Sprite;

        animatorPlayer.SetTrigger(playerAnimatorString);
        animatorOpponent.SetTrigger(opponentAnimatorString);
        
        yield return new WaitForSeconds(3f);
        Continue();
    }
}