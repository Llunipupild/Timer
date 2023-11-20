using System.Collections.Generic;
using ColorPicker.Enum;
using Timer.ElapsedTimeStructure;
using TMPro;
using UnityEngine;
using UserData.SavedTimerColors;

namespace UserData.Model
{
    public class UserDataModel
    {
        public bool AnimationEnabled { get; }
        public ElapsedTime ElapsedTime { get; }
        public List<SavedColorModel> SavedColorModels { get; }

        public UserDataModel(bool animationEnabled, ElapsedTime elapsedTime)
        {
            AnimationEnabled = animationEnabled;
            ElapsedTime = elapsedTime;
            SavedColorModels = new List<SavedColorModel>();
        }

        public VertexGradient GetSavedColors()
        {
            SavedColorModel topLeftColorModel = SavedColorModels.Find(scm => scm.ColorType == PickColorButtonType.TOP_LEFT);
            SavedColorModel topRightColorModel = SavedColorModels.Find(scm => scm.ColorType == PickColorButtonType.TOP_RIGHT);
            SavedColorModel bottomLeftColorModel = SavedColorModels.Find(scm => scm.ColorType == PickColorButtonType.BOTTOM_LEFT);
            SavedColorModel bottomRightColorModel = SavedColorModels.Find(scm => scm.ColorType == PickColorButtonType.BOTTOM_RIGHT);

            Color topLeftColor = new Color(topLeftColorModel.R, topLeftColorModel.G, topLeftColorModel.B, 1);
            Color topRightColor = new Color(topRightColorModel.R, topRightColorModel.G, topRightColorModel.B, 1);
            Color bottomLeftColor = new Color(bottomLeftColorModel.R, bottomLeftColorModel.G, bottomLeftColorModel.B, 1);
            Color bottomRightColor = new Color(bottomRightColorModel.R, bottomRightColorModel.G, bottomRightColorModel.B, 1);

            return new VertexGradient(topLeftColor, topRightColor, bottomLeftColor, bottomRightColor);
        }
        
        public void SaveColors(VertexGradient colors)
        {
            SavedColorModel topLeftColor = new SavedColorModel(colors.topLeft.r, colors.topLeft.g, colors.topLeft.b,
                PickColorButtonType.TOP_LEFT);
            SavedColorModel topRightColor = new SavedColorModel(colors.topRight.r, colors.topRight.g, colors.topRight.b,
                PickColorButtonType.TOP_RIGHT);
            SavedColorModel bottomLeftColor = new SavedColorModel(colors.bottomLeft.r, colors.bottomLeft.g, colors.bottomLeft.b,
                PickColorButtonType.BOTTOM_LEFT);
            SavedColorModel bottomRightColor = new SavedColorModel(colors.bottomRight.r, colors.bottomRight.g, colors.bottomRight.b,
                PickColorButtonType.BOTTOM_RIGHT);
            
            SavedColorModels.Add(topLeftColor);
            SavedColorModels.Add(topRightColor);
            SavedColorModels.Add(bottomLeftColor);
            SavedColorModels.Add(bottomRightColor);
        }
    }
}