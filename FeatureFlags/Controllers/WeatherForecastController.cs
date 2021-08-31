using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace FeatureFlags.Controllers
{
    [ApiController]
    [Route("weatherforecast")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;

        public WeatherForecastController(IFeatureManager featureManager, ILogger<WeatherForecastController> logger)
        {
            _featureManager = featureManager;
        }

        //[FeatureGate(MyFeatureFlags.FeatureA)]
        [HttpGet]
        public async Task<string> GetAsync()
        {
            var a = _featureManager.GetFeatureNamesAsync();
            var response = "";
            response += $"FeatureA for org1:{await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA, new OrganizationContext { OrganizationName = "org1" })}\r\n";
            response += $"FeatureA for org2:{await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA, new OrganizationContext { OrganizationName = "org2" })}\r\n";
            response += $"FeatureA for org3:{await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA, new OrganizationContext { OrganizationName = "org3" })}\r\n";
            response += $"FeatureA without org:{await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA)}\r\n";
            response += $"FeatureB without org:{await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureB)}\r\n";
            response += $"FeatureB for org1:{await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureB, new OrganizationContext { OrganizationName = "org1" })}\r\n";

            return response;
        }
    }
}
