using AutoMapper;
using EntityLayer.Concretes;

namespace BusinessLayer.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
