using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ColorPicker
{
    public class ColorPickerController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] 
        private Image _colorsImage;
    
        public delegate void OnClickColorsPalette(Color color);
        public event OnClickColorsPalette onSelectColor;

        public void OnPointerClick(PointerEventData eventData)
        {
            onSelectColor?.Invoke(GetSelectedColor(eventData.position));
        }
    
        private Color GetSelectedColor(Vector2 clickedPosition)
        {
            Vector2 sizeDelta = _colorsImage.rectTransform.sizeDelta;
            Texture2D texture2D = _colorsImage.sprite.texture;
        
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_colorsImage.rectTransform, clickedPosition, Camera.main, out Vector2 localPosition);
            localPosition += _colorsImage.rectTransform.sizeDelta / 2;
            int x = (int)(texture2D.width * localPosition.x / sizeDelta.x);
            int y = (int)(texture2D.height * localPosition.y / sizeDelta.y);

            return texture2D.GetPixel(x, y);
        }
    }
}