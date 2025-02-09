using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    private List<HeroScriptalbeObjects> lastTeam;

    public List<HeroScriptalbeObjects> GetLastTeam () => lastTeam;
    public void SetLastTeam(List<HeroScriptalbeObjects> LT) => lastTeam = LT;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(Instance);
        }
    }
}
