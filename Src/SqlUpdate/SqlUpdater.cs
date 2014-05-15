// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Reflection;
using Common.Logging;

namespace DigitalBeacon.CareCenter.SqlUpdate
{
	public class SqlUpdater : DigitalBeacon.SqlUpdate.SqlUpdater
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private const string Module = "CareCenter";

		public SqlUpdater()
		{
			ModuleName = Module;
			ResourceAssembly = GetType().Assembly;
		}

		static void Main(string[] args)
		{
			try
			{
				var updater = new SqlUpdater();
				updater.Run();
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				Log.Error(ex.StackTrace);
				if (ex.InnerException != null)
				{
					Log.Error(ex.InnerException.Message);
					Log.Error(ex.InnerException.StackTrace);
				}
			}
		}
	}
}
