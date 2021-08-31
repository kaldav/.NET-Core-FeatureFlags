using Microsoft.FeatureManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlags
{
    public class FeatureDefinitionProvider : IFeatureDefinitionProvider
    {
        public Task<FeatureDefinition> GetFeatureDefinitionAsync(string featureName)
        {
            // todo: query this data from database and add caching

            FeatureDefinition featureDefinition;
            if (featureName == MyFeatureFlags.FeatureA)
            {
                featureDefinition = new FeatureDefinition //enabled for some organizations
                {
                    Name = featureName,
                    EnabledFor = new[]
                    {
                        new FeatureFilterConfiguration
                        {
                            Name = "Organization",//"AlwaysOn",
                            Parameters = new OranizationConfiguration(new List<string>{"org1","org2"})
                        }
                    }
                };
            }
            else
            {
                featureDefinition = new FeatureDefinition //disabled
                {
                    Name = featureName
                };
            }

            return Task.FromResult(featureDefinition);
        }

        public async IAsyncEnumerable<FeatureDefinition> GetAllFeatureDefinitionsAsync()
        {
            foreach (var featureDefinition in new[]
            {
                await GetFeatureDefinitionAsync(MyFeatureFlags.FeatureA),
                await GetFeatureDefinitionAsync(MyFeatureFlags.FeatureB)
            })
            {
                yield return featureDefinition;
            }
        }
    }
}
