using UnityEngine;
using UnityEngine.UI;
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

        Screen.SetResolution(1080, 1920, true);

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
            if (canvasScaler != null)
            {
                float dpi = Screen.dpi;
                float scaleFactor = dpi > 0 ? dpi / 160f : 1f;
                canvasScaler.scaleFactor = scaleFactor;
            }
        }
    }

    public void GeToGameScene() => SceneManager.LoadScene(1);

    public void GoToMainMenuScene() => SceneManager.LoadScene(0);

    public void RestartGameScene() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
