using UnityEngine;
using UnityEngine.UI;

public class SounManager : MonoBehaviour
{
    [SerializeField] private Scrollbar _audioScrollbar;
    [SerializeField] private GameObject _audioButton;
    private AudioSource _audioSource;
    private bool _isPlaying = true;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _audioSource.volume = _audioScrollbar.value;
    }

    public void MuteAudio()
    {
        if (_isPlaying)
        {
            _audioButton.GetComponent<Image>().color = Color.red;
            _audioSource.mute = true;
            _isPlaying = false;
        }
        else
        {
            _audioButton.GetComponent<Image>().color = Color.white;
            _audioSource.mute = false; 
            _isPlaying = true;
        }
    }
}
