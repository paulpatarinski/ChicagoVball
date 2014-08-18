using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services
{
  public interface IVolleyballLocationService
  {
    Task<List<VolleyballLocationModel>> GetLocationsAsync ();
  }
}