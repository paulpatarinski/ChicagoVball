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

			if (scheduleEntryModels != null) {
				foreach (var scheduleEntryModel in scheduleEntryModels) {
					result.Add (new ScheduleEntry {
						Day = scheduleEntryModel.StartTime.ToString ("dddd"),
						HoursOfOperation = FormatHoursOfOperation (scheduleEntryModel) 
					});				
				}
			}

			return result;
		}

		private string FormatHoursOfOperation (ScheduleEntryModel entryModel)
		{
			return entryModel.StartTime.ToString ("HH:mm") + " - " + entryModel.EndTime.ToString ("HH:mm");
		}
	}
}

