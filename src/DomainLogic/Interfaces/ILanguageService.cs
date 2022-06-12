using EntityDal.Models;

namespace DomainLogic.Interfaces
{
    /// <summary>
    ///     Declares the interface extending IGenericRepository to the Language class that it will handle.
    /// </summary>
    public interface ILanguageService : IGenericRepository<Language, short>
    {
    }
}
