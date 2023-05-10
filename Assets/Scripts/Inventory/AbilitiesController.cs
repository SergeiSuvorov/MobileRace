using System.Linq;
using Inventory;
using Items;
using UnityEngine;

public class AbilitiesController : BaseController
{
    private readonly IInventoryModel _inventory;
    private readonly IAbilityRepository _abilitiesRepository;
    private readonly IAbilityCollectionView _view;
    private readonly IAbilityActivator _activator;
    private IInventoryModel inventoryModel;
    private IAbilityRepository abilitiesRepository;
    private AbilityCollectionViewStub abilityCollectionViewStub;
    private CarController carController;

    public AbilitiesController(IInventoryModel inventory, IAbilityRepository abilitiesRepository,
        IAbilityCollectionView view, IAbilityActivator activator)
    {
        _inventory = inventory;
        _abilitiesRepository = abilitiesRepository;
        _view = view;
        _activator = activator;

        var equiped = inventory.GetEquippedItems();
        var equipedAbilities = equiped
            .Where(i => _abilitiesRepository.AbilityMapById.ContainsKey(i.Id));

        view.Display(equipedAbilities.ToList());
        view.UseRequested += OnAbilityRequested;
    }

   

    private void OnAbilityRequested(object sender, IItem e)
    {
        var ability = _abilitiesRepository.AbilityMapById[e.Id];
        ability.Apply(_activator);
    }
}