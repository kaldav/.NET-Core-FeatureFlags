using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace FeatureFlags
{
    [FilterAlias("Organization")]
    public class OrganizationFilter : IContextualFeatureFilter<IOrganizationContext>
    {
        public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context, IOrganizationContext organizationContext)
        {
            var incomingOrganizationName = organizationContext.OrganizationName;
            var enabledOrganizationName = context.Parameters[incomingOrganizationName];

            return incomingOrganizationName == enabledOrganizationName;
        }
    }
}
