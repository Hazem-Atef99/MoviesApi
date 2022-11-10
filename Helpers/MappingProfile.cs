using AutoMapper;

namespace MoviesApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
          CreateMap<Movie, MoviesDetailsDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(src=>src.poster,opt =>opt.Ignore());
        }
    }
}
