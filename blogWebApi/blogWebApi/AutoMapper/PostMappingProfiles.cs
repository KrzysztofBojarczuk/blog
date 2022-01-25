using AutoMapper;
using blogWebApi.Dtos;
using blogWebApi.Models;

namespace blogWebApi.AutoMapper
{
    public class PostMappingProfiles : Profile
    {
        public PostMappingProfiles()
        {
            CreateMap<PostCreateDto, Post>();
            CreateMap<Post, PostGetDto>();
        }
    }
}
