using Model.Analytic;
using Tools;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Model
{
    public class ProfilePlayer
    {
        public ProfilePlayer(float speedCar, IAnalyticTools analytic)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            //CreditCount = new SubscriptionProperty<int>();
            CurrentCar = new Car(speedCar);
            Analytic = analytic;
            //CreditCount.Value = PlayerPrefs.GetInt(CreditKey, 0);
            IntConventor intConventor = new IntConventor();
            CreditCount = new PlayerPrefsSubscriptionProperty<int>(CreditKey, intConventor);
            //CreditCount.SubscribeOnChange(onCreditCountChange);
        }

        private const string CreditKey = nameof(CreditKey);
        public SubscriptionProperty<GameState> CurrentState { get; }

        public SubscriptionProperty<int> CreditCount { get; }

        public Car CurrentCar { get; }

        public IAnalyticTools Analytic { get; }
        public IStoreController storeController { get; }

        private void onCreditCountChange(int value)
        {
            PlayerPrefs.SetInt(CreditKey, value);
        }
    }
}

