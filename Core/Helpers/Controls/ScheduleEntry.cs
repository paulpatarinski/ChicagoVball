using Xamarin.Forms.Maps;
using System.Collections.Generic;
using System;

namespace Core.Helpers.Controls
{
	public class ScheduleEntry
	{
		public string Day {
			get;
			set;
		}

		/// <summary>
		/// The start and end time
		/// </summary>
		/// <value>The times.</value>
		public string HoursOfOperation { get; set; }
	}
}