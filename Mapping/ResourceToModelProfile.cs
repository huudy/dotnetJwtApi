using AutoMapper;
using myJWTAPI.Controllers.Resources;
using myJWTAPI.Core.Models;

namespace myJWTAPI.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<UserCredentialsResource, User>();
        }
    }
}