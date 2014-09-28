using Core.Helpers.Controls;
using System.Collections.Generic;

namespace Core.Models
{
	public class VolleyballLocationModel
	{
		public string Label { get; set; }

		public string Address { get; set; }

		public string PinIcon { get; set; }

		public string PhoneNumber { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		/// <summary>
		/// A list of all the days this location is open
		/// </summary>
		/// <value>The schedule entries.</value>
		public IEnumerable<ScheduleEntryModel> ScheduleEntries { get; set; }

		public IEnumerable<ExtraDetailModel> Others { get; set; }
	}
}