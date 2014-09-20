using System;
using AutoMapper;
using Core.Helpers.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
	public class ScheduleEntryResolver: ValueResolver<IEnumerable<ScheduleEntryModel>, IEnumerable<ScheduleEntry>>
	{
		protected override IEnumerable<ScheduleEntry> ResolveCore (IEnumerable<ScheduleEntryModel> scheduleEntryModels)
		{
			var result = new List<ScheduleEntry> ();
			DateTime startDate = new DateTime (2010, 1, 4);
			DateTime endDate = new DateTime (2010, 1, 10);

			for (DateTime date = startDate; date <= endDate; date = date.AddDays (1)) {

				DayOfWeek dayOfWeek = date.DayOfWeek;

				ScheduleEntryModel entryModel = null;

				if (scheduleEntryModels != null)
					entryModel = scheduleEntryModels.FirstOrDefault (x => x.StartTime.DayOfWeek == dayOfWeek);

				result.Add (new ScheduleEntry {
					Day = date.ToString ("dddd"),
					HoursOfOperation = entryModel != null ? FormatHoursOfOperation (entryModel) : "Closed" 
				});
			}

			return result;
		}

		private string FormatHoursOfOperation (ScheduleEntryModel entryModel)
		{
			return entryModel.StartTime.ToString ("HH:mm") + " - " + entryModel.EndTime.ToString ("HH:mm");
		}
	}
}

