using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace BoraWmsNew.Localization
{
    public static class BoraWmsNewLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(BoraWmsNewConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(BoraWmsNewLocalizationConfigurer).GetAssembly(),
                        "BoraWmsNew.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
