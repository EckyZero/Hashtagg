using System;
using System.Diagnostics;
using Shared.Common;
using Shared.BL;
using Shared.DAL;
using System.Collections.Generic;
using Shared.BL.Interfaces;
using Shared.Common.Utils;
using System.Threading.Tasks;
using Shared.BL.BLs.Base;
using Shared.Common.Logging;

namespace Shared.BL
{
    public class LifecycleService : BaseBL, ILifecycleService
	{
		public LifecycleService()
		{

		}

		public void OnStart()
		{
            try
            {

            }
            catch (BaseException)
            {
            }
            catch (Exception e)
            {
                BLException exc = new BLException("Error OnStart", e);
                _logger.Log(exc, LogType.ERROR);
            }

		}

		public void OnSleep()
		{
            try
            {

            }
            catch (BaseException)
            {
            }
            catch (Exception e)
            {
                BLException exc = new BLException("Error OnSleep", e);
                _logger.Log(exc, LogType.ERROR);
            }

		}

		public void OnResume()
		{
            try
            {


            }
            catch (BaseException)
            {
            }
            catch (Exception e)
            {
                BLException exc = new BLException("Error OnResume", e);
                _logger.Log(exc, LogType.ERROR);
            }
		}
	}
}

