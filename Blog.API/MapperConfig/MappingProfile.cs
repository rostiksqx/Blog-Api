using AutoMapper;
using Blog.API.Contracts;
using Blog.Core.Models;

namespace Blog.API.MapperConfig;

public class MappingProfile : Profile 
{
    public MappingProfile()
    {
        CreateMap<User, RegisterUserResponse>();
    }
}