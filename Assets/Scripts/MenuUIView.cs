using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuUIView : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private List<Button> heroSlotButtons; 
    [SerializeField] private List<Image> heroImageSlots;
    [SerializeField] private List<TextMeshProUGUI> heroTextSlots;
    [SerializeField] private Button nextSceneButton;
    private MenuUIController menuUIController;

    private void Start()
    {
        menuUIController = FindObjectOfType<MenuUIController>();

        for (int i = 0; i < heroSlotButtons.Count; i++)
        {
            int index = i; 
            heroSlotButtons[i].onClick.AddListener(() => menuUIController.AssignHeroToSlot(index));
        }
    }

    public void UpdateTeamUI(List<HeroScriptalbeObjects> selectedHeroes)
    {
        for (int i = 0; i < heroImageSlots.Count; i++)
        {
            if (selectedHeroes[i] != null)
            {
                heroImageSlots[i].sprite = selectedHeroes[i].hero_Sprite;
                heroTextSlots[i].text = selectedHeroes[i].hero_Name;
            }
            else
            {
                heroImageSlots[i].sprite = null;
                heroTextSlots[i].text = "Hero " + (i + 1);
            }
        }
    }

    public void ShowSelectedHero(HeroScriptalbeObjects selectedHero)
    {
        characterImage.sprite = selectedHero.hero_Sprite;
        characterNameText.text = selectedHero.hero_Name;
    }

    public void ReadyToStart() => nextSceneButton.interactable = true;

    public void ReadyToSelectTeam() => nextSceneButton.interactable = false;
}
