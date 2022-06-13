using System.ComponentModel.DataAnnotations;

using EntityDal.Interfaces;
using DomainLogic.Services;
using DomainLogic.Interfaces;

namespace PublicApi.Models.Classifiers
{
    using Validation;
    
    /// <summary>
    ///     Represents the data of the Language view model classifier.
    /// </summary>
    public class LanguageViewModel : IEntity<short>
    {
        #region Properties

        /// <summary>
        ///     ID.
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        ///     ISO language name.
        /// </summary>
        [Required]
        [Unique(typeof(ILanguageService), typeof(LanguageService), nameof(Id))]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        ///     ISO 639-1 code.
        /// </summary>
        [Required]
        [Unique(typeof(ILanguageService), typeof(LanguageService), nameof(Id))]
        [RegularExpression(@"^[a-z]{2}$", 
            ErrorMessage = "This value must contains only 2 lower case English letters.")]
        public string Alpha2 { get; set; } = null!;

        /// <summary>
        ///     Language notes.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        ///    The digital code consisting of 3 Arabic numerals and 
        ///    assigned to languages arranged in the order of Russian 
        ///    names. 
        /// </summary>
        [Unique(typeof(ILanguageService), typeof(LanguageService), nameof(Id))]
        [RegularExpression(@"^$|^[\d]{3}$",
            ErrorMessage = "This value must be zero or contains only 3 Arabic numerals.")]
        public string? DigitalCode { get; set; }

        #endregion
    }
}
