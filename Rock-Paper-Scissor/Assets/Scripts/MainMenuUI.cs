using UnityEngine;
public class MainMenuUI : MonoBehaviour
{
    void Start()
    {
        MainMenuButtonLogic.Instance.OnPanelActivator += PanelActivator;
    }
    
    private void PanelActivator(GameObject panel,bool condition)
    {
        panel.SetActive(condition);
    }

}
