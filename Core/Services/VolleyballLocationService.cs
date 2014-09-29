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
				PinIcon = Icons.BeachVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Tuesday).AddHours (16).AddMinutes (30)
						,
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Tuesday).AddHours (21)
					},
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Saturday).AddHours (9)
							,
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Saturday).AddHours (16)
					},
				},
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.cpdbeaches.com/beaches/montrose-beach/"
					}, new ExtraDetailModel {
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
				PinIcon = Icons.BeachVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Sunday).AddHours (9)
							,
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Sunday).AddHours (13)
					},
				},				
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.cpdbeaches.com/beaches/north-avenue-beach/"
					}, new ExtraDetailModel {
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
				PinIcon = Icons.IndoorVball,
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.windycityfieldhouse.com/"
					}, new ExtraDetailModel {
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
				PinIcon = Icons.IndoorVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Monday).AddHours (19).AddMinutes (30)
							,
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Monday).AddHours (23)
					},
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Wednesday).AddHours (19).AddMinutes (30)
							,
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Wednesday).AddHours (23)
					},
				},	
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "https://www.mppd.org/facility/rec-plex"
					}, new ExtraDetailModel {
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
				PinIcon = Icons.IndoorVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Thursday).AddHours (19)
							,
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Thursday).AddHours (22)
					},
				},	
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.palatineparks.org/"
					}, new ExtraDetailModel {
						Key = "Type",
						Value = "Indoor"
					}
				}
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Hanover Park District", 
				Address = "1919 Walnut Ave, Hanover Park, IL",
				Latitude = 41.993697,
				Longitude = -88.152380,
				PhoneNumber = "(630) 837-6300",
				PinIcon = Icons.IndoorVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Wednesday).AddHours (19)
							,
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Wednesday).AddHours (21).AddMinutes (30)
					},
				},	
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.hpparks.org/"
					}, new ExtraDetailModel {
						Key = "Type",
						Value = "Indoor"
					}
				}
			});


			locations.Add (new VolleyballLocationModel {
				Label = "Glenview Park District", 
				Address = "2400 Chestnut Ave, Glenview, IL",
				Latitude = 42.088504,
				Longitude = -87.816723,
				PhoneNumber = "(847) 724-5670",
				PinIcon = Icons.IndoorVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Sunday).AddHours (14),
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Sunday).AddHours (17)
					},
				},	
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.glenviewparks.org/"
					}, new ExtraDetailModel {
						Key = "Type",
						Value = "Indoor"
					}
				}
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Streamwood Park District", 
				Address = "550 S Park Blvd, Streamwood, IL",
				Latitude = 42.020581,
				Longitude = -88.170744,
				PhoneNumber = "(630) 483-3010",
				PinIcon = Icons.IndoorVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Monday).AddHours (18).AddMinutes (30),
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Monday).AddHours (20).AddMinutes (30)
					},
				},	
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.streamwoodparkdistrict.org/"
					}, new ExtraDetailModel {
						Key = "Type",
						Value = "Indoor"
					}
				}
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Edgar A Poe School", 
				Address = "2800 N Highland Ave, Arlington Heights, IL",
				Latitude = 42.126878,
				Longitude = -87.985978,
				PhoneNumber = "(847) 670-3200",
				PinIcon = Icons.IndoorVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Monday).AddHours (19),
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Monday).AddHours (21).AddMinutes (30)
					},
				},	
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.poe.d21.k12.il.us/"
					}, new ExtraDetailModel {
						Key = "Type",
						Value = "Indoor"
					}
				}
			});

			locations.Add (new VolleyballLocationModel {
				Label = "Meineke Pool", 
				Address = "220 East Weathersfield Way, Schaumburg, IL",
				Latitude = 42.017050,
				Longitude = -88.073599,
				PhoneNumber = "(847) 985-2133",
				PinIcon = Icons.IndoorVball,
				ScheduleEntries = new List<ScheduleEntryModel> {
					new ScheduleEntryModel { StartTime = new DateTime ().FromWeekDay (DayOfWeek.Friday).AddHours (19),
						EndTime = new DateTime ().FromWeekDay (DayOfWeek.Friday).AddHours (21).AddMinutes (30)
					},
				},	
				Others = new List<ExtraDetailModel> { new ExtraDetailModel {
						Key = "Website",
						Value = "http://www.parkfun.com/facilities/meineke-recreation-center"
					}, new ExtraDetailModel {
						Key = "Type",
						Value = "Indoor"
					}
				}
			});
		
			return locations;
		}
	}
}

