using Settings;
using Sound;
using Timer;
using Timer.ElapsedTimeStructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuController : MonoBehaviour
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
        [SerializeField]
        private Button _settingsButton = null!;
        [SerializeField] 
        private Button _soundButton = null!;
        [SerializeField] 
        private GameObject _settingsPanel;
        
        private Animation _timerTextAnimation;
        private TimerController _timerController;

        private bool _animationEnabled;
        
        private void Awake()
        {
            _startTimerButton.onClick.AddListener(OnStartTimerButtonClick);
            _stopTimerButton.onClick.AddListener(OnStopTimerButtonClick);
            _resetTimerButton.onClick.AddListener(OnResetTimerButtonClick);
            _screenButton.onClick.AddListener(OnScreenClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _soundButton.onClick.AddListener(OnSoundButtonClick);
            
            SettingsPanelController settingsPanelController = _settingsPanel.GetComponent<SettingsPanelController>();
            settingsPanelController.onChangeColorText += OnOnChangeTextColor;
            settingsPanelController.onAnimationEnabled += OnAnimationEnabled;
            
            _timerTextAnimation = _timerText.GetComponent<Animation>();
            _timerController = new TimerController(_timerText, null);
        }

        private void OnDestroy()
        {
            _startTimerButton.onClick.RemoveListener(OnStartTimerButtonClick);
            _stopTimerButton.onClick.RemoveListener(OnStopTimerButtonClick);
            _resetTimerButton.onClick.RemoveListener(OnResetTimerButtonClick);
            _screenButton.onClick.RemoveListener(OnScreenClick);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
            _soundButton.onClick.RemoveListener(OnSoundButtonClick);
            SettingsPanelController settingsPanelController = _settingsPanel.GetComponent<SettingsPanelController>();
            settingsPanelController.onChangeColorText -= OnOnChangeTextColor;
            settingsPanelController.onAnimationEnabled -= OnAnimationEnabled;
        }
        
        public void SetUserData(bool animationEnabled, ElapsedTime elapsedTime, VertexGradient colors)
        {
            _animationEnabled = animationEnabled;
            _timerText.colorGradient = colors;
            _timerController = new TimerController(_timerText, elapsedTime);
            _settingsPanel.GetComponent<SettingsPanelController>().SetUserData(animationEnabled);
        }
        
        public bool GetAnimationStatus()
        {
            return _animationEnabled;
        }

        public ElapsedTime GetTimerElapsedTime()
        {
            return _timerController.GetElapsedTime();
        }
        
        public VertexGradient GetTimerTextColors()
        {
            return _timerText.colorGradient;
        }

        private void OnStartTimerButtonClick()
        {
            if (_timerController.IsStarted) {
                return;
            }
            if (_animationEnabled) {
                _timerTextAnimation.enabled = true;
            }
            
            _timerController.Start();
        }

        private void OnStopTimerButtonClick()
        {
            if (!_timerController.IsStarted) {
                return;
            }
        
            _timerTextAnimation.enabled = false;
            ReturnStartTextScale();
            _timerController.Stop();
        }

        private void OnResetTimerButtonClick()
        {
            _timerController.Reset();
        }

        private void OnSettingsButtonClick()
        {
            OnStopTimerButtonClick();
            HideButtons();
            _settingsPanel.gameObject.SetActive(true);
        }
    
        private void OnScreenClick()
        {
            _startTimerButton.gameObject.SetActive(!_startTimerButton.gameObject.activeSelf);
            _stopTimerButton.gameObject.SetActive(!_stopTimerButton.gameObject.activeSelf);
            _resetTimerButton.gameObject.SetActive(!_resetTimerButton.gameObject.activeSelf);
            _settingsButton.gameObject.SetActive(!_settingsButton.gameObject.activeSelf);
            _soundButton.interactable = !_soundButton.interactable;
            _settingsPanel.gameObject.SetActive(false);
        }

        private void OnOnChangeTextColor(VertexGradient vertexGradient)
        {
            _timerText.colorGradient = vertexGradient;
        }

        private void OnAnimationEnabled(bool status)
        {
            _animationEnabled = status;
        }

        private void OnSoundButtonClick()
        {
            _soundButton.GetComponent<SoundComponent>().Play();
        }

        private void ReturnStartTextScale()
        {
            _timerText.transform.localScale = new Vector3(1, 1, 1);
        }
        
        private void HideButtons()
        {
            _resetTimerButton.gameObject.SetActive(false);
            _settingsButton.gameObject.SetActive(false);
            _stopTimerButton.gameObject.SetActive(false);
            _startTimerButton.gameObject.SetActive(false);
            _soundButton.interactable = false;
        }
    }
}