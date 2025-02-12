using TMPro; 
using UnityEngine; 
using System.Collections;



public class ResultHandler : ChainOfResponsibility
{
    [SerializeField] private TextMeshProUGUI playerPoint;
    [SerializeField] private TextMeshProUGUI opponentPoint;

    private void OnEnable() => Refree.Instance.OnScoreUpdate += UpdateScoreUI;

    private void OnDisable() => Refree.Instance.OnScoreUpdate -= UpdateScoreUI;

    protected override IEnumerator Process()
    {
        Refree.Instance.DetermineWinner(Player.player.GetSelectedHero(), Opponent.opponent.GetOpponentHero());
        yield return new WaitForSeconds(3f);
        Continue();
    }

    public void UpdateScoreUI(int playerScore , int opponentScore)
    {
        playerPoint.text = playerScore.ToString();
        opponentPoint.text = opponentScore.ToString();
    }

}