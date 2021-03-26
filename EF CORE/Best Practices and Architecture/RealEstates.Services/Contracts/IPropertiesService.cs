using RealEstates.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services.Contracts
{
    public interface IPropertiesService
    {
        void Add(string district, int price,
           int floor, int maxFloor, int size, int yardSize,
           int year, string propertyType, string buildingType);

        decimal AveragePricePerSquareMeter();

        IEnumerable<PropertyInfoDTO> Search(int minPrice, int maxPrice, int minSize, int maxSize);
    }
}
