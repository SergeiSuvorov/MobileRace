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
            CurrentCar = new Car(speedCar);
            Analytic = analytic;
            IntConventer intConventor = new IntConventer();
            CreditCount = new PlayerPrefsSubscriptionProperty<int>(CreditKey, intConventor);
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

