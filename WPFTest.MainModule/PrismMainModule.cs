using Prism.Modularity;
using Prism.Regions;

namespace WPFTest.MainModule
{
   public class PrismMainModule:IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;
        public PrismMainModule(IRegionViewRegistry registry)
        {
            this.regionViewRegistry = registry;
        }
        public void Initialize()
        {
            regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof(Views.PrismView));
        }
    }
}
