using AutoMapper;
using BlogPostWebApi.DTOs.Comments;
using BlogPostWebApi.DTOs.Posts;
using BlogPostWebApi.DTOs.Users;
using BlogPostWebApi.Entities;

namespace BlogPostWebApi.Common.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Post, AddPostDto>().ReverseMap();
        CreateMap<Post, PostDto>();

        CreateMap<User, AddUserDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Image, opt => opt.Ignore());
        CreateMap<User, UpdateUserDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Image, opt => opt.Ignore());
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

        CreateMap<Comment, AddCommentDto>().ReverseMap();
    }
}