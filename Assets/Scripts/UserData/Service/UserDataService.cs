using Newtonsoft.Json;
using UnityEngine;
using UserData.Model;

namespace UserData.Service
{
    public class UserDataService
    {
        private const string KEY = "userData";
        
        public void SetUserData(UserDataModel model)
        {
            PlayerPrefs.SetString(KEY, JsonConvert.SerializeObject(model));
            PlayerPrefs.Save();
        }
        
        public UserDataModel GetUserData()
        {
            string result = PlayerPrefs.GetString(KEY);
            return string.IsNullOrEmpty(result) ? null : JsonConvert.DeserializeObject<UserDataModel>(result);
        }
        
        public void DeleteUserData()
        {
            PlayerPrefs.DeleteKey(KEY);
        }
    }
}