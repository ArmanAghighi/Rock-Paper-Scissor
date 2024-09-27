using System;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public event Action OnStartGameEvent;
    public event Action OnPlayerScoreAdd;
    public event Action OnPlayerScoreRemove;
    public event Action OnOpponentScoreAdd;
    public event Action OnOpponentScoreRemove;

    public static GameManger Instance { get; private set; }

    private int _playerScore = 0;
    private int _opponentScore = 0;

    private int _playerChoice = -1;
    private int _opponentChoice = -1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        //win lose situation
    }

    public void StartGame()
    {
        OnStartGameEvent?.Invoke();
    }

    public void AddPlayerScore()
    {
        _playerScore++;
        OnPlayerScoreAdd?.Invoke();
    }

    public void RemovePlayerScore()
    {
        _playerScore--;
        OnPlayerScoreRemove?.Invoke();
    }

    public void AddOpponentScore()
    {
        _opponentScore++;
        OnOpponentScoreAdd?.Invoke();
    }

    public void RemoveOpponentScore()
    {
        _opponentScore--;
        OnOpponentScoreRemove?.Invoke();
    }
}
