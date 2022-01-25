using AutoMapper;
using blogWebApi.Dtos;
using blogWebApi.Models;

namespace blogWebApi.AutoMapper
{
    public class CommentMappingProfiles : Profile
    {
        public CommentMappingProfiles()
        {

            CreateMap<CommentCreateDto, Comment>();
            CreateMap<Comment, CommentGetDto>();
        }
        }
    }
