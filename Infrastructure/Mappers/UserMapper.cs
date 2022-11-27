using AutoMapper;
using Infrastructure.Models.User;
using Repository.Entities;

namespace Infrastructure.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>()
            .ForMember(
                dest => dest.FirstName,
                opt => opt.MapFrom(src => $"{src.FirstName}")
            )
            .ForMember(
                dest => dest.LastName,
                opt => opt.MapFrom(src => $"{src.LastName}")
            )
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => src.Email)
            )
            .ForMember(
                dest => dest.Active,
                opt => opt.MapFrom(src => src.Active)
            )
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            ).ReverseMap();

        CreateMap<CreateUserDto, User>()
            .ForMember(
                dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName)
            )
            .ForMember(
                dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName)
            )
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => src.Email)
            )
            .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email)
            ).ReverseMap();
    }
}