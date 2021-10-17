using JoostenProductions;
using Tools;
using UnityEngine;

public class TrailController: BaseController
{
    private GameObject _trail;
    private TrailView _view;
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Trail" };
    public TrailController()
    {
        _view = LoadView();
        _view.Init(); 
    }


    private TrailView LoadView()
    {
        _trail = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
        AddGameObjects(_trail);
        
        return _trail.GetComponent<TrailView>();
    }
}


