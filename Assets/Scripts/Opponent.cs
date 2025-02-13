using UnityEngine;
using System.Collections.Generic;

public class Opponent : MonoBehaviour
{
    public static Opponent opponent { get; private set;}

    [SerializeField] private List<HeroScriptalbeObjects> opponentHeroTeam;
    
    private HeroScriptalbeObjects opponentHero;

    public HeroScriptalbeObjects GetOpponentHero() => opponentHero;

    private void Awake()
    {
        if (opponent == null)
            opponent = this;
        else
            Destroy(gameObject);
    }

    private void Start() => Refree.Instance.OnGameStart += SelectOpponentHero;

    private void SelectOpponentHero()
    {
        int randomIndex = Random.Range(0, opponentHeroTeam.Count);
        opponentHero = opponentHeroTeam[randomIndex];
    }
}
