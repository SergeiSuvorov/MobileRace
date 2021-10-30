using Model;
using Model.Shop;
using Tools;
using Tools.Ads;
using UnityEngine;

namespace Ui
{
    public class MainMenuController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
        private readonly ProfilePlayer _profilePlayer;
        private readonly IAdsShower _adsShower;
        private readonly MainMenuView _view;
        private readonly IShop _shop;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAdsShower adsShower, IShop shop)
        {
            _profilePlayer = profilePlayer;
            _adsShower = adsShower;
            _view = LoadView(placeForUi);
            _shop = shop;

            TrailController trail = new TrailController();
            AddController(trail);
            _view.Init(StartGame, ShowAddRequested, PurchaseRequasted, OpenGarage);
            _profilePlayer.CreditCount.SubscribeOnChange(OnCreditChange);
            _view.UpdateCredit(_profilePlayer.CreditCount.Value);
            
        }

        private BaseController ConfigureCursorTrail()
        {
            TrailController trailController = new TrailController();;
            AddController(trailController);
            return trailController;
        }

        

        private void OnCreditChange(int creditValue)
        {
            _view.UpdateCredit(_profilePlayer.CreditCount.Value);
        }
        private void PurchaseRequasted(string idProduct)
        {
            _shop.Buy(idProduct);
        }
        private MainMenuView LoadView(Transform placeForUi)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
            AddGameObjects(objectView);

            return objectView.GetComponent<MainMenuView>();
        }
        private void ShowAddRequested()
        {
            _adsShower.ShowVideo(OnVideoShowSuccess);
        }

        private void OnVideoShowSuccess()
        {
            // Add model reward
        }
        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void OpenGarage()
        {
            _profilePlayer.CurrentState.Value = GameState.Garage;
        }
    }
}

