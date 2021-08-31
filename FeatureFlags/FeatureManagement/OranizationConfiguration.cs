using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureFlags
{
    internal class OranizationConfiguration : IConfiguration
    {
        private readonly List<string> _enabledOrgs;
        public OranizationConfiguration(List<string> enabledOrgs)
        {
            _enabledOrgs = enabledOrgs;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return Enumerable.Empty<IConfigurationSection>();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotSupportedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotSupportedException();
        }

        public string this[string key]
        {
            get
            {
                if (_enabledOrgs.Contains(key))
                {
                    return key;
                }

                return null;
            }

            set => throw new NotSupportedException();
        }
    }
}
