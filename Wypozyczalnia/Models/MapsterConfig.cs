using Mapster;
using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Models;

public class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Author, AuthorViewModel>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.LastName, src => src.LastName);
        TypeAdapterConfig<AuthorViewModel, Author>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.LastName, src => src.LastName);
    }
}