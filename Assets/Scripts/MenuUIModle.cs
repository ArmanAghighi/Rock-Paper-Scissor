using System;
using System.Collections.Generic;

public class MenuUIModle
{
    public event Action<List<HeroScriptalbeObjects>> OnTeamUpdated;
    public event Action OnReadyToStart;
    public event Action OnReadyToSelectTeam;
    public event Action<HeroScriptalbeObjects> OnSelectHeroToJoin;

    private List<HeroScriptalbeObjects> selectedHeroes = new List<HeroScriptalbeObjects>(new HeroScriptalbeObjects[3]);
    private HeroScriptalbeObjects pendingHero = null;

    public void SelectHero(HeroScriptalbeObjects hero)
    {
        if (hero.hero_Catagory != CharacterCategory.Unknown)
        {
            pendingHero = hero;
            OnSelectHeroToJoin?.Invoke(hero);
        }
    }

    public void AssignHeroToSlot(int slotIndex)
    {
        if (pendingHero == null) return;

        if (selectedHeroes.Contains(pendingHero))
        {
            int oldIndex = selectedHeroes.IndexOf(pendingHero);
            selectedHeroes[oldIndex] = null;
        }

        selectedHeroes[slotIndex] = pendingHero;
        pendingHero = null;


        if (selectedHeroes.FindAll(h => h != null).Count >= 3)
            OnReadyToStart?.Invoke();
        else
            OnReadyToSelectTeam?.Invoke();

        OnTeamUpdated?.Invoke(selectedHeroes);
    }

    public void AssignLastVersionTeam() =>  GameManager.Instance.SetLastTeam(selectedHeroes);
}
