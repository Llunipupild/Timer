using ColorPicker.Enum;

namespace UserData.SavedTimerColors
{
    public class SavedColorModel
    {
        public float R { get; }
        public float G { get; }
        public float B { get; }
        public PickColorButtonType ColorType { get; }
        
        public SavedColorModel(float r, float g, float b, PickColorButtonType colorType)
        {
            R = r;
            G = g;
            B = b;
            ColorType = colorType;
        }
    }
}