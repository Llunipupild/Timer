using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timerText = null!;
    [SerializeField]
    private Button _startTimerButton = null!;
    [SerializeField]
    private Button _stopTimerButton = null!;
    [SerializeField]
    private Button _resetTimerButton = null!;
    [SerializeField]
    private Button _screenButton = null!;

    private int _elapsedSeconds;
    private int _elapsedMinutes;
    private int _elapsedHours;
    private int _elapsedDays;

    private bool _stopped;
    
    private void Start()
    {
        _startTimerButton.onClick.AddListener(OnStartTimerButtonClick);
        _stopTimerButton.onClick.AddListener(OnStopTimerButtonClick);
        _resetTimerButton.onClick.AddListener(OnResetTimerButtonClick);
        _screenButton.onClick.AddListener(OnScreenClick);
    }

    private void OnStartTimerButtonClick()
    {
        if (IsStarted() && !_stopped) {
            return;
        }

        _stopped = false;
        StartCoroutine(DrawTextTimerCoroutine());
    }

    private void OnStopTimerButtonClick()
    {
        if (!IsStarted()) {
            return;
        }

        _stopped = true;
        StopAllCoroutines();
    }

    private void OnResetTimerButtonClick()
    {
        OnStopTimerButtonClick();
        _elapsedSeconds = 0;
        _elapsedMinutes = 0;
        _elapsedHours = 0;
        _elapsedDays = 0;
        _timerText.text = "0:00";
    }

    private void OnScreenClick()
    {
        _startTimerButton.gameObject.SetActive(!_startTimerButton.gameObject.activeSelf);
        _stopTimerButton.gameObject.SetActive(!_stopTimerButton.gameObject.activeSelf);
        _resetTimerButton.gameObject.SetActive(!_resetTimerButton.gameObject.activeSelf);
    }
    
    private IEnumerator DrawTextTimerCoroutine()
    {
        while (true) {
            IncreaseTimerValue();
            _timerText.text = GetElapsedTimeText();
            yield return new WaitForSeconds(1);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void IncreaseTimerValue()
    {
        _elapsedSeconds++;

        if (_elapsedSeconds < 60) {
            return;
        }
        
        _elapsedSeconds = 0;
        _elapsedMinutes++;

        if (_elapsedMinutes < 60) {
            return;
        }
        
        _elapsedMinutes = 0;
        _elapsedHours++;

        if (_elapsedHours < 24) {
            return;
        }

        _elapsedHours = 0;
        _elapsedDays++;
    }

    private string GetElapsedTimeText()
    {
        string seconds = _elapsedSeconds.ToString();
        if (_elapsedSeconds <= 9) {
            seconds = $"0{_elapsedSeconds}";
        }
        
        if (_elapsedDays > 0) {
            return $"{_elapsedDays}:{_elapsedHours}:{_elapsedMinutes}:{seconds}";
        }

        return _elapsedHours > 0 ? $"{_elapsedHours}:{_elapsedMinutes}:{seconds}" : $"{_elapsedMinutes}:{seconds}";
    }

    private bool IsStarted()
    {
        return _elapsedSeconds != 0 || _elapsedMinutes != 0 || _elapsedHours != 0 || _elapsedDays != 0;
    }
}
