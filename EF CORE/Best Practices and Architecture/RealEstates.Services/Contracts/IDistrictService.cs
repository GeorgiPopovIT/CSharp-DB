using RealEstates.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services.Contracts
{
    public interface IDistrictService
    {
        IEnumerable<DistrictInfoDTO> GetMostExpensiveDistricts(int count);
        IEnumerable<DistrictInfoDTO> GetCheapestDistrits();
    }
}
