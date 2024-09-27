using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoreGameUI : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private GameObject _counterImage;
    [SerializeField] private Text _playerScoreCounterText;
    [SerializeField] private Text _opponentScoreCounterText;

    void Start()
    {
        GameManger.Instance.OnStartGameEvent += StartGameUI;
        GameManger.Instance.OnPlayerScoreAdd += AddPlayerScore;
        GameManger.Instance.OnPlayerScoreAdd += RemovePlayerScore;
        GameManger.Instance.OnOpponentScoreAdd += AddOpponentScore;
        GameManger.Instance.OnOpponentScoreRemove += RemoveOpponentScore;
    }

    public void StartGameUI()
    {
        _startGameButton.gameObject.SetActive(false);    
        _counterImage.SetActive(true);
        StartCoroutine(WaitForAnimationFinish());
    }

    public void AddPlayerScore()
    {
        int score = int.Parse(_playerScoreCounterText.text);
        score++;
        _playerScoreCounterText.text = score.ToString();
    }

    public void RemovePlayerScore()
    {
        int score = int.Parse(_playerScoreCounterText.text);
        score--;
        _playerScoreCounterText.text = score.ToString();
    }

    public void AddOpponentScore()
    {
        int score = int.Parse(_opponentScoreCounterText.text);
        score++;
        _opponentScoreCounterText.text = score.ToString();
    }

    public void RemoveOpponentScore()
    {
        int score = int.Parse(_opponentScoreCounterText.text);
        score--;
        _opponentScoreCounterText.text = score.ToString();
    }

    IEnumerator WaitForAnimationFinish()
    {
        yield return new WaitForSeconds(3);
        _counterImage.SetActive(false);
    }
}
