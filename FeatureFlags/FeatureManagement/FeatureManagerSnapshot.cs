using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FeatureFlags
{
    /// <summary>
    /// Provides a snapshot of feature state to ensure consistency across a given request.
    /// </summary>
    public class FeatureManagerSnapshot : IFeatureManagerSnapshot
    {
        private readonly IFeatureManager _featureManager;
        private readonly IDictionary<string, bool> _flagCache = new Dictionary<string, bool>();
        private IEnumerable<string> _featureNames;

        public FeatureManagerSnapshot(IFeatureManager featureManager)
        {
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
        }

        public async IAsyncEnumerable<string> GetFeatureNamesAsync()
        {
            if (_featureNames == null)
            {
                var featureNames = new List<string>();

                await foreach (string featureName in _featureManager.GetFeatureNamesAsync().ConfigureAwait(false))
                {
                    featureNames.Add(featureName);
                }

                _featureNames = featureNames;
            }

            foreach (string featureName in _featureNames)
            {
                yield return featureName;
            }
        }

        public async Task<bool> IsEnabledAsync(string feature)
        {
            if (_flagCache.ContainsKey(feature))
            {
                return _flagCache[feature];
            }

            bool enabled = await _featureManager.IsEnabledAsync(feature).ConfigureAwait(false);

            _flagCache[feature] = enabled;

            return enabled;
        }

        public async Task<bool> IsEnabledAsync<TContext>(string feature, TContext context)
        {
            if (_flagCache.ContainsKey(feature))
            {
                return _flagCache[feature];
            }

            bool enabled = await _featureManager.IsEnabledAsync(feature, context).ConfigureAwait(false);

            _flagCache[feature] = enabled;

            return enabled;
        }
    }
}
