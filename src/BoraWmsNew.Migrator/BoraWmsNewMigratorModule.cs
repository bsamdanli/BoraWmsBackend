using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BoraWmsNew.Configuration;
using BoraWmsNew.EntityFrameworkCore;
using BoraWmsNew.Migrator.DependencyInjection;

namespace BoraWmsNew.Migrator
{
    [DependsOn(typeof(BoraWmsNewEntityFrameworkModule))]
    public class BoraWmsNewMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public BoraWmsNewMigratorModule(BoraWmsNewEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(BoraWmsNewMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                BoraWmsNewConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoraWmsNewMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
