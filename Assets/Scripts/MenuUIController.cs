using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    private MenuUIModle menuUIModel;
    [SerializeField] private MenuUIView menuUIView;

    private void OnEnable()
    {
        menuUIModel = new MenuUIModle();

        menuUIModel.OnTeamUpdated += menuUIView.UpdateTeamUI;
        menuUIModel.OnReadyToStart += menuUIView.ReadyToStart;
        menuUIModel.OnReadyToSelectTeam += menuUIView.ReadyToSelectTeam;
        menuUIModel.OnSelectHeroToJoin += menuUIView.ShowSelectedHero;
    }

    public void SelectHero(HeroScriptalbeObjects hero) => menuUIModel.SelectHero(hero);
    
    public void AssignHeroToSlot(int slotIndex) => menuUIModel.AssignHeroToSlot(slotIndex);

    public void AssignLastVersion() => menuUIModel.AssignLastVersionTeam();
}
