using System;
using UnityEngine;

public class Refree : MonoBehaviour
{
    public static Refree Instance { get; private set; }

    [SerializeField] private GameObject refreeUI;

    private int playerScore = 0;
    private int opponentScore = 0;

    public bool gameEnd = false;
    public static bool isDraw = false;

    public event Action<int, int> OnScoreUpdate;
    public event Action<bool> OnSetPLayerAbilityToChooseHero;
    public event Action<bool> OnShowResult;
    public event Action OnGameStart;
    public event Action OnGameStop;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ChainOfResponsibility animation1 = refreeUI.GetComponent<Animation1Handler>();
        ChainOfResponsibility animation2 = refreeUI.GetComponent<Animation2Handler>();
        ChainOfResponsibility decision = refreeUI.GetComponent<DecisionHandler>();
        ChainOfResponsibility result = refreeUI.GetComponent<ResultHandler>();

        animation1.SetNext(animation2);
        animation2.SetNext(decision);
        decision.SetNext(result);
        result.SetNext(animation1);

        animation1.Execute();
    }

    private void Update()
    {
        if (gameEnd)
        {
            if (playerScore > opponentScore)
                OnShowResult?.Invoke(true);
            else
                OnShowResult?.Invoke(false);
        }
    }

    public void TriggerGameStart()
    {
        OnGameStart.Invoke();
        isDraw = false;
    }

    public void AllowPlayerToSelect() => OnSetPLayerAbilityToChooseHero?.Invoke(true);

    public void DisallowPlayerToSelect() => OnSetPLayerAbilityToChooseHero?.Invoke(false);


    public void DetermineWinner(HeroScriptalbeObjects playerChoice, HeroScriptalbeObjects opponentChoice)
    {
        bool drawRound = playerChoice.hero_Catagory == opponentChoice.hero_Catagory;
        bool playerWin = playerChoice.CanDefeatEnemyList.Contains(opponentChoice.hero_Catagory);

        UpdateScore(playerWin, drawRound);
    }

    public void UpdateScore(bool playerWon, bool draw)
    {
        if (draw)
        {
            isDraw = true;
        }
        else if (playerWon)
        {
            playerScore++;
        }
        else
        {
            opponentScore++;
        }

        OnScoreUpdate?.Invoke(playerScore, opponentScore);

        if (playerScore >= 3 || opponentScore >= 3)
        {
            gameEnd = true;
            return;
        }
    }
}