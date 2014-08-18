using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services
{
	public class VolleyballLocationService : IVolleyballLocationService
	{
		public async Task<List<VolleyballLocationModel>> GetLocationsAsync ()
		{
			var locations = new List<VolleyballLocationModel> ();

			locations.Add (new VolleyballLocationModel {
				Label = "Montrose Ave Beach", 
				Address = "200 W Montrose Dr, Chicago, IL",
				Latitude = 41.965097,
				Longitude = -87.634818,
				PhoneNumber = "Unknown",
				PinIcon = Icons.CrazyRobot
			});

			locations.Add (new VolleyballLocationModel {
				Label = "RecPlex", 
				Address = "420 Dempster St, Mt Prospect, IL",
				Latitude = 42.038580,
				Longitude = -87.944055,
				PhoneNumber = "(847)640-1000",
				PinIcon = Icons.CrazyRobot
			});

			return locations;
		}
	}
}

