using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Helpers.Controls;
using System;

namespace Core.Services
{
	public class VolleyballLocationService : IVolleyballLocationService
	{
		public async Task<List<VolleyballLocationModel>> GetLocationsAsync ()
		{
			var locations = new List<VolleyballLocationModel> ();

			//Chicago
			locations.Add (new VolleyballLocationModel {
				Label = "Montrose Ave Beach", 
				Address = "200 W Montrose Dr, Chicago, IL",
				Latitude = 41.965097,
				Longitude = -87.634818,
				PhoneNumber = "(312) 742-5121",
				PinIcon = Icons.CrazyRobot,
				ScheduleEntries = new List<ScheduleEntryModel> {
					//Tuesday
					new ScheduleEntryModel { StartTime = new DateTime (2014, 9, 16, 16, 30, 0)
						,
						EndTime = new DateTime (2014, 9, 16, 21, 0, 0)
					},
					//Saturday
					new ScheduleEntryModel { StartTime = new DateTime (2014, 9, 20, 9, 0, 0)
							,
						EndTime = new DateTime (2014, 9, 20, 16, 0, 0)
					}
				},
				Others = new List<UrlModel> { new UrlModel {
						Key = "Website",
						Value = "http://www.cpdbeaches.com/beaches/montrose-beach/"
					}, new UrlModel {
						Key = "Type",
						Value = "Sand"
					}
				}
			});

			locations.Add (new VolleyballLocationModel {
				Label = "North Ave Beach", 
				Address = "1603 N Lake Shore Dr, Chicago, IL",
				Latitude = 41.913559,
				Longitude = -87.622498,
				PhoneNumber = "(312) 742-5121",
				PinIcon = Icons.CrazyRobot,
				ScheduleEntries = new List<ScheduleEntryModel> {
					//Sunday
					new ScheduleEntryModel { StartTime = new DateTime (2014, 9, 21, 9, 0, 0),
						EndTime = new DateTime (2014, 9, 21, 13, 0, 0)
					}
				},				
				Others = new List<UrlModel> { new UrlModel {
						Key = "Website",
						Value = "http://www.cpdbeaches.com/beaches/north-avenue-beach/"
					}, new UrlModel {
						Key = "Type",
						Value = "Sand"
					}
				}
			});

			//TODO: add schedule
			locations.Add (new VolleyballLocationModel {
				Label = "Windy City Fieldhouse", 
				Address = "2367 W Logan Blvd, Chicago, IL",
				Latitude = 41.928497,
				Longitude = -87.686766,
				PhoneNumber = "(773) 486-7300",
				PinIcon = Icons.CrazyRobot,
				Others = new List<UrlModel> { new UrlModel {
						Key = "Website",
						Value = "http://www.windycityfieldhouse.com/"
					}, new UrlModel {
						Key = "Type",
						Value = "Indoor"
					}
				}
			});


			//Suburbs
			locations.Add (new VolleyballLocationModel {
				Label = "RecPlex", 
				Address = "420 Dempster St, Mt Prospect, IL",
				Latitude = 42.038580,
				Longitude = -87.944055,
				PhoneNumber = "(847) 640-1000",
				PinIcon = Icons.CrazyRobot,
				ScheduleEntries = new List<ScheduleEntryModel> {
					//Monday
					new ScheduleEntryModel { StartTime = new DateTime (2014, 9, 15, 19, 30, 0),
						EndTime = new DateTime (2014, 9, 15, 23, 0, 0)
					},
					//Wednesday
					new ScheduleEntryModel { StartTime = new DateTime (2014, 9, 17, 19, 30, 0),
						EndTime = new DateTime (2014, 9, 17, 23, 0, 0)
					}
				},	
				Others = new List<UrlModel> { new UrlModel {
						Key = "Website",
						Value = "https://www.mppd.org/facility/rec-plex"
					}, new UrlModel {
						Key = "Type",
						Value = "Sand/Indoor"
					}
				}
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Palatine Park District", 
				Address = "200 E Wood St, Palatine, IL",
				Latitude = 42.113547,
				Longitude = -88.038834,
				PhoneNumber = "(847) 991-0333",
				PinIcon = Icons.CrazyRobot
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Hanover Park District", 
				Address = "1919 Walnut Ave, Hanover Park, IL",
				Latitude = 41.993697,
				Longitude = -88.152380,
				PhoneNumber = "(630) 837-6300",
				PinIcon = Icons.CrazyRobot
			});


			locations.Add (new VolleyballLocationModel {
				Label = "Glenview Park District", 
				Address = "2400 Chestnut Ave, Glenview, IL",
				Latitude = 42.088504,
				Longitude = -87.816723,
				PhoneNumber = "(847) 724-5670",
				PinIcon = Icons.CrazyRobot
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Streamwood Park District", 
				Address = "550 S Park Blvd, Streamwood, IL",
				Latitude = 42.020581,
				Longitude = -88.170744,
				PhoneNumber = "(630) 483-3010",
				PinIcon = Icons.CrazyRobot
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Edgar A Poe School", 
				Address = "2800 N Highland Ave, Arlington Heights, IL",
				Latitude = 42.126878,
				Longitude = -87.985978,
				PhoneNumber = "(847) 670-3200",
				PinIcon = Icons.CrazyRobot
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Meineke Pool", 
				Address = "220 East Weathersfield Way, Schaumburg, IL",
				Latitude = 42.017050,
				Longitude = -88.073599,
				PhoneNumber = "(847) 985-2133",
				PinIcon = Icons.CrazyRobot
			});
		
			return locations;
		}
	}
}

