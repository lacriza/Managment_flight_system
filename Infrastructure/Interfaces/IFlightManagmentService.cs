using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
  public interface IFlightManagmentService
  {
    Task<IEnumerable<Flight>> ListAsync();
  }
}
