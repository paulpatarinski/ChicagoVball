﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Core.Helpers.Controls;
using Core.Models;
using Core.Services;
using Xamarin.Forms.Maps;

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
			Mapper.CreateMap<VolleyballLocationModel, CustomPin> ().ForMember (dest => dest.Position, opt => opt.MapFrom (src => new Position (src.Latitude, src.Longitude)));

		  var locations = await _volleyballLocationService.GetLocationsAsync();
     
      foreach (var location in locations)
      {
        VolleyballLocations.Add(Mapper.Map<VolleyballLocationModel, CustomPin>(location));
      }
		}
	}
}