using System.Threading;
using Cysharp.Threading.Tasks;
using Timer.ElapsedTimeStructure;
using TMPro;

namespace Timer
{
    public class TimerController
    {
        private int _elapsedSeconds;
        private int _elapsedMinutes;
        private int _elapsedHours;
        private int _elapsedDays;
        private bool _started;
        private readonly TextMeshProUGUI _timerText;
        CancellationTokenSource _cancelTokenSource; 
    
        public TimerController(TextMeshProUGUI timerText, ElapsedTime elapsedTime)
        {
            _timerText = timerText;
            if (elapsedTime == null) {
                return;
            }

            _elapsedSeconds = elapsedTime.ElapsedSeconds;
            _elapsedMinutes = elapsedTime.ElapsedMinutes;
            _elapsedHours = elapsedTime.ElapsedHours;
            _elapsedDays = elapsedTime.ElapsedDays;
            _timerText.text = GetElapsedTimeText();
        }

        public void Start()
        {
            _cancelTokenSource = new CancellationTokenSource();
            StartAsync().Forget();
            IsStarted = true;
        }
    
        private async UniTask StartAsync()
        {
            while (!_cancelTokenSource.IsCancellationRequested) {
                IncreaseTimerValue();
                _timerText.text = GetElapsedTimeText();
                await UniTask.WaitForSeconds(1, false, PlayerLoopTiming.Update, _cancelTokenSource.Token);
            }
        }

        public void Reset()
        {
            _elapsedSeconds = 0;
            _elapsedMinutes = 0;
            _elapsedHours = 0;
            _elapsedDays = 0;
            _timerText.text = "0:00";
        }

        public void Stop()
        {
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();
            IsStarted = false;
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

        public ElapsedTime GetElapsedTime()
        {
            return new ElapsedTime(_elapsedSeconds, _elapsedMinutes, _elapsedHours, _elapsedDays);
        }
    
        public bool IsStarted { get; private set; }
    }
}
