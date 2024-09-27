using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuButtonLogic : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _helpButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _helpPanel;

    [HideInInspector] public static MainMenuButtonLogic Instance { get; set; }
    [HideInInspector] public event Action<GameObject , bool> OnPanelActivator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("CoreScene");
    }

    public void Help()
    {
        OnPanelActivator?.Invoke(_helpPanel, true);
    }

    public void Option()
    {
        OnPanelActivator?.Invoke(_optionPanel, true);
    }

    public void ExitPanel()
    {
        OnPanelActivator?.Invoke(_optionPanel, false);
        OnPanelActivator?.Invoke(_helpPanel, false);
    }
}
