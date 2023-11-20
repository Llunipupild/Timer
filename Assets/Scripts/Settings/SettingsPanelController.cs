using System;
using ColorPicker;
using ColorPicker.Enum;
using ColorPicker.UiController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsPanelController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _colorPalette;
        [SerializeField] 
        private TextMeshProUGUI _testText;
        [SerializeField] 
        private ColorPickerButtonController _topLeftPickColorButton;
        [SerializeField] 
        private ColorPickerButtonController _bottomLeftPickColorButton;
        [SerializeField] 
        private ColorPickerButtonController _topRightPickColorButton;
        [SerializeField] 
        private ColorPickerButtonController _bottomRightPickColorButton;
        [SerializeField] 
        private ColorPickerController _colorPickerController;
        [SerializeField] 
        private Toggle _animationToggle;

        private Button _button;
        private PickColorButtonType _currentPickColorButtonType;

        public delegate void OnChangeColor(VertexGradient vertexGradient);
        public delegate void OnAnimationEnabled(bool status);
    
        public event OnChangeColor onChangeColorText;
        public event OnAnimationEnabled onAnimationEnabled;
    
        private void Start()
        {
            _animationToggle.onValueChanged.AddListener(OnToggleValueChanged);
            _topLeftPickColorButton.onButtonClick += OnPickColorButtonClick;
            _bottomLeftPickColorButton.onButtonClick += OnPickColorButtonClick;
            _topRightPickColorButton.onButtonClick += OnPickColorButtonClick;
            _bottomRightPickColorButton.onButtonClick += OnPickColorButtonClick;
            _colorPickerController.onSelectColor += OnOnSelectColor;
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(OnSelfClick);
            _colorPalette.gameObject.SetActive(false);

            _topLeftPickColorButton.ChangeButtonColor(_testText.colorGradient.topLeft);
            _bottomLeftPickColorButton.ChangeButtonColor(_testText.colorGradient.bottomLeft);
            _topRightPickColorButton.ChangeButtonColor(_testText.colorGradient.topRight);
            _bottomRightPickColorButton.ChangeButtonColor(_testText.colorGradient.bottomRight);
        }
    
        private void OnDestroy()
        {
            _topLeftPickColorButton.onButtonClick -= OnPickColorButtonClick;
            _bottomLeftPickColorButton.onButtonClick -= OnPickColorButtonClick;
            _topRightPickColorButton.onButtonClick -= OnPickColorButtonClick;
            _bottomRightPickColorButton.onButtonClick -= OnPickColorButtonClick;
            _colorPickerController.onSelectColor -= OnOnSelectColor;
            _button.onClick.RemoveListener(OnSelfClick);
        }

        private void OnDisable()
        {
            _colorPalette.gameObject.SetActive(false);
        }

        public void SetUserData(bool animationEnabled)
        {
            _animationToggle.isOn = animationEnabled;
        }
    
        private void OnSelfClick()
        {
            _colorPalette.gameObject.SetActive(false);
        }

        private void OnPickColorButtonClick(PickColorButtonType pickColorButtonType)
        {
            _colorPalette.gameObject.SetActive(true);
            _currentPickColorButtonType = pickColorButtonType;
        }

        private void OnOnSelectColor(Color color)
        {
            VertexGradient initialColorGradient = _testText.colorGradient;
            switch (_currentPickColorButtonType)
            {
                case PickColorButtonType.TOP_LEFT:
                    _testText.colorGradient = new VertexGradient(color, initialColorGradient.topRight, initialColorGradient.bottomLeft, initialColorGradient.bottomRight);  
                    _topLeftPickColorButton.ChangeButtonColor(_testText.colorGradient.topLeft);
                    break;
                case PickColorButtonType.BOTTOM_LEFT:
                    _testText.colorGradient = new VertexGradient(initialColorGradient.topLeft, initialColorGradient.topRight, color, initialColorGradient.bottomRight);
                    _bottomLeftPickColorButton.ChangeButtonColor(_testText.colorGradient.bottomLeft);
                    break;
                case PickColorButtonType.TOP_RIGHT:
                    _testText.colorGradient = new VertexGradient(initialColorGradient.topLeft, color, initialColorGradient.bottomLeft, initialColorGradient.bottomRight);
                    _topRightPickColorButton.ChangeButtonColor(_testText.colorGradient.topRight);
                    break;
                case PickColorButtonType.BOTTOM_RIGHT:
                    _testText.colorGradient = new VertexGradient(initialColorGradient.topLeft, initialColorGradient.topRight, initialColorGradient.bottomLeft, color);
                    _bottomRightPickColorButton.ChangeButtonColor(_testText.colorGradient.bottomRight);
                    break;
            }
        
            onChangeColorText?.Invoke(_testText.colorGradient);
        }

        private void OnToggleValueChanged(bool argument)
        {
            onAnimationEnabled?.Invoke(argument);
        }
    }
}