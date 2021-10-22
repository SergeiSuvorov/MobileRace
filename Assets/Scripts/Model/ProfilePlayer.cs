using Model.Analytic;
using Tools;
using UnityEngine.Purchasing;

namespace Model
{
    public class ProfilePlayer
    {
        public ProfilePlayer(float speedCar, IAnalyticTools analytic)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CreditCount = new SubscriptionProperty<int>();
            CurrentCar = new Car(speedCar);
            Analytic = analytic;
        }

        public SubscriptionProperty<GameState> CurrentState { get; }

        public SubscriptionProperty<int> CreditCount { get; }

        public Car CurrentCar { get; }

        public IAnalyticTools Analytic { get; }
        public IStoreController storeController { get; }
    }
}

