using System.Linq;
using Prism.Regions;

namespace ClientAssignment.Helpers
{
    public static class RegionManagerExtension
    {
        public static void DeactivateView<T>(this IRegionManager regionManager, string regionName)
        {
            var region = regionManager.Regions[regionName];
            object view = region.ActiveViews.FirstOrDefault(av => av is T);
            if (view != null)
            {
                region.Deactivate(view);
            }
        }
    }
}
