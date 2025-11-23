using AutoMapper;
using CineReview.DTOs;
using CineReview.Models;


namespace CineReview.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<Movie, MovieReadDto>();

            CreateMap<ReviewCreateDto, Review>();
            CreateMap<Review, ReviewReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MediaName, opt => opt.MapFrom(src => src.Media.Name));

            CreateMap<SerieCreateDto, Serie>();
            CreateMap<Serie, SerieReadDto>();

            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReadDto>();

            CreateMap<Media, MediaReadDto>();
        }
    }
}

