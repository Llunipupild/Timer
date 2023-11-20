using ColorPicker.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace ColorPicker.UiController
{
    public class ColorPickerButtonController : MonoBehaviour
    {
        [SerializeField]
        private PickColorButtonType ColorPickerButtonType;
    
        private Button _button;
        private Image _image;

        public delegate void OnButtonClick(PickColorButtonType pickColorButtonType);
        public event OnButtonClick onButtonClick;
    
        private void Start()
        {
            _button = gameObject.GetComponent<Button>();
            _image = gameObject.GetComponent<Image>();
            _button.onClick.AddListener(OnColorPickerButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnColorPickerButtonClick);
        }

        public void ChangeButtonColor(Color color)
        {
            _image.color = color;
        }

        private void OnColorPickerButtonClick()
        {
            onButtonClick?.Invoke(ColorPickerButtonType);
        }
    }
}