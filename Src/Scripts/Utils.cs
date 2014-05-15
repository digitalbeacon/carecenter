// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Runtime.CompilerServices;

namespace DigitalBeacon.CareCenter
{
	public static class Utils
	{
		public static int age(Date dob)
		{
			var today = new Date();
			var age = today.getFullYear() - dob.getFullYear();
			var temp = new Date(today.getFullYear() - age, today.getMonth(), today.getDate());
			if (dob.getTime() > temp.getTime())
			{
				age--;
			}
			return age;
		}

		public static double parseDecimal(string val)
		{
			if (val == null)
			{
				return window.NaN;
			}
			return window.parseFloat(val.replace("$", "").replace(",", ""));
		}

		public static string formatDecimal(double val)
		{
			return (Math.round(val * 100) % 100 == 0) ? val.toFixed(0) : val.toFixed(2);
		}
	}
}