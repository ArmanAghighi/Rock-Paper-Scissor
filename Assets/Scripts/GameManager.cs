using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<HeroScriptalbeObjects> lastTeam;

    public List<HeroScriptalbeObjects> GetLastTeam() => lastTeam;

    public void SetLastTeam(List<HeroScriptalbeObjects> LastTeam) => lastTeam = LastTeam;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GeToGameScene() => SceneManager.LoadScene(1);

    public void GoToMainMenuScene() => SceneManager.LoadScene(0);

    public void RestartGameScene() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
