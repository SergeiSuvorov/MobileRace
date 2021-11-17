using Inventory;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public static class ContentDataSourceLoader
    {
        public static List<UpgradeItemConfig> LoadUpgradeItemConfigs(ResourcePath resourcePath)
        {
            var config = ResourceLoader.LoadObject<UpgradeItemConfigDataSource>(resourcePath);
            return config == null ? new List<UpgradeItemConfig>() : config.ItemConfigs.ToList();
        }

        public static List<AbilityConfig> LoadAbilityItemConfigs(ResourcePath resourcePath)
        {
            var config = ResourceLoader.LoadObject<AbilityItemConfigDataSource>(resourcePath);
            return config == null ? new List<AbilityConfig>() : config.AbilityConfigs.ToList();
        }
    }
}


