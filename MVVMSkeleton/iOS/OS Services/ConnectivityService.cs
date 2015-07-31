using System;
using System.Net;
using SystemConfiguration;
using Shared.Common;
using CoreFoundation;

namespace iOS
{
    public class ConnectivityService : BaseService, IConnectivityService
    {
        private NetworkReachability _defaultRouteReachability;

        public ConnectivityService()
        {
            _defaultRouteReachability = new NetworkReachability(new IPAddress(0));
            _defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
        }

        public bool IsConnected
        {
            get
            {
                NetworkReachabilityFlags flags;
                if (_defaultRouteReachability.TryGetFlags(out flags))
                {
                    // Is it reachable with the current network configuration?
                    bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

                    // Do we need a connection to reach it?
                    bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

                    // Since the network stack will automatically try to get the WAN up,
                    // probe that
                    if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                        noConnectionRequired = true;

                    return isReachable && noConnectionRequired;
                }
                return false;
            }
        }
    }
}