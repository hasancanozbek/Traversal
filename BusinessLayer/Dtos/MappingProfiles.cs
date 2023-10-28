using AutoMapper;
using BusinessLayer.Dtos.Comment;
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
