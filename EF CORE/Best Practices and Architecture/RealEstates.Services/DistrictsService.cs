using RealEstates.Data;
using RealEstates.Services.Contracts;
using RealEstates.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{
    public class DistrictsService : IDistrictService
    {
        private readonly ApplicationDbContext dbContext;
        public DistrictsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<DistrictInfoDTO> GetMostExpensiveDistricts(int count)
        {
            var districts = dbContext.Districts.Select(x => new DistrictInfoDTO
            {
                Name = x.Name,
                PropertiesCount = x.Properties.Count,
                AveragePricePerSquareMeter = x.Properties.Where(p => p.Price.HasValue)
                     .Average(p => p.Price / (decimal)p.Size) ?? 0
            })
                .OrderByDescending(x => x.AveragePricePerSquareMeter)
             .Take(count)
              .ToList();

            return districts;
        }
        public IEnumerable<DistrictInfoDTO> GetCheapestDistrits()
        {
            var cheapestDistricts = dbContext.Districts
                .Select(x => new DistrictInfoDTO
                {
                    Name = x.Name,
                    PropertiesCount = x.Properties.Count,
                    AveragePricePerSquareMeter = x.Properties
                    .Where(p => p.Price.HasValue)
                    .Average(p => p.Price / (decimal)p.Size) ?? 0
                })
                .OrderBy(x => x.AveragePricePerSquareMeter)
                .Take(10)
                .ToList();

            return cheapestDistricts;
        }
    }
}
