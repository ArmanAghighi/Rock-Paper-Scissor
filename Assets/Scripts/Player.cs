using UnityEngine;

public class Player : MonoBehaviour
{
    private HeroScriptalbeObjects hero1;
    private HeroScriptalbeObjects hero2;
    private HeroScriptalbeObjects hero3;
    private HeroScriptalbeObjects selectedHero;

    public HeroScriptalbeObjects GetSelectedHero() => selectedHero;

    public void SetSelectedHero (HeroScriptalbeObjects hero) => selectedHero = hero;

    public static Player player { get; private set; }

    private void OnEnable() => Refree.Instance.OnGameStart += SetHero;
    
    private void Awake()
    {
        if (player == null)
            player = this;
        else
            Destroy(gameObject);

        hero1 = GameManager.Instance.GetLastTeam()[0];
        hero2 = GameManager.Instance.GetLastTeam()[1];
        hero3 = GameManager.Instance.GetLastTeam()[2];
    }
    private void OnDisable()
    {
        Refree.Instance.OnGameStart -= SetHero;
    }

    public void SetHero()
    {
        if(selectedHero == null)
            selectedHero = GameManager.Instance.GetLastTeam()[Random.Range(0, GameManager.Instance.GetLastTeam().Count)];
    }

    public void OnHero1Click() => selectedHero = hero1;
    
    public void OnHero2Click() => selectedHero = hero2;
    
    public void OnHero3Click() => selectedHero = hero3;

}
