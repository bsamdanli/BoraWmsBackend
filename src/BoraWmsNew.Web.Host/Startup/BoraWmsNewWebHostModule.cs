using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BoraWmsNew.Configuration;

namespace BoraWmsNew.Web.Host.Startup
{
    [DependsOn(
       typeof(BoraWmsNewWebCoreModule))]
    public class BoraWmsNewWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BoraWmsNewWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoraWmsNewWebHostModule).GetAssembly());
        }
    }
}
