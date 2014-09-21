using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace Core.Helpers.Controls
{
	public class CustomPin
	{
		public string Label { get; set; }

		public string Address { get; set; }

		public string PinIcon { get; set; }

		public string PhoneNumber { get; set; }

		public Position Position { get; set; }

		public List<ScheduleEntry> ScheduleEntries { get; set; }

		/// <summary>
		/// Other data relevant to the pin. ie. Website Url, 
		/// </summary>
		/// <value>The others.</value>
		public List<Url> Others { get; set; }
	}
}

