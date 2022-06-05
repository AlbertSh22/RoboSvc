using AutoMapper;

using EntityDal.Models;

namespace PublicApi.Infrastructure
{
    using Models.Classifiers;

    /// <summary>
    ///     Provides a named configuration for maps.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        ///     Creates and maintains all mappings.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Language, LanguageViewModel>()
                .ReverseMap();
            // etc. ...
        }
    }
}
