using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarDealer.DTO;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //CreateMap<Part, PartDTO>()
            //    .ForMember(x => x.SupplierId, y => y.MapFrom(x => x.Supplier.Id));

            this.CreateMap<CarDTO, Car>()
                 .ForMember(x => x.PartCars,y => y.MapFrom(x => x.PartsId));

            this.CreateMap<PartDTO, Part>();
        }
    }
}
