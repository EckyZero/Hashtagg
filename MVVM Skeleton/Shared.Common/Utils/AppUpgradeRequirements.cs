using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common.Utils
{
    public class AppUpgradeRequirements
    {
        public string IOSMinimumRequiredVersion { get; set; }
        public string IOSMinimumRecommendedVersion { get; set; }
        public string IOSUpgradeUrl { get; set; }
        public string AndroidMinimumRequiredVersion { get; set; }
        public string AndroidMinimumRecommendedVersion { get; set; }
    }
}
