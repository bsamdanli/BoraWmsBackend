using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BoraWmsNew.EntityFrameworkCore;
using BoraWmsNew.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace BoraWmsNew.Web.Tests
{
    [DependsOn(
        typeof(BoraWmsNewWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class BoraWmsNewWebTestModule : AbpModule
    {
        public BoraWmsNewWebTestModule(BoraWmsNewEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoraWmsNewWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(BoraWmsNewWebMvcModule).Assembly);
        }
    }
}