using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BoraWmsNew.Authorization;

namespace BoraWmsNew
{
    [DependsOn(
        typeof(BoraWmsNewCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BoraWmsNewApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BoraWmsNewAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BoraWmsNewApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
