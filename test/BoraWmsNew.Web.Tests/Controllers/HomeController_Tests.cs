using System.Threading.Tasks;
using BoraWmsNew.Models.TokenAuth;
using BoraWmsNew.Web.Controllers;
using Shouldly;
using Xunit;

namespace BoraWmsNew.Web.Tests.Controllers
{
    public class HomeController_Tests: BoraWmsNewWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}