using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CoreGameUI : MonoBehaviour
{
    [SerializeField] private List<Button> playerTeamButton;
    [SerializeField] private GameObject inGameParentObject;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Start()
    {
        StartGameScene();
        for (int i = 0; i < GameManager.Instance.GetLastTeam().Count; i++)
        {
            playerTeamButton[i].GetComponent<Image>().sprite = GameManager.Instance.GetLastTeam()[i].hero_Sprite;
        }
        Refree.Instance.OnShowResult += ShowResult;
        Refree.Instance.OnSetPLayerAbilityToChooseHero += EnableButtons;
    }

    public void EnableButtons(bool situation)
    {
        foreach (var button in playerTeamButton)
        {
            button.interactable =situation;
        }
    }

    public void ShowResult(bool playerWinTheMatch)
    {
        inGameParentObject.gameObject.SetActive(false);

        if (playerWinTheMatch)
            winPanel.gameObject.SetActive(true);
        else
            losePanel.gameObject.SetActive(true);
    }

    public void StartGameScene()
    {
        inGameParentObject.gameObject.SetActive(true);
        winPanel.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);
    }
}
