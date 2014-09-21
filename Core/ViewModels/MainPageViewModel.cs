using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Core.Helpers.Controls;
using Core.Models;
using Core.Services;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace Core.ViewModels
{
	public class MainPageViewModel : BaseViewModel
	{
		private readonly IVolleyballLocationService _volleyballLocationService;

		public MainPageViewModel (IVolleyballLocationService volleyballLocationService)
		{
			_volleyballLocationService = volleyballLocationService;
			LoadVolleyballLocationsAsync ();
		}

		ObservableCollection<CustomPin> _volleyballLocations;

		public ObservableCollection<CustomPin> VolleyballLocations {
			get {
				if (_volleyballLocations == null) {
					_volleyballLocations = new ObservableCollection<CustomPin> ();
				}
				return _volleyballLocations;
			}
			set { ChangeAndNotify (ref _volleyballLocations, value); }
		}

		public async Task LoadVolleyballLocationsAsync ()
		{
			Mapper.CreateMap<UrlModel, Url> ();

			Mapper.CreateMap<VolleyballLocationModel, CustomPin> ()
		    .ForMember (dest => dest.Position, opt => opt.MapFrom (src => new Position (src.Latitude, src.Longitude)))
		    .ForMember (dest => dest.PhoneNumber, opt => opt.MapFrom (src => src.PhoneNumber))
		    .ForMember (dest => dest.Others, opt => opt.MapFrom (src => src.Others ?? new List<UrlModel> ()))
		    .ForMember (dest => dest.ScheduleEntries,
				opt =>
		        opt.ResolveUsing<ScheduleEntryResolver> ()
		          .FromMember (src => src.ScheduleEntries ?? new List<ScheduleEntryModel> ()));
				
			var locations = await _volleyballLocationService.GetLocationsAsync ();
     
			foreach (var location in locations) {
				VolleyballLocations.Add (Mapper.Map<VolleyballLocationModel, CustomPin> (location));
			}
		}
	}
}