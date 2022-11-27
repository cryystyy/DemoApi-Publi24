using AutoMapper;
using Infrastructure.Models.Company;
using Infrastructure.Models.User;
using Repository.Entities;

namespace Infrastructure.Mappers;

public class CompanyMapper : Profile
{
    public CompanyMapper()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Isin,
                opt => opt.MapFrom(src => src.Isin)
            )
            .ForMember(
                dest => dest.Exchange,
                opt => opt.MapFrom(src => src.Exchange)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.Ticker,
                opt => opt.MapFrom(src => src.Ticker)
            )
            .ForMember(
                dest => dest.Website,
                opt => opt.MapFrom(src => src.Website)
            ).ReverseMap();

        CreateMap<CreateCompanyDto, Company>()
            .ForMember(
                dest => dest.Isin,
                opt => opt.MapFrom(src => src.Isin)
            )
            .ForMember(
                dest => dest.Exchange,
                opt => opt.MapFrom(src => src.Exchange)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.Ticker,
                opt => opt.MapFrom(src => src.Ticker)
            )
            .ForMember(
                dest => dest.Website,
                opt => opt.MapFrom(src => src.Website)
            ).ReverseMap();
    }
}