using AutoMapper;

namespace Notes.Application.Common.Mapping
{
    public class IMapWith<T>
    {
        void Mapping(Profile profile) =>
           profile.CreateMap(typeof(T), GetType());
    }
}
