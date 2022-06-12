using EntityDal.Models;
using EntityDal.Context;

namespace DomainLogic.Services
{
    using Interfaces;

    /// <summary>
    ///     Inherits a GenericRepository class to the Language class 
    ///     that it will be handle.
    /// </summary>
    public class LanguageService : GenericRepository<Language, short>,
        ILanguageService
    {
        #region Ctor

        /// <summary>
        ///     Calls the base constructor to remember an instance of DB context.
        /// </summary>
        /// <param name="dbContext">
        ///     The instance of the DB Context.
        /// </param>
        public LanguageService(RoboSvcContext dbContext) : base(dbContext) 
        { 
        }

        #endregion
    }
}
