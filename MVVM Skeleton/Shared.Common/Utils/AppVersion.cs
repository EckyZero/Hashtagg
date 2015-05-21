
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common.Utils
{
    public class AppVersion
    {
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int BuildNumber { get; set; }

        public AppVersion()
        {
            MajorVersion = 0;
            MinorVersion = 0;
            BuildNumber = 0;
        }

        public AppVersion(int version)
        {
            MajorVersion = 0;
            MinorVersion = 0;
            BuildNumber = version;
        }


        public AppVersion(string version)
        {
            MajorVersion = 0;
            MinorVersion = 0;
            BuildNumber = 0;

            if (version == null)
            {
                return;
            }

            var splitVersion = version.Split(new char[]
            {
                '.',
            });
            //One Digit Build Number/Count (Android uses this for Builds)
            if (splitVersion.Length == 1)
            {
                var parseValue = -1;
                if (int.TryParse(splitVersion[0], out parseValue))
                    BuildNumber = parseValue;
                return;
            }
            //Two Digit Build Number/Count (For storing two digit versions
            if (splitVersion.Length == 2)
            {
                var parseValue = -1;
                if (int.TryParse(splitVersion[0], out parseValue))
                    MinorVersion = parseValue;
                if (int.TryParse(splitVersion[1], out parseValue))
                    BuildNumber = parseValue;
                return;
            }
            //Three Digit Build Number/Count (iOS uses this for Builds)
            if (splitVersion.Length == 3)
            {
                var parseValue = -1;
                if (int.TryParse(splitVersion[0], out parseValue))
                    MajorVersion = parseValue;
                if (int.TryParse(splitVersion[1], out parseValue))
                    MinorVersion = parseValue;
                if (int.TryParse(splitVersion[2], out parseValue))
                    BuildNumber = parseValue;
                return;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}", MajorVersion, MinorVersion, BuildNumber);
        }

        public static bool operator <(AppVersion c1, AppVersion c2)
        {
            if (c1.MajorVersion < c2.MajorVersion)
                return true;
            if(c1.MajorVersion > c2.MajorVersion)
                return false;
            if (c1.MinorVersion  < c2.MinorVersion)
                return true;
            if (c1.MinorVersion > c2.MinorVersion)
                return false;
            return c1.BuildNumber < c2.BuildNumber;
        }

        public static bool operator >(AppVersion c1, AppVersion c2)
        {
            if (c1 == c2)
                return false;
            return !(c1 < c2);
        }

		public static bool operator ==(AppVersion c1, AppVersion c2)
		{
		    if ((object) c1 == null && (object) c2 == null)
		        return true;
		    if ((object)c1 == null || (object)c2 == null)
		        return false;
			return (c1.MajorVersion == c2.MajorVersion && c2.MinorVersion == c1.MinorVersion && c2.BuildNumber == c1.BuildNumber);
		}

		public static bool operator !=(AppVersion c1, AppVersion c2)
		{
			return !(c1 == c2);
		}
    }
}
