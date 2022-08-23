using Abp.AutoMapper;
using BoraWmsNew.Authentication.External;

namespace BoraWmsNew.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
