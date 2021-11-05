using UnityEngine;

namespace Tools
{
    public class PlayerPrefsSubscriptionProperty<T>: SubscriptionProperty<T>, IReadOnlySubscriptionProperty<T> 
    {

        private string _ppKey;

        public PlayerPrefsSubscriptionProperty(string ppKey, IConventor<T> conventor)
        {
            _ppKey = ppKey;
            Debug.Log(PlayerPrefs.GetString(_ppKey));
            Value = conventor.Parse(PlayerPrefs.GetString(_ppKey,"0"));
            SubscribeOnChange(UpdateValue);
        }

        private void UpdateValue(T value)
        {
            PlayerPrefs.SetString(_ppKey, value.ToString());
        }
    }

}

