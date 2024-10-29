using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Automappers {

    public class MappingProfile: Profile {

        public MappingProfile () {
            // This only works if all the attributes have the same name
            CreateMap<JuiceInsertDto, Juice>();
            
            // This works to assigning those attributes with different names
            // Id = beer.BeerId <- In the case of Hector de Leon 
            // Id = juice.Id <- Doesn't affect to me
            // ForMember(Destiny) = MapFrom(Origin)
            CreateMap<Juice, JuiceDto>()
                .ForMember(dto => dto.Id, // Receive a First class functions
                           m => m.MapFrom(j => j.Id));

            CreateMap<JuiceUpdateDto, Juice>();

            CreateMap<BrandInsertDto, Brand>();
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandUpdateDto, Brand>();
        }

    }

}
