using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "AbilityItemConfigDataSource", menuName = "AbilityItemConfigDataSource")]
    public class AbilityItemConfigDataSource : ScriptableObject
    {
        [SerializeField]
        private AbilityConfig[] _abilityConfigs;

        public AbilityConfig[] AbilityConfigs => _abilityConfigs;
    }
}



