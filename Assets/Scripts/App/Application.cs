using MainMenu;
using UnityEngine;
using UserData.Model;
using UserData.Service;

namespace App
{
    public class Application : MonoBehaviour
    {
        [SerializeField] 
        private MainMenuController _mainMenuController;
        
        private UserDataService _userDataService;

        private void Start()
        {
            _userDataService = new UserDataService();
            UserDataModel userDataModel = _userDataService.GetUserData();
            if (userDataModel == null) {
                return;
            }
            
            _mainMenuController.SetUserData(userDataModel.AnimationEnabled, userDataModel.ElapsedTime, userDataModel.GetSavedColors());
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus) {
                return;
            }

            SaveUserData();
        }

        private void OnApplicationQuit()
        {
            SaveUserData();
        }

        private void SaveUserData()
        {
            UserDataModel userDataModel = new UserDataModel(_mainMenuController.GetAnimationStatus(), _mainMenuController.GetTimerElapsedTime());
            userDataModel.SaveColors(_mainMenuController.GetTimerTextColors());
            _userDataService.SetUserData(userDataModel);
        }
    }
}